using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
namespace DomainModel.Models.Dtos;

public class ClinicDto
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public string? Photo { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? Reason { get; set; }

    public string? Phone { get; set; }

    public short? WorkingHours { get; set; }

    public bool Appearance { get; set; }

    public int HospitalId { get; set; }

    public int BuildId { get; set; }

    public int FloorId { get; set; }

    public int RoomId { get; set; }

    public int SpecialtyId { get; set; }
    public ICollection<ClinicTranslation> ClinicTranslations { get; set; } = null!;


    public static implicit operator ClinicDto(Clinic entity)
    {
        if (entity == null)
            return null!;

        return new ClinicDto
        {
            Id = entity.Id,
            CodeNumber = entity.CodeNumber,
            Photo = entity.Photo,
            Appearance = entity.Appearance,
            IsActive = entity.IsActive,
            IsDeleted = entity.IsDeleted,
            Phone = entity.Phone,
            Reason = entity.Reason,
            WorkingHours = entity.WorkingHours,
            HospitalId = entity.HospitalId,
            BuildId = entity.BuildId,
            FloorId = entity.FloorId,
            RoomId = entity.RoomId,
            SpecialtyId = entity.SpecialtyId,
            ClinicTranslations = entity.ClinicTranslations,
        };
    }

    public static implicit operator Clinic(ClinicDto dto)
    {
        var listTranc = new List<ClinicTranslation>();
        Clinic hospital;
        if (dto.ClinicTranslations != null)
            foreach (var item in dto.ClinicTranslations)
            {
                item.ClinicId = dto.Id;
                listTranc.Add(item);
            }

        hospital = new Clinic
        {
            Id = dto.Id,
            CodeNumber = dto.CodeNumber ?? string.Empty,
            Photo = dto.Photo,
            HospitalId = dto.HospitalId,
            BuildId = dto.BuildId,
            FloorId = dto.FloorId,
            RoomId = dto.RoomId,
            IsDeleted = dto.IsDeleted,
            Reason= dto.Reason,
            SpecialtyId = dto.SpecialtyId,
            WorkingHours = dto.WorkingHours,
            Phone   = dto.Phone,
            Appearance = dto.Appearance,
            IsActive = dto.IsActive,
            ClinicTranslations = listTranc,
        };

        return hospital;
    }

    public static List<ClinicDto> ToList(IEnumerable<Clinic> hospitals)
    {
        var listDto = new List<ClinicDto>(hospitals.Count());
        foreach (var entity in hospitals)
        {
            var dto = (ClinicDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}
