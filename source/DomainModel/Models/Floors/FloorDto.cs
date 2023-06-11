using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Models.Floors;

public class FloorDto
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public string? Photo { get; set; }

    public DateTime? CreateOn { get; set; }

    public bool IsDeleted { get; set; }

    public int HospitalId { get; set; }

    public int BuildId { get; set; }

    public virtual ICollection<FloorTranslation>? FloorTranslations { get; set; } = null!;



    public static implicit operator FloorDto(HosFloor entity)
    {
        if (entity == null)
            return null!;

        return new FloorDto
        {
            Id = entity.Id,
            CodeNumber = entity.CodeNumber,
            IsDeleted = entity.IsDeleted,
            Photo = entity.Photo,
            HospitalId = entity.HospitalId,
            BuildId = entity.BuildId,
            CreateOn = entity.CreateOn,
            FloorTranslations = entity.FloorTranslations
        };
    }

    public static implicit operator HosFloor(FloorDto dto)
    {
        var listTranc = new List<FloorTranslation>();
        HosFloor hospital;
        if (dto.FloorTranslations != null)
            foreach (var item in dto.FloorTranslations)
            {
                item.FloorId = dto.Id;
                listTranc.Add(item);
            }

        hospital = new HosFloor
        {
            Id = dto.Id,
            CodeNumber = dto.CodeNumber ?? string.Empty,
            Photo = dto.Photo,
            HospitalId = dto.HospitalId,
            BuildId = dto.BuildId,
            FloorTranslations = listTranc
        };

        return hospital;
    }

    public static List<FloorDto> ToList(IEnumerable<HosFloor> hospitals)
    {
        var listDto = new List<FloorDto>(hospitals.Count());
        foreach (var entity in hospitals)
        {
            var dto = (FloorDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}
