using Project.Web.Models.Peoples;

namespace Project.Web.Models.Users
{
    public class UserModel
    {
        public UserModel()
        {
            Person = new PersonModel();
        }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public virtual PersonModel Person { get; set; }
    }
}
