using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.ViewModels
{
    public class BasketViewModelItem
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
