﻿using DomainModel.Entities.DoctorEntities;
using DomainModel.Entities.TranslationModels;
namespace DomainModel.Models.Doctors;

public class DoctorDto
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public byte? Gender { get; set; }

    public string? Photo { get; set; }

    public byte? WorkingHours { get; set; }

    public byte? DocStatus { get; set; }

    public string? Reason { get; set; }

    public bool IsAppearanceOnSite { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberAppearance { get; set; }

    public bool VisitPriceAppearance { get; set; }

    public bool IsDeleted { get; set; }

    public short? DoctorsDegreeId { get; set; }

    public int? NationalityId { get; set; }

    public ICollection<DoctorWorkPeriod>? DoctorWorkPeriods = new List<DoctorWorkPeriod>();


    public ICollection<DoctorTranslation> DoctorTranslations { get; set; } = null!;
    //public ICollection<SpecialtiesDoctor> SpecialtiesDoctors { get; set; } = new List<SpecialtiesDoctor>();
    //public ICollection<DoctorWorkPeriodDto> DoctorWorkPeriods { get; set; } = new List<DoctorWorkPeriodDto>();
    public ICollection<PeriodWorkDoctorClinicDto>? PeriodWorkDoctorClinics;


    public static implicit operator DoctorDto(Doctor arg)
    {
        if (arg == null)
            return null!;

        return new DoctorDto
        {
            Id = arg.Id,
            CodeNumber = arg.CodeNumber,
            Photo = arg.Photo,
            WorkingHours = arg.WorkingHours,
            DocStatus = arg.DocStatus,
            Reason = arg.Reason,
            DoctorsDegreeId = arg.DoctorsDegreeId,
            PhoneNumber = arg.PhoneNumber,
            Gender  = arg.Gender,
            PhoneNumberAppearance = arg.PhoneNumberAppearance,
            VisitPriceAppearance = arg.VisitPriceAppearance,
            IsDeleted = arg.IsDeleted,
            IsAppearanceOnSite  = arg.IsAppearanceOnSite,
            NationalityId = arg.NationalityId,
            DoctorTranslations = arg.DoctorTranslations,
            DoctorWorkPeriods = arg.DoctorWorkPeriods

        };
    }

    public static implicit operator Doctor(DoctorDto arg)
    {
        var listTranc = new List<DoctorTranslation>();
        Doctor doctor;
        if (arg.DoctorTranslations != null)
            foreach (var item in arg.DoctorTranslations)
            {
                item.DoctorId = arg.Id;
                listTranc.Add(item);
            }

        doctor = new Doctor
        {
            Id = arg.Id,
            CodeNumber = arg.CodeNumber ?? string.Empty,
            Photo = arg.Photo,
            WorkingHours = arg.WorkingHours,
            DocStatus = arg.DocStatus,
            Reason = arg.Reason,
            DoctorsDegreeId = arg.DoctorsDegreeId,
            PhoneNumber = arg.PhoneNumber,
            Gender = arg.Gender,
            PhoneNumberAppearance = arg.PhoneNumberAppearance,
            VisitPriceAppearance = arg.VisitPriceAppearance,
            //IsDeleted = arg.IsDeleted,
            IsAppearanceOnSite = arg.IsAppearanceOnSite,
            NationalityId = arg.NationalityId,
            DoctorTranslations = listTranc
        };

        return doctor;
    }

    public static List<DoctorDto> ToList(IEnumerable<Doctor> entities)
    {
        var listDto = new List<DoctorDto>(entities.Count());
        foreach (var entity in entities)
        {
            var dto = (DoctorDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}
