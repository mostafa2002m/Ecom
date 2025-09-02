using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PhotoRepo(AppDbContext context) : Repository<Photo>(context), IPhoto
    {
    }
}
