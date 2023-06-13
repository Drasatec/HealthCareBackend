using DomainModel.Entities.TranslationModels;
using DomainModel.Entities;
namespace DomainModel.Models.MedicalSpecialteis;

public class MedicalSpecialtyDto
{

    public int Id { get; set; }

    public string? Photo { get; set; }

    public string? CodeNumber { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public bool? Appearance { get; set; }

    public string? Reason { get; set; }

    public int HospitalId { get; set; }

    public ICollection<MedicalSpecialtyTranslation> MedicalSpecialtyTranslations { get; set; } = null!;



    public static implicit operator MedicalSpecialtyDto(MedicalSpecialty entity)
    {
        if (entity == null)
            return null!;

        return new MedicalSpecialtyDto
        {
            Id = entity.Id,
            CodeNumber = entity.CodeNumber,
            IsDeleted = entity.IsDeleted,
            Photo = entity.Photo,
            Appearance = entity.Appearance,
            IsActive = entity.IsActive,
            Reason = entity.Reason,
            MedicalSpecialtyTranslations = entity.MedicalSpecialtyTranslations
        };
    }

    public static implicit operator MedicalSpecialty(MedicalSpecialtyDto dto)
    {
        var listTranc = new List<MedicalSpecialtyTranslation>();
        MedicalSpecialty hospital;
        if (dto.MedicalSpecialtyTranslations != null)
            foreach (var item in dto.MedicalSpecialtyTranslations)
            {
                item.MedicalSpecialtyId = dto.Id;
                listTranc.Add(item);
            }

        hospital = new MedicalSpecialty
        {
            Id = dto.Id,
            CodeNumber = dto.CodeNumber ?? string.Empty,
            Photo = dto.Photo,
            Appearance = dto.Appearance,
            IsActive = dto.IsActive,
            Reason = dto.Reason,
            MedicalSpecialtyTranslations = listTranc
        };

        return hospital;
    }

    public static List<MedicalSpecialtyDto> ToList(IEnumerable<MedicalSpecialty> hospitals)
    {
        var listDto = new List<MedicalSpecialtyDto>(hospitals.Count());
        foreach (var entity in hospitals)
        {
            var dto = (MedicalSpecialtyDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }
}
