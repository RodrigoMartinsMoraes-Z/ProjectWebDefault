﻿using Project.Web.Models.Images;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public virtual CategoryModel Category { get; set; }
        public virtual ICollection<ImageModel> Images { get; set; }
    }
}