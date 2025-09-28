using Domain.Entities;

namespace Application.Dtos.Request.Models
{
    public class PhotoDto : CreatePhotoDto
    {
        public int Id { get; set; }
        public virtual ProductDto Product { get; set; }
    }

    public class CreatePhotoDto
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }
    }  
    public class UpdatePhotoDto : CreatePhotoDto { }

}
