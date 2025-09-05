using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Request.Models
{
    public class CategoryDto : CreateCategoryDto
    {
        public int Id { get; set; }
        //public ICollection<ProductDto> ProductDtos { get; set; }
    }

    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "category must not be empty")]
        [StringLength(maximumLength: 50, ErrorMessage = "letters not more than 50")]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryDto : CreateCategoryDto
    {

    }
}
