using System.Security.Claims;
using Readly.Domain.Entities.UserAccess;

namespace Readly.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
}
