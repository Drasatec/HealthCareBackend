using DomainModel.Contracts;
using DomainModel.Entities;

namespace DomainModel.Models.Hospitals;
public class HospitalDto
{

    public int Id { get; set; }
    public string? Photo { get; set; }
    public string? CodeNumber { get; set; }
    public bool IsDeleted { get; set; } = false;
    //public Error? Error { get; set; } 
    public ICollection<HospitalsContactDatum>? HospitalsContactData { get; set; } = null!;
    public ICollection<HospitalTranslation>? HospitalTrasnlations { get; set; } = null!; 

    public static implicit operator HospitalDto(Hospital entity)
    {
        if (entity == null)
            return null!;
        return new HospitalDto
        {
            Id = entity.Id,
            CodeNumber = entity.CodeNumber,
            Photo = entity.Photo,
            HospitalsContactData = entity.HospitalsContactData,
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
            CodeNumber = dto.CodeNumber,
            Photo = dto.Photo,
            HospitalTranslations = listTranc
        };

        if (dto.HospitalsContactData != null)
            hospital.HospitalsContactData = dto.HospitalsContactData;

        return hospital;
    }

    public static IEnumerable<HospitalDto> ToList(IEnumerable<Hospital> hospitals)
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
