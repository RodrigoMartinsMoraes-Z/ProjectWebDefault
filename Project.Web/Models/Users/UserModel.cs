using Project.Domain.Users;
using Project.Web.Models.People;

namespace Project.Web.Models.Users
{
    public class UserModel
    {
        public UserModel()
        {
            Person = new PersonModel();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }

        public virtual PersonModel Person { get; set; }
    }
}
