using Project.Domain.People;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Project.Domain.Users
{
    public class User
    {
        private string _pass;    

        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get => _pass; set => _pass = EncryptPassword(value); }
        public string Token { get; set; }

        public virtual Person Person { get; }

        private string EncryptPassword(string value)
        {
            byte[] salt = Encoding.UTF8.GetBytes(Login);
            byte[] passByte = Encoding.UTF8.GetBytes(value);
            byte[] sha256 = new SHA256Managed().ComputeHash(passByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}

