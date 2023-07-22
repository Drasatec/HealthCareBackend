using DomainModel.Entities.TranslationModels;
using DomainModel.Entities;
using DomainModel.Models.Dtos;

namespace DomainModel.Models.Doctors;

public class DoctorVisitPriceDto
{
    public int Id { get; set; }

    public string? DoctorName { get; set; }

    public string? PriceCategory { get; set; }

    public string? TypeVisit { get; set; }

    public string? PriceCurrency { get; set; }

    public int? Price { get; set; }



    public int? DoctorId { get; set; }

    public int? PriceCategoryId { get; set; }

    public int? TypeVisitId { get; set; }

   // public string? Specialty { get; set; }


    public static implicit operator DoctorVisitPriceDto(DoctorVisitPrice arg)
    {
        if (arg == null)
            return null!;

        return new DoctorVisitPriceDto
        {
            Id = arg.Id,
            Price = arg.Price,
            PriceCurrency = arg.PriceCurrency,
            TypeVisitId = arg.TypeVisitId,
            PriceCategoryId = arg.PriceCategoryId,
            DoctorId = arg.DoctorId,
        };
    }

    public static implicit operator DoctorVisitPrice(DoctorVisitPriceDto arg)
    {
        var enity = new DoctorVisitPrice
        {
            Id = arg.Id,
            Price = arg.Price,
            PriceCurrency = arg.PriceCurrency,
            TypeVisitId = arg.TypeVisitId,
            PriceCategoryId = arg.PriceCategoryId,
            DoctorId = arg.DoctorId,
        };

        return enity;
    }

    public static List<DoctorVisitPriceDto> ToList(IEnumerable<DoctorVisitPrice> enList)
    {
        var listDto = new List<DoctorVisitPriceDto>(enList.Count());
        foreach (var entity in enList)
        {
            var dto = (DoctorVisitPriceDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}
