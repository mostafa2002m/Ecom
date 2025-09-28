using Application.Dtos.Request.Models;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepo(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
        : Repository<Product>(context), IProduct
    {
        private readonly AppDbContext context = context;
        private readonly IMapper mapper = mapper;
        public async Task<bool> AddAsync(CreateProductDto createProductDto)
        {
            
            if (createProductDto == null) {
                return false;
            }
            var product = mapper.Map<Product>(createProductDto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
                        
            var imagePath = await imageManagementService.AddImageAsync(createProductDto.Photo, createProductDto.Name);
            
            var photos = imagePath.Select(img => new Photo
            {
                ImageName = img,
                ProductId = product.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
           if(updateProductDto == null)
            {
                return false;
            }

            var isExist = await context.Products.Include(m=>m.Category).Include(m=>m.Photos)
                .FirstOrDefaultAsync(m=>m.Id == updateProductDto.Id);

            if (isExist == null)
            {
                return false;
            }
            var result = mapper.Map(updateProductDto, isExist);
            var findPhoto = await context.Photos.Where(m=>m.ProductId == updateProductDto.Id).ToListAsync();
            foreach (var item in findPhoto)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }
            context.Photos.RemoveRange(findPhoto);

            var imagePath = await imageManagementService.AddImageAsync(updateProductDto.Photo, updateProductDto.Name);
            var photo = imagePath.Select(path => new Photo
            {
                ImageName=path,
                ProductId = updateProductDto.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

