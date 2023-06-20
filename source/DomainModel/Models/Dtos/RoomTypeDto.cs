using DomainModel.Entities.TranslationModels;
using DomainModel.Entities;

namespace DomainModel.Models.Dtos;

public class RoomTypeDto
{
    public int Id { get; set; }

    public string? CodeNumber { get; set; }

    public DateTime? CreateOn { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<RoomTypeTranslation> RoomTypeTranslations { get; set; } = null!;

    public static implicit operator RoomTypeDto(RoomType arg)
    {
        if (arg == null)
            return null!;

        return new RoomTypeDto
        {
            Id = arg.Id,
            CodeNumber = arg.CodeNumber,
            CreateOn = arg.CreateOn,
            IsDeleted = arg.IsDeleted,
            RoomTypeTranslations = arg.RoomTypeTranslations,
        };
    }

    public static implicit operator RoomType(RoomTypeDto arg)
    {
        var listTranc = new List<RoomTypeTranslation>();
        RoomType hospital;
        if (arg.RoomTypeTranslations != null)
            foreach (var item in arg.RoomTypeTranslations)
            {
                item.RoomTypeId = arg.Id;
                listTranc.Add(item);
            }

        hospital = new RoomType
        {
            Id = arg.Id,
            CodeNumber = arg.CodeNumber ?? string.Empty,
            //CreateOn = arg.CreateOn,
            IsDeleted = arg.IsDeleted,
            RoomTypeTranslations = listTranc,
        };

        return hospital;
    }

    public static List<RoomTypeDto> ToList(IEnumerable<RoomType> enList)
    {
        var listDto = new List<RoomTypeDto>(enList.Count());
        foreach (var entity in enList)
        {
            var dto = (RoomTypeDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}
