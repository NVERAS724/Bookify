using Bookify.Domain.Users.Events;
using Bookify.Domain.Users;
using FluentAssertions;

namespace Bookify.Domain.UnitTests.Users
{
    public class UserTests : BaseTest
    {
        [Fact]
        public void Create_Should_Raise_UserCreatedDomainEvent()
        {
            //Act
            var user = User.Create(new FirstName(UserData.FirstName), new LastName(UserData.LastName), UserData.Email);

            //Assert
            var userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

            userCreatedDomainEvent.UserId.Should().Be(user.Id);
        }


        [Fact]
        public void Create_Should_AddRegisteredRoleToUser()
        {
            //Act
            var user = User.Create(new FirstName(UserData.FirstName), new LastName(UserData.LastName), UserData.Email);

            //Assert
            user.Roles.Should().Contain(Role.Registered);
        }
    }
}
