using Application.Dtos.Request.Models;
using Application.Helper;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{


    public class ProductController(IUnitOfWork context, IMapper mapper): BaseController(context, mapper)
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        { 
            try
            {
                var response = await context.GetRepository<Product>().GetAllAsync(x => x.Category, x => x.Photos);
                if (response is null || !response.Any())
                {
                    return BadRequest(error: new ResponseApi(400, "No Data Found"));
                }
                var result = mapper.Map<List<ProductDto>>(response);
                return Ok(result);
            }
            catch (Exception ex)
            {

                
                return BadRequest(new ResponseApi(500, ex.Message));
            }
        }

        [HttpGet("one/{id}")]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                var response = await context.GetRepository<Product>().GetAsync(_ => _.Id == id, x=> x.Category, x => x.Photos);

                if (response is null)
                {
                    return BadRequest(error: new ResponseApi(400, "Not Found"));
                }
                var result = mapper.Map<ProductDto>(response);
                return Ok(result);
            }
            catch (Exception ex)
            {

               
                return BadRequest(new ResponseApi(500, ex.Message));
            }
        }

        [HttpPost("add"),DisableRequestSizeLimit]

        public async Task<IActionResult> Add(List<IFormFile> x, CreateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }
            try
            {
                Product existData = await context.GetRepository<Product>().GetAsync(_ => _.Name.Equals(model.Name));
                if (existData is not null)
                {
                    
                    return BadRequest(new ResponseApi(400, "Already Exist"));
                }

                await context.BeginTransactionAsync();

                
                await context.ProductRepo.AddAsync(model);
                await context.CommitAsync();

                return Ok(new ResponseApi(200 , "Added"));


            }
            catch (Exception ex)
            {
                await context.RollbackAsync();
                return BadRequest(new ResponseApi(500, ex.Message));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }
            try
            {
                var existData = await context.GetRepository<Product>().GetAsync(_=>_.Id != id && _.Name.Equals(model.Name));
                if (existData is not null)
                {
                    
                    return BadRequest(error: new ResponseApi(400,  "Already Exist"));
                }

                
                    await context.ProductRepo.UpdateAsync(model);
                    await context.SaveChangesAsync();
                    return Ok(new ResponseApi(200, "Updated"));

               
            }
            catch (Exception ex)
            {
                
                return BadRequest(new ResponseApi(500, ex.Message));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                
                return BadRequest(error: new ResponseApi(400));
            }

            try
            {
                var deletedData = await context.GetRepository<Product>().GetAsync(_ => _.Id == id);
                if (deletedData == null)
                {
                    
                    return BadRequest(error: new ResponseApi(400, "Deleted Data Is Invalid"));
                }
                await context.GetRepository<Product>().DeleteAsync(id);
                await context.SaveChangesAsync();
                return Ok(new ResponseApi(200, "Deleted"));

            }
            catch (Exception ex)
            {

                
                return BadRequest(new ResponseApi(500, ex.Message));
            }
        }
    }
}
