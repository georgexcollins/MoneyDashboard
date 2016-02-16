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
            _store = store;
        }

        public void Register(string email, string password)
        {
            var newReg =  new UserRegistration(email, password);
            _store.Save(newReg);
            return;
        }
    }
}
