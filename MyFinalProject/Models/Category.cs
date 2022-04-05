using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isPopular { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }
        public List<SubCategory> Subcategories { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
