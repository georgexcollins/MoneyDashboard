using MoneyDashboard.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoneyDashboard.Tests
{
    public class StorageTests
    {
        [Fact]
        public void UserRegistration_is_saved()
        {
            var store = new UserRegistrationStore();
            var reg = new UserRegistration("me@gmail.com", "password");

            store.Save(reg);

            var stored = store.Users.FirstOrDefault(x => x.Id == reg.Id);

            Assert.NotNull(stored);
            Assert.Equal("me@gmail.com", reg.Email);
        }

        [Fact]
        public void Password_is_saved()
        {
            var store = new UserRegistrationStore();
            var reg = new UserRegistration("me@gmail.com", "password");

            store.Save(reg);

            var stored = store.Users.FirstOrDefault(x => x.Id == reg.Id);

            Assert.NotNull(stored);
            Assert.True(stored.PasswordMatches("password"));
        }


        [Fact]
        public void Password_is_hashed()
        {
            var store = new UserRegistrationStore();
            var reg = new UserRegistration("me@gmail.com", "password");

            store.Save(reg);

            var stored = store.Users.FirstOrDefault(x => x.Id == reg.Id);

            Assert.NotNull(stored);
            Assert.NotEqual("password", stored.Password);
        }
    }
}
