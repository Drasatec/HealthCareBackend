using DomainModel.Contracts;
using DomainModel.Entities;
using System.Text.Json.Serialization;

namespace DomainModel.Models.Hospitals;
public class HospitalDto
{

    public int Id { get; set; }
    public string? Photo { get; set; }
    public string? CodeNumber { get; set; }
    public string? Email { get; set; }
    public string? WhatsAppNumber { get; set; }
    public bool IsDeleted { get; set; } = false;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<HospitalPhoneNumber>? PhoneNumbers { get; set; } = null!;
    public ICollection<HospitalTranslation>? HospitalTrasnlations { get; set; } = null!;

    //[JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
    //public Error? Error { get; set; } = null;

    public static implicit operator HospitalDto(Hospital entity)
    {
        if (entity == null)
            return null!;

        return new HospitalDto
        {
            Id = entity.Id,
            CodeNumber = entity.CodeNumber,
            IsDeleted = entity.IsDeleted,
            Email = entity.Email,
            WhatsAppNumber = entity.WhatsAppNumber,
            Photo = entity.Photo,
            PhoneNumbers = entity.HospitalPhoneNumbers,
            HospitalTrasnlations = entity.HospitalTranslations
        };
    }

    public static implicit operator Hospital(HospitalDto dto)
    {
        var listTranc = new List<HospitalTranslation>();
        Hospital hospital;
        if (dto.HospitalTrasnlations != null)
            foreach (var item in dto.HospitalTrasnlations)
            {
                item.HospitalId = dto.Id;
                item.Address = item.Address;
                item.Name = item.Name;
                listTranc.Add(item);
            }

        hospital = new Hospital
        {
            Id = dto.Id,
            CodeNumber = dto.CodeNumber ?? string.Empty,
            Photo = dto.Photo,
            Email = dto.Email,
            WhatsAppNumber = dto.WhatsAppNumber,
            HospitalTranslations = listTranc
        };

        if (dto.PhoneNumbers != null)
            hospital.HospitalPhoneNumbers = dto.PhoneNumbers;

        return hospital;
    }

    public static List<HospitalDto> ToList(IEnumerable<Hospital> hospitals)
    {
        var listDto = new List<HospitalDto>(hospitals.Count());
        foreach (var entity in hospitals)
        {
            var dto = (HospitalDto)entity;
            listDto.Add(dto);
        }
        return listDto;
        //yield return (HospitalDto)entity;
    }
}
