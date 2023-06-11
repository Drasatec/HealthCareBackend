using DomainModel.Entities.TranslationModels;
using DomainModel.Entities;
using DomainModel.Models.Floors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models.Rooms;

public class RoomDto
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public string? CodeNumber { get; set; }

    public string? Kind { get; set; }

    //public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int HospitalId { get; set; }

    public int BuildId { get; set; }

    public int FloorId { get; set; }

    public int RoomTypeId { get; set; }

    public virtual ICollection<RoomTranslation>? RoomTranslations { get; set; } = null!;



    public static implicit operator RoomDto(HosRoom entity)
    {
        if (entity == null)
            return null!;

        return new RoomDto
        {
            Id = entity.Id,
            CodeNumber = entity.CodeNumber,
            IsDeleted = entity.IsDeleted,
            Photo = entity.Photo,
            RoomTypeId = entity.RoomTypeId,
            //IsActive = entity.IsActive,
            Kind = entity.Kind,
            HospitalId = entity.HospitalId,
            BuildId = entity.BuildId,
            FloorId = entity.FloorId,
            RoomTranslations = entity.RoomTranslations
        };
    }

    public static implicit operator HosRoom(RoomDto dto)
    {
        var listTranc = new List<RoomTranslation>();
        HosRoom hospital;
        if (dto.RoomTranslations != null)
            foreach (var item in dto.RoomTranslations)
            {
                item.RoomId = dto.Id;
                listTranc.Add(item);
            }

        hospital = new HosRoom
        {
            Id = dto.Id,
            CodeNumber = dto.CodeNumber ?? string.Empty,
            Photo = dto.Photo,
            HospitalId = dto.HospitalId,
            BuildId = dto.BuildId,
            FloorId = dto.FloorId,
            RoomTypeId= dto.RoomTypeId,
            Kind = dto.Kind,
            RoomTranslations = listTranc

        };

        return hospital;
    }

    public static List<RoomDto> ToList(IEnumerable<HosRoom> hospitals)
    {
        var listDto = new List<RoomDto>(hospitals.Count());
        foreach (var entity in hospitals)
        {
            var dto = (RoomDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}
