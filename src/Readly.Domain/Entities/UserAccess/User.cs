namespace Readly.Domain.Entities.UserAccess;

public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = null!; // Many-to-many with Roles
}
