using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Users
{
    public sealed class User(Guid id, FirstName firstName, LastName lastname, Email email) : Entity(id)
    {
        public FirstName FirstName { get; private set; } = firstName;
        public LastName Lastname { get; private set; } = lastname;
        public Email Email { get; private set; } = email;
        public static User Create(FirstName firstName, LastName lastName, Email email)
        {
            return new(Guid.NewGuid(), firstName, lastName, email);
        }
    }
}
