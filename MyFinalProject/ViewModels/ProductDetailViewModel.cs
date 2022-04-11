using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public ProductColor ProductColor { get; set; }
        public List<Product> RelatedProdcts { get; set; }
        public ProductComment Comment { get; set; }
    }
}
