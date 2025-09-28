using Microsoft.AspNetCore.Http;
using System.Collections;

namespace Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public List<Photo> Photos { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
       
    }
}
