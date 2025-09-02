using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ProductRepo(AppDbContext context) : Repository<Product>(context), IProduct
    {
    }
}
