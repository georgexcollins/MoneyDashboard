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
        public UserRegistrationStore() : base("UserRegistrationStore") { }

        public IDbSet<UserRegistration> Users { get; set; }

        public UserRegistration Load(string email)
        {
            return Users.FirstOrDefault(x => x.Email == email);
        }

        public void Save(UserRegistration newReg)
        {
            Users.Add(newReg);
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var reg = modelBuilder.Entity<UserRegistration>();
            reg.Property(x => x.Email).HasMaxLength(200).IsRequired();
            reg.Property(x => x.Password).HasMaxLength(60).IsRequired();
        }
    }
}
