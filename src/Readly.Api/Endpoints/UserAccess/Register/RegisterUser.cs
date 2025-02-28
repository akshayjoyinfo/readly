using Microsoft.AspNetCore.Http.HttpResults;
using Readly.Application.UseCases.UserAccess.RegisterUser;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Readly.Api.Endpoints.UserAccess.Register;

public class RegisterUser(ILogger<RegisterUser> logger, IMediator mediator) :
    Endpoint<RegisterUserRequest, RegisterUserResponse>
{
    public override void Configure()
    {
        Post(RegisterUserRequest.Route);
        AllowAnonymous();
        Summary(SwaggerMetadataHelper.GetSwaggerMetadata<RegisterUserRequest>(
            "Registered User",
            "Registers a new user."
        ));
    }

    public override async Task HandleAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("RegisterUser.HandleAsync");
        var result =
            await mediator.Send(
                new RegisterUserCommand()
                {
                    Email = request.Email, UserName = request.UserName, Password = request.Password
                }, cancellationToken);
        
        if(result.UserId !=0)
            await SendAsync(new RegisterUserResponse(result.UserId, result.Email, result.UserName),
                statusCode: StatusCodes.Status201Created,
            cancellation: cancellationToken);
        else
        {
            AddError(r => r.Email, "Unable to register user");
            ThrowIfAnyErrors();
        }
    }
}
