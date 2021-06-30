using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Project.Domain.Shopping;

using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Context.Types
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Product).WithMany().HasForeignKey(i => i.ProductId);
            builder.HasOne(i => i.Cart).WithMany(c => c.Items).HasForeignKey(i => i.CartId);
        }
    }
}
