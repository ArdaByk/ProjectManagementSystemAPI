using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using PMS.Core.Security.Authorization;
using PMS.Core.Security.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProjectRoleProvider _projectRoleProvider;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, IProjectRoleProvider projectRoleProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _projectRoleProvider = projectRoleProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Guid? userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

        if (userId == null)
            throw new AuthorizationException("You are not authenticated.");

        var requiredRoles = request.RequiredProjectRoles;

        if (request.RequiredProjectRoles == null || !request.RequiredProjectRoles.Any())
            return await next();
        

        if (request.ProjectId == null)
            throw new AuthorizationException("ProjectId is required for role-based authorization.");

        var userRole = await _projectRoleProvider.GetUserOperationClaim(userId, request.ProjectId);

        if (userRole == null)
            throw new AuthorizationException("User is not a member of this project.");

        if (!requiredRoles.Contains(userRole))
            throw new AuthorizationException("You do not have permission to do this.");

        return await next();
    }
}
