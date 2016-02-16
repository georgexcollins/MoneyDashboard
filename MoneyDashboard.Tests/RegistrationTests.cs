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
    }
}
