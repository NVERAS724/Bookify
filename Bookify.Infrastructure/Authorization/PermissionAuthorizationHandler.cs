﻿using Bookify.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authorization
{
    internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceProvider _serviceProvider;

        public PermissionAuthorizationHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity is not { IsAuthenticated: true})
                return;

            using var scope = _serviceProvider.CreateScope();

            var authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

            var identityId = context.User.GetIdentityId();

            //TODO: Introduce caching to avoid calling the database every time
            HashSet<string> permissions = await authorizationService.GetPermissionsForUserAsync(identityId);

            if (permissions.Contains(requirement.Permissions))
                context.Succeed(requirement);


        }
    }
}
