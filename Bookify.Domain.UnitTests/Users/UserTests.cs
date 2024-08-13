using Bookify.Domain.Users.Events;
using Bookify.Domain.Users;
using FluentAssertions;
using Bookify.Domain.UnitTests.Infrastructure;

namespace Bookify.Domain.UnitTests.Users
{
    public class UserTests : BaseTest
    {


        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            //Acc
            var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            //Assert
            user.FirstName.Should().Be(UserData.FirstName);
            user.LastName.Should().Be(UserData.LastName);
            user.Email.Should().Be(UserData.Email);
        }

        [Fact]
        public void Create_Should_Raise_UserCreatedDomainEvent()
        {
            //Act
            var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            //Assert
            var userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

            userCreatedDomainEvent.UserId.Should().Be(user.Id);
        }


        [Fact]
        public void Create_Should_AddRegisteredRoleToUser()
        {
            //Act
            var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            //Assert
            user.Roles.Should().Contain(Role.Registered);
        }
    }
}
