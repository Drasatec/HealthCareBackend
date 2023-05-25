using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models.Hospitals;
public  class HospitalDto
{

    public int HospitalId { get; set; }
    public string? Photo { get; set; }
    public string? CodeNumber { get; set; }
    public ICollection<HospitalsContactDatum> HospitalsContactData { get; set; } = null!;
    public ICollection<HospitalTranslation> HospitalTrasnlations { get; set; } = null!;

    public static implicit operator HospitalDto(Hospital entity)
    {
        return new HospitalDto
        {
            HospitalId = entity.Id,
            CodeNumber = entity.CodeNumber,
            Photo = entity.Photo,
            HospitalsContactData = entity.HospitalsContactData,
            HospitalTrasnlations = entity.HospitalTranslations
        };
    }

    public static implicit operator Hospital(HospitalDto entity)
    {
        return new Hospital
        {
            Id = entity.HospitalId,
            CodeNumber = entity.CodeNumber,
            Photo = entity.Photo,
            HospitalsContactData = entity.HospitalsContactData,
            HospitalTranslations = entity.HospitalTrasnlations
        };
    }

    public static IEnumerable<HospitalDto> ToList(IEnumerable<Hospital> hospitals)
    {
        var listDto = new List<HospitalDto>();
        foreach (var entity in hospitals)
        {
            var dto = (HospitalDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }
}
