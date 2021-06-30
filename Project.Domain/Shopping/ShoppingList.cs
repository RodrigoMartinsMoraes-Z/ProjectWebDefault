using Project.Domain.Users;

using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Shopping
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual List<Item> Items { get; set; }
        public virtual User User{ get; set; }
    }
}
