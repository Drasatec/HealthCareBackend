using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models.Hospitals;
using System.Text.Json.Serialization;

namespace DomainModel.Models.Buildings;

public class BuildingDto
{

    public int Id { get; set; }
    public string? Photo { get; set; }
    public string? CodeNumber { get; set; } 
    public bool IsDeleted { get; set; }
    public int HospitalId { get; set; }


    public ICollection<BuildingTranslation>? BuildingTranslation { get; set; } = null!;


    public static implicit operator BuildingDto(HosBuilding entity)
    {
        if (entity == null)
            return null!;

        return new BuildingDto
        {
            Id = entity.Id,
            CodeNumber = entity.CodeNumber,
            IsDeleted = entity.IsDeleted,
            Photo = entity.Photo,
            HospitalId = entity.HospitalId,
            BuildingTranslation = entity.BuildingTranslations
        };
    }

    public static implicit operator HosBuilding(BuildingDto dto)
    {
        var listTranc = new List<BuildingTranslation>();
        HosBuilding hospital;
        if (dto.BuildingTranslation != null)
            foreach (var item in dto.BuildingTranslation)
            {
                item.BuildeingId = dto.Id;
                listTranc.Add(item);
            }

        hospital = new HosBuilding
        {
            Id = dto.Id,
            CodeNumber = dto.CodeNumber ?? string.Empty,
            Photo = dto.Photo,
            HospitalId = dto.HospitalId,
            BuildingTranslations = listTranc
        };

        return hospital;
    }

    public static List<BuildingDto> ToList(IEnumerable<HosBuilding> hospitals)
    {
        var listDto = new List<BuildingDto>(hospitals.Count());
        foreach (var entity in hospitals)
        {
            var dto = (BuildingDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }
}
