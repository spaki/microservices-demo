using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSD.Sales.Domain.Models;

namespace MSD.Sales.Repository.Db.Maps
{
    internal class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasMany(e => e.Items).WithOne(e => e.Order).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
