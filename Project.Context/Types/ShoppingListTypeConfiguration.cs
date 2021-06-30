using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Project.Domain.Shopping;

using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Context.Types
{
    public class ShoppingListTypeConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId);
        }
    }
}
