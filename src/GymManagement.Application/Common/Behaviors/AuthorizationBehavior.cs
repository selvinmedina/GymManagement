using ErrorOr;
using GymManagement.Application.Common.Authorization;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace GymManagement.Application.Common.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse>(ICurrentUserProvider _currentUserProvider)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType()
                .GetCustomAttributes<AuthorizeAttribute>()
                .ToList();

            if (authorizeAttributes.Count == 0)
            {
                return next();
            }

            var requiredPermissions = authorizeAttributes
                .SelectMany(
                authorizeAttributes => authorizeAttributes?.Permissions?.Split(",") ?? []
                ).ToList();

            var currentUser = _currentUserProvider.GetCurrentUser();

            if (requiredPermissions.Except(currentUser.Permissions).Any())
            {
                var unauthorizedError = (TResponse)(dynamic)Error.Unauthorized(description: "You are not authorized to perform this action");
                return Task.FromResult(unauthorizedError);
            }

            var requiredRoles = authorizeAttributes
                .SelectMany(
                authorizeAttributes => authorizeAttributes?.Roles?.Split(",") ?? []
                ).ToList();

            if (requiredRoles.Except(currentUser.Roles).Any())
            {
                var unauthorizedError = (TResponse)(dynamic)Error.Unauthorized(description: "You are not authorized to perform this action");
                return Task.FromResult(unauthorizedError);
            }

            return next();
        }
    }
}
