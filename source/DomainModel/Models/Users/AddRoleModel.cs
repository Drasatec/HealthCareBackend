using System.ComponentModel.DataAnnotations;

namespace DomainModel.Models.Users;

public class AddRoleModel
{
    public string UserId { get; set; }
    public string Role { get; set; }
}
