using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.ViewModels
{
    public class CheckOutViewModel
    {
        public BasketViewModel Basket { get; set; }
        public Order Order { get; set; }
    }
}
