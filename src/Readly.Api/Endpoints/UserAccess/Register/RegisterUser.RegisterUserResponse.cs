namespace Readly.Api.Endpoints.UserAccess.Register;

public class RegisterUserResponse(long userId, string userName, string email)
{
    public long UserId { get; set; } = userId;
    public string UserName { get; set; } = userName;
    public string Email { get; set; } = email;
}
