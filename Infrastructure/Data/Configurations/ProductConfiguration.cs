using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.NewPrice).HasColumnType("Decimal(18.2)");
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "IPhone 13",
                    Description = "IPhone 13 with 128GB",
                    NewPrice = 999.99m,
                    OldPrice = 1099.99m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S21",
                    Description = "Samsung Galaxy S21 with 128GB",
                    NewPrice = 899.99m,
                    OldPrice = 999.99m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Dell XPS 13",
                    Description = "Dell XPS 13 Laptop with Intel i7",
                    NewPrice = 1199.99m,
                    OldPrice = 1299.99m,
                    CategoryId = 2
                }
            );
        }
    }
}
