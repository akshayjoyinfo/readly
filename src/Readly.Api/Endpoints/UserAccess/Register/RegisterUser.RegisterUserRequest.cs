using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Readly.Api.Endpoints.UserAccess.Register;

public class RegisterUserRequest
{
    // Const fields, metadata
    public const string Route = "/identity";

    public static Action<EndpointSummary<RegisterUserRequest>> GetSwaggerMetadata()
    {
        return s =>
        {
            s.Description = "Registered User";
            s.Summary = "Registered User";
            s.ExampleRequest = new RegisterUserRequest()
            {
                UserName = "John Doe", Email = "johndoe@gmail.com", Password = "password"
            };
            s.Responses[200] = "User registered successfully.";
            s.Responses[400] = "Invalid request data.";
        };
    }

    [Required] public string UserName { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}
