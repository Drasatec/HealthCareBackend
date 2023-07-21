using DomainModel.Entities;

namespace DomainModel.Models.Clients;
public class HotClientDto
{
    public int Id { get; set; }

    public string? Photo { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    public string NameEn { get; set; } = string.Empty;

    public string NameOriginalLang { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? TelephoneNumber1 { get; set; }

    public string? TelephoneNumber2 { get; set; }

    public string? Email { get; set; }

    public string? WhatsAppNumber { get; set; }
    

    //public string? Username { get; set; }

    //public string? Password { get; set; }

    public byte? ClientStatus { get; set; }

    public string? Reason { get; set; }

    public static implicit operator HotClientDto(HosClient arg)
    {
        if (arg == null)
            return null!;

        return new HotClientDto
        {
            Id = arg.Id,
            CodeNumber = arg.CodeNumber,
            NameEn = arg.NameEn,
            NameOriginalLang = arg.NameOriginalLang,
            Address = arg.Address,
            Photo = arg.Photo,
            ClientStatus = arg.ClientStatus,
            Reason = arg.Reason,
            //Username = arg.Username,
            //Password = arg.Password,
            Email   = arg.Email,
            TelephoneNumber1    = arg.TelephoneNumber1,
            TelephoneNumber2 = arg.TelephoneNumber2,
            WhatsAppNumber = arg.WhatsAppNumber
        };
    }

    public static implicit operator HosClient(HotClientDto arg)
    {
        return  new HosClient
        {
            Id = arg.Id,
            CodeNumber = arg.CodeNumber,
            NameEn = arg.NameEn,
            NameOriginalLang = arg.NameOriginalLang,
            Address = arg.Address,
            Photo = arg.Photo,
            ClientStatus = 0,
            Reason = arg.Reason,
            //Username = arg.Username,
            //Password = arg.Password,
            Email = arg.Email,
            TelephoneNumber1 = arg.TelephoneNumber1,
            TelephoneNumber2 = arg.TelephoneNumber2,
            WhatsAppNumber = arg.WhatsAppNumber
        };
    }

    public static List<HotClientDto> ToList(IEnumerable<HosClient> hospitals)
    {
        var listDto = new List<HotClientDto>(hospitals.Count());
        foreach (var entity in hospitals)
        {
            var dto = (HotClientDto)entity;
            listDto.Add(dto);
        }
        return listDto;
    }

}
