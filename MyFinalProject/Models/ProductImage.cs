using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool? PosterStatus { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
