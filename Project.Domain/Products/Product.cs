using System.Collections.Generic;

namespace Project.Domain.Products
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
