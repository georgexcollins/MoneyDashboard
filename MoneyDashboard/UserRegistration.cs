using System;
using System.Linq;
using CryptSharp.Utility;

namespace MoneyDashboard
{
    public class UserRegistration
    {
        private string _email;
        private byte[] _password;

        protected UserRegistration() { }

        public UserRegistration(string email, string password)
        {
            Id = Guid.NewGuid();
            _email = email;
            _password = Hash(password);
        }

        public Guid Id { get; protected set; }
        public string Email { get { return _email; } }
        public byte[] Password { get { return _password; } }

        public bool PasswordMatches(string pass)
        {
            return _password.SequenceEqual(Hash(pass));
        }

        private byte[] Hash(string password)
        {
            var input = System.Text.Encoding.UTF8.GetBytes(password);
            return BlowfishCipher.BCrypt(input, Id.ToByteArray(), 10);
        }
    }
}