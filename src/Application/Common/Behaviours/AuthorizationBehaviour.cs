
using System.Reflection;
using Microsoft.Extensions.Logging;
using RedArbor.Application.Common.Security;

namespace Redarbor.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly IIdentityService _identityService;

    private readonly IUser _user;

    private readonly ILogger<AuthorizationBehaviour<TRequest, TResponse>> _logger;


    public AuthorizationBehaviour(IIdentityService identityService, IUser user, ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger)
    {
        _identityService = identityService;
        _user = user;
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Authorization logic can be added here in the future

        try
        {

            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (string.IsNullOrWhiteSpace(_user.UserId))
                {
                    throw new UnauthorizedException();
                }
            }

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).ToList();

            if (authorizeAttributesWithRoles.Count > 0)
            {
                var authorized = false;


                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles?.Split(',')))
                {
                    if (roles == null) continue;

                    foreach (var role in roles)
                    {
                        var isInRole = _user.Roles?.Any(x => role == x) ?? false;
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }






                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }

            }

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_user.UserId, policy);

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(ex, "Error during authorization check for {RequestType}", typeof(TRequest).Name);
            }
            throw;
        }


        return await next(cancellationToken);
    }
}