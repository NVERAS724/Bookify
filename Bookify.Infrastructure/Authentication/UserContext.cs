using Bookify.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authentication
{
    internal sealed class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

        public Guid UserId =>
            _contextAccessor
                .HttpContext?.User
                .GetUserId() ?? throw new ApplicationException("User context is unavailable");

        public string IdentityId =>
            _contextAccessor
                .HttpContext?.User
                .GetIdentityId() ?? throw new ApplicationException("User context is unavailable");
    }
}
