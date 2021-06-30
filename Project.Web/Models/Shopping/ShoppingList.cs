using Project.Domain.Users;
using Project.Web.Models.Users;

using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Web.Models.Shopping
{
    public class ShoppingListModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual List<ItemModel> Items { get; set; }
    }
}
