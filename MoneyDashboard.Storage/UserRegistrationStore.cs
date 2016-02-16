using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDashboard.Storage
{
    public class UserRegistrationStore : DbContext, IUserRegistrationStore
    {
        public IDbSet<UserRegistration> Users { get; set; }

        public UserRegistration Load(string email)
        {
            throw new NotImplementedException();
        }

        public void Save(UserRegistration newReg)
        {
            Users.Add(newReg);
            SaveChanges();
        }
    }
}
