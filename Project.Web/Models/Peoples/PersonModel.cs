using Project.Web.Models.Users;

using System;

namespace Project.Web.Models.Peoples
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }

        public virtual UserModel User { get; set; }
    }
}