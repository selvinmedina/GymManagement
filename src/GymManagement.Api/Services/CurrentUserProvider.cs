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

            var claim = _httpContectAccesor.HttpContext.User.Claims.First(claim => claim.Type == "id");

            var userId = Guid.Parse(claim.Value);

            return new CurrentUser(userId);
        }
    }
}
