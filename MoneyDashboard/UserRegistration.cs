using System;
using System.Linq;
using CryptSharp.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyDashboard
{
    public class UserRegistration
    {
        protected UserRegistration() { }

        public UserRegistration(string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = Hash(password);
        }

        public Guid Id { get; protected set; }
        [Index(IsUnique = true)]
        public string Email { get; protected set; }
        public byte[] Password { get; protected set; }

        public bool PasswordMatches(string pass)
        {
            return Password.SequenceEqual(Hash(pass));
        }

        private byte[] Hash(string password)
        {
            var input = System.Text.Encoding.UTF8.GetBytes(password);
            return BlowfishCipher.BCrypt(input, Id.ToByteArray(), 10);
        }
    }
}