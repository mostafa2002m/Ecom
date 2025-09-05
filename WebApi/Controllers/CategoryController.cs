using Application.Dtos.Request.Models;
using Application.Helper;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{


    public class CategoryController(IUnitOfWork context, IMapper mapper)
        : BaseController(context, mapper)
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await context.CategoryRepo.GetAllAsync();
                if (response is null || !response.Any())
                {
                    return BadRequest(error: new ResponseApi(400));
                }
                var result = mapper.Map<List<CategoryDto>>(response);
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
                var response = await context.CategoryRepo.GetAsync(_ => _.Id == id);

                if (response is null)
                {
                    return BadRequest(error: new ResponseApi(400, "Not Found"));
                }
                var result = mapper.Map<CategoryDto>(response);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseApi(500 , ex.Message));
            }
        }

        [HttpPost("add")]

        public async Task<IActionResult> Add(CreateCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existData = await context.CategoryRepo.GetAsync(_=>_.Name.Equals(model.Name, StringComparison.CurrentCultureIgnoreCase));
                if (existData is not null)
                {
                    return BadRequest(error: new ResponseApi(400, "Already Exist"));
                }

                await context.BeginTransactionAsync();

                var result = mapper.Map<Category>(model);

                await context.CategoryRepo.AddAsync(result);
                await context.SaveChangesAsync();
                await context.CommitAsync();

                return Ok(new ResponseApi(200 , "Added"));


            }
            catch (Exception ex)
            {
                await context.RollbackAsync();
                return BadRequest(new ResponseApi(500 ,ex.Message));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existData = await context.CategoryRepo.GetAsync(_=>_.Id == id && _.Name.Equals(model.Name, StringComparison.CurrentCultureIgnoreCase));
                if (existData is not null)
                {
                    return BadRequest(error: new ResponseApi(400,  "Already Exist"));
                }

                var isExist = await context.CategoryRepo.GetAsync(_ => _.Id == id);
                if (isExist is not null)
                {
                    var result = mapper.Map(model, isExist);
                    await context.CategoryRepo.UpdateAsync(result);
                    await context.SaveChangesAsync();
                    return Ok(new ResponseApi(200, "Updated"));
                }
                else
                {
                    return BadRequest(error: new ResponseApi(400, "Category not found"));
                }
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
                var deletedData = await context.CategoryRepo.GetAsync(_ => _.Id == id);
                if (deletedData == null)
                {
                    return BadRequest(error: new ResponseApi(400, "Deleted Data Is Invalid"));
                }
                await context.CategoryRepo.DeleteAsync(id);
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
