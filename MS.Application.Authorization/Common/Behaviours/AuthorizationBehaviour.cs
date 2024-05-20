using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Security;
using MS.Application.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Authorization.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse>(
        IUser user,
        IAuthorizationDbContext authorizationDbContext) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUser _user = user;
        private readonly IAuthorizationDbContext _authorizationDbContext = authorizationDbContext;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
            var authorized = false;
            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            //var isInRole = await _authorizationDbContext.UserRoles.Where(s => s.UserId.ToString() == _user.Id).ToListAsync();
                            //if (isInRole)
                            //{
                            //    authorized = true;
                            //    break;
                            //}
                        }
                    }

                    // Must be a member of at least one role in roles
                    //if (!authorized)
                    //{
                    //    throw new ForbiddenAccessException();
                    //}
                }

                // Policy-based authorization
                //var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                //if (authorizeAttributesWithPolicies.Any())
                //{
                //    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                //    {
                //        var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);

                //        if (!authorized)
                //        {
                //            throw new ForbiddenAccessException();
                //        }
                //    }
                //}
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
