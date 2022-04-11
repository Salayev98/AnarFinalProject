using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Material { get; set; }
        public string EngineType { get; set; }
        public int BatteryVoltage { get; set; }
        public int NumberofSpeeds { get; set; }
        public int ChargeTime { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public bool IsBestseller { get; set; }
        public bool isAvailable { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        [NotMapped]
        public List<int> ColorIds { get; set; } = new List<int>();
        [NotMapped]
        public IFormFile PosterFile { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
        [NotMapped]
        public List<int> PhotosIds { get; set; } = new List<int>();
        public List<ProductComment> Comments { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
