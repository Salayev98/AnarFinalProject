using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.ViewModels
{
    public class BasketViewModel
    {
        public List<BasketViewModelItem> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
