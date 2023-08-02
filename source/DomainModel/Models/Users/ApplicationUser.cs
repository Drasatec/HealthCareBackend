using System.ComponentModel.DataAnnotations;

namespace DomainModel.Models.Users;

public class ApplicationUser
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;
}
