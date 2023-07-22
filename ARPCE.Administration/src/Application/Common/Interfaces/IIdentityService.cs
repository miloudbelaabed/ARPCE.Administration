using ARPCE.Administration.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace ARPCE.Administration.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);
    Task<List<ARPCEUser>> GetAll();

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
}
