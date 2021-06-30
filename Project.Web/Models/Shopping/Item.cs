using Project.Domain.Products;
using Project.Web.Models.Product;

namespace Project.Web.Models.Shopping
{
    public class ItemModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual ProductModel Product{ get; set; }
        public virtual ShoppingListModel Cart{ get; set; }
    }
}
