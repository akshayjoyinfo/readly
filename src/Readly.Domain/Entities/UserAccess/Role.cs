namespace Readly.Domain.Entities.UserAccess;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = null!; // Many-to-many with Users
}
