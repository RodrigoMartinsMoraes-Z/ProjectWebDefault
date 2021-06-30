using Project.Domain.Products;

namespace Project.Domain.Shopping
{
    public class Item
    {
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual Product Product{ get; set; }
        public virtual ShoppingList ShoppingList{ get; set; }
    }
}
