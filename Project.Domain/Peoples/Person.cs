using Project.Domain.Users;

using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Peoples
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }

        public virtual User User { get; set; }
    }
}
