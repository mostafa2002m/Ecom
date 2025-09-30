using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Request.Models
{
    public class ProductDto : CreateProductDto
    {
        public int Id { get; set; }
        public virtual List<PhotoDto> Photos { get; set; }
        public virtual CategoryDto Category { get; set; }
        public string CategoryName { get; set; }

    }

    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photo { get; set; }

    }
    public class UpdateProductDto : CreateProductDto
    {
        public int Id { get; set; }
    }
}
