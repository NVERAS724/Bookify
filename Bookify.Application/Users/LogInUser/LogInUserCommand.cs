using Bookify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Users.LogInUser
{
    public sealed record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
}
