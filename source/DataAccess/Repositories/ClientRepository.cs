using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Client;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ClientRepository : GenericRepository, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context) { }


    #region Add
    public async Task<ResponseId> CreateWithImage(HotClientDto dto, Stream? image = null)
    {
        var entity = (HosClient)dto;

        try
        {
            if (image != null)
            {
                var imageName = Helper.GenerateImageName();
                _ = DataAccessImageService.SaveSingleImage(image, imageName);
                entity.Photo = imageName;
            }
            else
                entity.Photo = null;

            var result = await Context.HosClients.AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
                entity.CodeNumber = "bui-" + Context.HosClients.Count().ToString();
            var row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return new ResponseId(true, "created ", result.Entity.Id);
            }
            return new ResponseId(false, "No row effected ", 0);
        }
        catch (Exception ex)
        {
            return new ResponseId(false, ex.Message, 0);
        }
    }
    #endregion


    #region Update
    public async Task<Response<HotClientDto?>> Update(HotClientDto dto, int id, Stream? image = null)
    {
        Response<HotClientDto?> respons;
        try
        {
            var current = Context.HosClients.Find(id);
            if (current == null)
                return respons = new(false, $"id: {id} is not found");
            dto.Id = id;
            var imageName = "";
            var modfied = false;

            if (image != null)
            {
                // if photo in database is null
                if (string.IsNullOrEmpty(current.Photo))
                {
                    imageName = Helper.GenerateImageName();
                    _ = DataAccessImageService.SaveSingleImage(image, imageName);
                    dto.Photo = imageName;
                    modfied = true;
                }
                else
                {
                    _ = DataAccessImageService.UpdateSingleImage(image, current.Photo);
                    modfied = false;
                }
            }

            current = dto;
            Context.Update(current).Property(propa => propa.Photo).IsModified = modfied;
            Context.Entry(current).Property(p => p.ClientStatus).IsModified = false;

            await Context.SaveChangesAsync();
            return respons = new(true, $"update on id: {id}", null);
        }
        catch (Exception ex)
        {
            return respons = new(false, "can not duplicate foreignKey with same Id ......." + ex.Message + "________and _______" + ex.InnerException?.Message, null); ;
        }
    }
    #endregion


    #region Read
    public async Task<HotClientDto?> ReadById(int? Id)
    {
        try
        {
            var entity = await Context.HosClients.FirstOrDefaultAsync(x => x.Id == Id);
            return entity!;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedResponse<HotClientDto>?> ReadAll(byte? status, int? pageSize, int? page)
    {

        IQueryable<HosClient> query = Context.HosClients;

        var totalCount = 0;
        if (status is not null)
        {
            query = query.Where(h => h.ClientStatus.Equals(status));
        }

        query = query.OrderByDescending(o => o.NameOriginalLang);

        totalCount = query.Count();
        if (totalCount < 0)
            return null;

        // page size
        GenericPagination(ref query, ref pageSize, ref page, totalCount);

        await query.ToListAsync();

        var result = HotClientDto.ToList(query);
        var all = new PagedResponse<HotClientDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = result.ToList()
        };

        return all;
    }


    public async Task<PagedResponse<HotClientDto>?> SearchByNameOrCode(byte? status, string searchTerm, int? page, int? pageSize)
    {
        var query = Context.HosClients.Where(n => n.NameOriginalLang.Contains(searchTerm) || n.NameEn.Contains(searchTerm) || n.CodeNumber.Contains(searchTerm));

        if (status is not null)
        {
            query = query.Where(h => h.ClientStatus.Equals(status));
        }

        var totalCount = await query.CountAsync();

        GenericPagination(ref query, ref pageSize, ref page, totalCount);

        var listDto = await query.OrderByDescending(h => h.Id)
                                 .ToListAsync();

        var all = new PagedResponse<HotClientDto>
        {
            Total = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = HotClientDto.ToList(listDto)
        };

        return all;
    }
    #endregion
}
