using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Common.Models;
using System.Security.Claims;
using Throw;

namespace GymManagement.Api.Services
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContectAccesor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
            _httpContectAccesor.HttpContext.ThrowIfNull();

            var userId = GetClaimValues("id")
                .Select(Guid.Parse)
                .First();

            var permissions = GetClaimValues("permissions");

            var roles = GetClaimValues(ClaimTypes.Role);

            return new CurrentUser(
                userId, 
                Permissions: permissions,
                Roles: roles
                );
        }

        private IReadOnlyList<string> GetClaimValues(string claimType)
        {
            return _httpContectAccesor.HttpContext!.User.Claims
                .Where(claim => claim.Type == claimType)
                .Select(claim => claim.Value)
                .ToList();
        }
    }
}
