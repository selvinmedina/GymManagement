using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Common.Models;
using Throw;

namespace GymManagement.Api.Services
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContectAccesor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
            _httpContectAccesor.HttpContext.ThrowIfNull();

            var idClaim = _httpContectAccesor.HttpContext.User.Claims
                .First(claim => claim.Type == "id");

            var permissionClaim = _httpContectAccesor.HttpContext.User.Claims
                .First(claim => claim.Type == "permissions");

            var userId = Guid.Parse(idClaim.Value);

            return new CurrentUser(
                userId, 
                Permissions: permissionClaim.Value.Split(",")
                );
        }
    }
}
