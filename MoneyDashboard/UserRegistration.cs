using System;

namespace MoneyDashboard
{
    public class UserRegistration
    {
        private string email;
        private string password;

        protected UserRegistration() { }

        public UserRegistration(string email, string password)
        {
            this.email = email;
            this.password = password;
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
        public string Email { get { return email; } }
    }
}