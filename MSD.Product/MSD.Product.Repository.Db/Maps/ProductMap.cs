using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSD.Product.Repository.Db.Maps
{
    internal class ProductMap : IEntityTypeConfiguration<Domain.Models.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
