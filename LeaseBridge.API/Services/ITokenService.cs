using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Identity;

namespace LeaseBridge.API.Services;

public interface ITokenService
{
    Task<string> CreateTokenAsync(IdentityUser user);
}