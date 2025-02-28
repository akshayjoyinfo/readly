using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Readly.Application.Common.Interfaces;
using Readly.Domain.Entities.UserAccess;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Readly.Application.UseCases.UserAccess.RegisterUser;

public record RegisterUserCommand : IRequest<UserDto>
{
    public string Email { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
}

public class RegisterUserCommandHandler(
    IReadlyDbContext context,
    ILogger<RegisterUserCommandHandler> logger,
    IPasswordHasher<User> passwordHasher)
    : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogDebug("RegisterUserCommandHandler.Handle");

        await EnsureUserDoesNotExistAsync(request, cancellationToken);
      
        var userEntity = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = passwordHasher.HashPassword(null!, request.Password),
            UserRoles = new List<UserRole> { new() { RoleId = 2 } }
        };

        await context.Users.AddAsync(userEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new UserDto(userEntity.Id, userEntity.UserName, userEntity.Email);
    }

    private async Task EnsureUserDoesNotExistAsync (RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
        {
            throw new ValidationException("User Email is already registered", new List<ValidationFailure>
            {
                new("Email", "User with the same email already exists")
            });
        }
    }
}
