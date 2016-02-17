using MoneyDashboard.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoneyDashboard.Tests
{
    public class StorageTests : IDisposable
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
            Assert.False(Encoding.UTF8.GetBytes("password").SequenceEqual(stored.Password));
        }

        [Fact]
        public void End_to_end_success()
        {
            var store = new UserRegistrationStore();
            var svc = new UserRegistrationService(store);

            svc.Register("12345@gmail.com", "password");

            var id = svc.Login("12345@gmail.com", "password");
            Assert.NotEqual(Guid.Empty, id);

            var user = store.Users.FirstOrDefault(x => x.Id == id);
            Assert.Equal("12345@gmail.com", user.Email);
        }

        [Fact]
        public void End_to_end_failed_login()
        {
            var store = new UserRegistrationStore();
            var svc = new UserRegistrationService(store);

            svc.Register("me@gmail.com", "password");

            Assert.Equal(Guid.Empty, svc.Login("me@gmail.com", "wrong"));
            Assert.Equal(Guid.Empty, svc.Login("other@gmail.com", "password"));
        }

        [Fact]
        public void Registration_fails_with_duplicate_emails()
        {
            var store = new UserRegistrationStore();
            var svc = new UserRegistrationService(store);

            svc.Register("me@gmail.com", "password");

            Assert.Throws<DbUpdateException>(() => svc.Register("me@gmail.com", "other"));
        }

        public void Dispose()
        {
            // Clean up test data
            var store = new UserRegistrationStore().Database.ExecuteSqlCommand("delete from UserRegistrations");
        }
    }
}
