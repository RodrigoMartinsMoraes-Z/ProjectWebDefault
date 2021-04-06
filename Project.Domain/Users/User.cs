using Project.Domain.Peoples;

namespace Project.Domain.Users
{
    public class User
    {
        public User()
        {
            Person = new Person();
        }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public virtual Person Person { get; set; }
    }
}
}
