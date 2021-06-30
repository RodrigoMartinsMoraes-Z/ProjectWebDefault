using Project.Web.Models.Images;
using Project.Web.Models.Product;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Models.Shopping
{
    public class CartProduct
    {
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public CategoryModel Category { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
