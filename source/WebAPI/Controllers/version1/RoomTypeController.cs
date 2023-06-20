using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Dtos;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]", Order = 000)]
[ApiController]
[ApiVersion("1.0")]
public class RoomTypeController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public RoomTypeController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] RoomType model)
    {

        if (model == null)
        {
            return BadRequest(new Error("400", "model is requerd"));
        }
        var res = await Data.RoomTypes.GenericCreate(model);
        int id = 0;

        if (res.Success)
        {
            if (res.Value is not null)
                id = res.Value.Id;
            var response = new ResponseId(res.Success, res.Message, id);
            return Created("", response);
        }
        return BadRequest(res);

    }


    // ============================= get ============================= 


    [HttpGet(Order = 0801)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1) return BadRequest(new Error("400", "can not assign 0"));

        Expression<Func<RoomType, object>>? filterExpression;
        if (lang != null)
        {
            filterExpression = inc => inc.RoomTypeTranslations.Where(l => l.LangCode == lang);
        }
        else
            filterExpression = inc => inc.RoomTypeTranslations;

        var result = await Data.RoomTypes.GenericReadById(i => i.Id == id, filterExpression);

        return Ok(result);
    }


    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] int? page, [FromQuery] int? pageSize)
    {

        var result = await Data.RoomTypes.GenericReadAll<RoomTypeTranslation>(t => t.LangCode.Equals(lang), null, page, pageSize);
        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] bool? isActive, [FromQuery] int? pageSize, [FromQuery] int? page, [FromQuery] string? lang)
    {
        Expression<Func<RoomType, bool>>? filter;
        if (isActive is not null)
        {
            filter = f => f.IsDeleted == !isActive;
        }
        else filter = null;
        //filter = isActive != null ? (filter = f => f.IsDeleted == !isActive) : null;

        var result = await Data.RoomTypes.GenericReadAllWihInclude(filter, o => o.Id, inc => inc.RoomTypeTranslations.Where(l => l.LangCode == lang), page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }

        var res = new PagedResponse<RoomTypeDto>()
        {
            Total = result.Total,
            Page = result.Page,
            PageSize = result.PageSize,
            Data = RoomTypeDto.ToList(result.Data)
        };

        return Ok(res);
    }


    [HttpGet("search", Order = 0814)]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm, [FromQuery] string? name, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.RoomTypes.GenericSearchByText<RoomTypeTranslation>(t => t.Name.Contains(name), null, page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
        {
            Expression<Func<RoomType, bool>> filter = f => f.CodeNumber.Contains(searchTerm) || f.RoomTypeTranslations.Any(t => t.Name.Contains(searchTerm));
            Expression<Func<RoomType, object>> include = i => i.RoomTypeTranslations.Where(l => l.LangCode == lang);

            return Ok(await Data.RoomTypes.GenericSearchByText(filter, include, page, pageSize));
        }
        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] RoomTypeDto model)
    {
        Response response;

        RoomType entity = model;

        response = await Data.RoomTypes.GenericUpdate(entity, p1 => p1.IsDeleted, p2 => p2.CreateOn!);

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }



    [HttpPut("edit-translations", Order = 0822)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<RoomTypeTranslation> translations)
    {
        Response response;

        response = await Data.RoomTypes.GenericUpdate(translations);

        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }



    [HttpPut("deactivate", Order = 0825)]
    public async Task<IActionResult> EditSingleProp([FromQuery] int? id, [FromQuery] string status)
    {
        if (!id.HasValue)
        {
            return BadRequest(new Response(false, "id field is requerd"));
        }
        bool isDeleted;
        if (status == "active")
            isDeleted = false;
        else if (status == "inactive")
            isDeleted = true;
        else
        {
            return BadRequest(new Response(false, "The status field in this context allows the values 'active' or 'inactive' "));
        }
        RoomType entity = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.RoomTypes.GenericUpdateSinglePropertyById(id.Value, entity, p => p.IsDeleted));
    }

    // ============================= delete ============================= 

    [HttpDelete("delete-translat", Order = 0830)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] int? parentId, [FromQuery] params int[] translteId)
    {
        // var res = new Response();
        Expression<Func<RoomTypeTranslation, bool>> expression;

        if (parentId.HasValue)
            expression = t => t.RoomTypeId.Equals(parentId);
        else
            expression = t => translteId.Contains(t.Id);

        var res = await Data.RoomTypes.GenericDelete(expression, translteId);

        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    [HttpDelete("delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        //var res = new Response();
        //res = await Data.RoomTypes.GenericDelete<RoomTypeTranslation>(t => t.RoomTypeId.Equals(id));
        var res = await Data.RoomTypes.GenericDelete<RoomType>(t => t.Id.Equals(id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class