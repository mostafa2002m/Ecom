using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CategoryRepo(AppDbContext context) : Repository<Category>(context) , ICategory
    {
    }
}
