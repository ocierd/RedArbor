
using System.Reflection;
using Microsoft.Extensions.Logging;
using RedArbor.Application.Common.Security;

namespace Redarbor.Application.Common.Behaviours;

/// <summary>
/// Authorization behavior for MediatR pipeline
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <param name="identityService"></param>
/// <param name="user"></param>
/// <param name="logger"></param>
public class AuthorizationBehaviour<TRequest, TResponse>(
    IIdentityService identityService,
    IUser user,
    ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Identity service for authorization checks
    /// </summary>
    private readonly IIdentityService _identityService = identityService;

    /// <summary>
    /// Authenticated user information
    /// </summary>
    private readonly IUser _user = user;

    /// <summary>
    /// Logger instance
    /// </summary>
    private readonly ILogger<AuthorizationBehaviour<TRequest, TResponse>> _logger = logger;

    /// <summary>
    /// Handle method for authorization
    /// </summary>
    /// <param name="request"> Request to be authorized </param>
    /// <param name="next"> Next handler in the pipeline </param>
    /// <param name="cancellationToken"> Cancellation token </param>
    /// <returns> Response from the next handler in the pipeline </returns>
    /// <exception cref="UnauthorizedException"> Exception thrown when user is not authenticated and it's required </exception>
    /// <exception cref="ForbiddenAccessException">Forbidden thrown when user is authenticated but lacks required permissions or the policy is not met</exception>
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
                    if (policy == null) continue;

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