using Application.Dtos.Request.Models;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepo : Repository<Product>, IProduct
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;

        public ProductRepo(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.imageManagementService = imageManagementService;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> AddAsync(CreateProductDto pruductDto)
        {
            
            if (pruductDto == null) {
                return false;
            }
            var product = mapper.Map<Product>(pruductDto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
                        
            var imagePath = await imageManagementService.AddImageAsync(pruductDto.Photo, pruductDto.Name);
            
            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
            {
                return false;
            }
            //var existData = await context.Products.GetAsync(_ => _.Id != id && _.Name.Equals(model.Name));
            //if (existData is not null)
            //{

            //    return BadRequest(error: new ResponseApi(400, "Already Exist"));
            //}

            var isExist = await context.Products.Include(m => m.Category).Include(m => m.Photos)
                .FirstOrDefaultAsync(m => m.Id == updateProductDto.Id);

            if (isExist == null)
            {
                return false;
            }

            var result = mapper.Map(updateProductDto, isExist);

            var findPhoto = await context.Photos.Where(m => m.ProductId == updateProductDto.Id).ToListAsync();

            foreach (var item in findPhoto)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }
            context.Photos.RemoveRange(findPhoto);

            var imagePath = await imageManagementService.AddImageAsync(updateProductDto.Photo, updateProductDto.Name);
            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDto.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await context.Photos.Where(m=>m.ProductId == product.Id).ToListAsync();
            foreach (var item in photo)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);

            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

      

       
    }
}

