using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class ItemInBasket
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int ProductColorId { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }

        public AppUser AppUser { get; set; }
        public ProductColor ProductColor { get; set; }
    }
}
