using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.UnitTests.Users
{
    internal static class UserData
    {
        public static readonly string FirstName = "First";
        public static readonly string LastName = "Last";
        public static readonly Email Email = new("test@test.com");
    }
}
