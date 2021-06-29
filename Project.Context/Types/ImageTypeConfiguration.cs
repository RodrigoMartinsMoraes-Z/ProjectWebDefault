using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Project.Domain.Products;

namespace Project.Context.Types
{
    public class ImageTypeConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Ignore(i => i.Base64);

            builder.HasOne(i => i.Product).WithMany(p => p.Images).HasForeignKey(i => i.ProductId);
        }
    }
}
