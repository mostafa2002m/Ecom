using Application.Dtos.Request.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProduct :IRepository<Product>
    {
        Task<bool> AddAsync(ProductDto productDto);
    }
}
