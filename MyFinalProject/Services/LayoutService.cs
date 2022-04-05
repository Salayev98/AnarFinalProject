using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using MyFinalProject.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Services
{
    public class LayoutService
    {
        private readonly ProjectContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(ProjectContext context, IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager)
        {
           _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }   

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }

        public BasketViewModel GetBasket()
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                Products = new List<BasketViewModelItem>(),
                TotalPrice = 0
            };

            List<ProductToBasket> basketItems = new List<ProductToBasket>();
            AppUser member = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == _httpContextAccessor.HttpContext.User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                var baksetStr = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

                if (!string.IsNullOrWhiteSpace(baksetStr))
                    basketItems = JsonConvert.DeserializeObject<List<ProductToBasket>>(baksetStr);

            }
            else
            {
                basketItems = _context.BasketItems.Where(x => x.AppUserId == member.Id).Select(b => new ProductToBasket { ProductId = b.ProductId, Count = b.Count }).ToList();
            }


            foreach (var item in basketItems)
            {
                Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                BasketViewModelItem productBasketitems = new BasketViewModelItem
                {
                    Product = product,
                    Count = item.Count
                };

                basketVM.Products.Add(productBasketitems);
                decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                basketVM.TotalPrice += totalPrice * item.Count;
            }

            return basketVM;
        }
    }
}
