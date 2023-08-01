using System.ComponentModel.DataAnnotations;

namespace DomainModel.Models.Users;

public class TokenRequestModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
