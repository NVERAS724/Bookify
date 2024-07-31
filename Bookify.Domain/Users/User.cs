using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;


namespace Bookify.Domain.Users
{
    public sealed class User(Guid id, FirstName firstName, LastName lastname, Email email) : Entity(id)
    {
        public FirstName FirstName { get; private set; } = firstName;
        public LastName Lastname { get; private set; } = lastname;
        public Email Email { get; private set; } = email;
        public static User Create(FirstName firstName, LastName lastName, Email email)
        {
            User user = new(Guid.NewGuid(), firstName, lastName, email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
