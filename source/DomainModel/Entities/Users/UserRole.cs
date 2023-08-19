namespace DomainModel.Entities.Users;
public partial class UserRole
{
    public long Id { get; set; }

    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public DateTime? CreateOn { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

