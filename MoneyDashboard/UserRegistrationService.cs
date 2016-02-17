using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDashboard
{
    public class UserRegistrationService
    {
        private readonly IUserRegistrationStore _store;
        public UserRegistrationService(IUserRegistrationStore store)
        {
            if (store == null) throw new ArgumentNullException("store");
            _store = store;
        }

        public void Register(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password");

            var newReg =  new UserRegistration(email, password);
            _store.Save(newReg);
        }

        // Guid.Empty denotes failed login
        public Guid Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password");

            var user = _store.Load(email);
            return (user != null && user.PasswordMatches(password))
                ? user.Id
                : new Guid();
        }
    }
}
