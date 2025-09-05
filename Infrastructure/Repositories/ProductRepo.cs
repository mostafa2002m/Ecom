using Application.Dtos.Request.Models;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ProductRepo(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : Repository<Product>(context), IProduct
    {
        private readonly AppDbContext context = context;
        public async Task<bool> AddAsync(ProductDto productDto)
        {
            if (productDto == null) return false;

            var product = mapper.Map<Product>(productDto);
            await context.AddAsync(product);
            await context.SaveChangesAsync();
            return true;

            var imagePath = await imageManagementService.UploadImageAsync(productDto.Photo,productDto.Name);
            if (imagePath != null && imagePath.Count > 0)
            {
                foreach (var image in imagePath)
                {
                    product.Photos.Add(new Photo
                    {
                        ImageName = image,
                        ProductId = product.Id
                    });
                }
                await context.SaveChangesAsync();
                return true;

            }
        }
    }
}
