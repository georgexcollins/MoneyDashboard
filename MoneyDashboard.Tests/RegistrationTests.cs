using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace MoneyDashboard.Tests
{
    public class RegistrationTests
    {
        [Fact]
        public void Stores_new_UserRegistration()
        {
            var store = new Mock<IUserRegistrationStore>();
            store.Setup(x => x.Save(It.IsAny<UserRegistration>()));
            var svc = new UserRegistrationService(store.Object);

            svc.Register("me@gmail.com", "password");

            store.VerifyAll();
        }

        [Theory]
        [InlineData("me@gmail.com", "password")]
        public void Stores_registration_with_email(string email, string password)
        {
            var store = new Mock<IUserRegistrationStore>();
            store.Setup(x => x.Save(It.Is<UserRegistration>(r => r.Email == email)));
            var svc = new UserRegistrationService(store.Object);

            svc.Register(email, password);

            store.VerifyAll();
        }

        [Fact]
        public void Fails_with_empty_inputs()
        {
            var store = new Mock<IUserRegistrationStore>();
            var svc = new UserRegistrationService(store.Object);

            Assert.Throws<ArgumentNullException>(() => svc.Register(null, "password"));
            Assert.Throws<ArgumentNullException>(() => svc.Register("email", null));
        }

        [Fact]
        public void Can_check_password()
        {
            var reg = new UserRegistration("me@gmail.com", "password");

            Assert.True(reg.PasswordMatches("password"));
            Assert.False(reg.PasswordMatches("wrong"));
        }

        [Fact]
        public void Service_can_verify_credentials()
        {
            var reg = new UserRegistration("me@gmail.com", "password");
            var store = new Mock<IUserRegistrationStore>();
            store.Setup(x => x.Load("me@gmail.com")).Returns(reg);
            var svc = new UserRegistrationService(store.Object);

            var result = svc.Login("me@gmail.com", "password");

            store.VerifyAll();
            Assert.Equal(reg.Id, result);
        }

        [Fact]
        public void Login_fails_with_wrong_password()
        {
            var reg = new UserRegistration("me@gmail.com", "password");
            var store = new Mock<IUserRegistrationStore>();
            store.Setup(x => x.Load("me@gmail.com")).Returns(reg);
            var svc = new UserRegistrationService(store.Object);

            var result = svc.Login("me@gmail.com", "wrong");

            store.VerifyAll();
            Assert.NotEqual(reg.Id, result);
            Assert.Equal(new Guid(), result);
        }


        [Fact]
        public void Login_fails_with_wrong_email()
        {
            var reg = new UserRegistration("me@gmail.com", "password");
            var store = new Mock<IUserRegistrationStore>();
            store.Setup(x => x.Load("other@gmail.com"));
            var svc = new UserRegistrationService(store.Object);

            var result = svc.Login("other@gmail.com", "password");

            store.VerifyAll();
            Assert.NotEqual(reg.Id, result);
            Assert.Equal(new Guid(), result);
        }


        [Fact]
        public void Login_fails_with_empty_inputs()
        {
            var store = new Mock<IUserRegistrationStore>();
            var svc = new UserRegistrationService(store.Object);

            Assert.Throws<ArgumentNullException>(() => svc.Login("me@gmail.com", null));
            Assert.Throws<ArgumentNullException>(() => svc.Login(null, "password"));
        }
    }
}
