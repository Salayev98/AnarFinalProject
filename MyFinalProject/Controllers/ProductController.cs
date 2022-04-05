using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using MyFinalProject.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(ProjectContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int? brandid,int? categoryid, int? colorid,decimal? minprice,decimal? maxprice,int page=1)
        {
            ViewBag.CategoryId = categoryid;
            ViewBag.BrandId = brandid;
            ViewBag.ColorId = colorid;
            ViewBag.Page = page;
            ViewBag.FilterbyMin = minprice;
            ViewBag.FilterbyMax = maxprice;
            ProductViewModel productVM = new ProductViewModel();
            var products = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductColors).ThenInclude(x => x.Color).AsQueryable();
            if (brandid != null)
            {
                products = products.Where(x => x.BrandId == brandid);
            }
            if (categoryid != null)
            {
                products = products.Where(x => x.CategoryId == categoryid);
            };
            if (colorid != null)
            {
                products = products.Where(x => x.ProductColors.Any(x => x.Color.Id == colorid));
            };

            if (products.Any())
            {
                productVM.MinPrice = products.Min(x => x.SalePrice);
                productVM.MaxPrice = products.Max(x => x.SalePrice);
            }

            if (minprice != null && maxprice != null)
                products = products.Where(x => x.SalePrice >= minprice && x.SalePrice <= maxprice);
            else
            {
                ViewBag.FilterbyMin = productVM.MinPrice;
                ViewBag.FilterbyMax = productVM.MaxPrice;
            }
            
            ViewBag.TotalPage = (int)Math.Ceiling(products.Count() / 4d);

            productVM.Products = products.Skip((page - 1) * 4).Take(4).ToList();
            productVM.Categories = _context.Categories.Include(x => x.Products).ToList();
            productVM.Brands = _context.Brands.Include(x => x.Products).ToList();
            productVM.Colors = _context.Colors.Include(x => x.ProductColors).ThenInclude(x => x.Product).ToList();
            

            return View(productVM);
        }
        public IActionResult GetProducts(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductImages).Include(x=>x.Brand).Include(x => x.ProductColors).ThenInclude(x=>x.Color).FirstOrDefault(x => x.Id == id);
            return PartialView("_ProductModalPartial", product);
        }


        public IActionResult AddtoBasket(int id)
        {
            if (!_context.Products.Any(x => x.Id == id))
                return NotFound();

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["basket"];
                List<ProductToBasket> items = new List<ProductToBasket>();

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                    items = JsonConvert.DeserializeObject<List<ProductToBasket>>(basketItemsStr);

                ProductToBasket item = items.FirstOrDefault(x => x.ProductId == id);

                if (item == null)
                {
                    item = new ProductToBasket { ProductId = id, Count = 1 };
                    items.Add(item);
                }
                else
                    item.Count++;

                basketItemsStr = JsonConvert.SerializeObject(items);

                HttpContext.Response.Cookies.Append("basket", basketItemsStr);

                return PartialView("_BasketPartialView", _getBasket(items));
            }
            else
            {
                BasketItem item = _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (item == null)
                {
                    item = new BasketItem
                    {
                        AppUserId = member.Id,
                        ProductId = id,
                        CreatedAt = DateTime.UtcNow.AddHours(4),
                        Count = 1
                    };
                    _context.BasketItems.Add(item);
                }
                else
                    item.Count++;

                _context.SaveChanges();

                var items = _context.BasketItems.Where(x => x.AppUserId == member.Id).ToList();
                return PartialView("_BasketPartialView", _getBasket(items));
            }
        }

        private BasketViewModel _getBasket(List<ProductToBasket> basketItems)
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                Products = new List<BasketViewModelItem>(),
                TotalPrice = 0
            };

            foreach (var item in basketItems)
            {
                Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                BasketViewModelItem productBasketItem = new BasketViewModelItem
                {
                    Product = product,
                    Count = item.Count
                };

                basketVM.Products.Add(productBasketItem);
                decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                basketVM.TotalPrice += totalPrice * item.Count;
            }

            return basketVM;
        }

        private BasketViewModel _getBasket(List<BasketItem> basketItems)
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                Products = new List<BasketViewModelItem>(),
                TotalPrice = 0
            };

            foreach (var item in basketItems)
            {
                Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                BasketViewModelItem productBasketItem = new BasketViewModelItem
                {
                    Product = product,
                    Count = item.Count
                };

                basketVM.Products.Add(productBasketItem);
                decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                basketVM.TotalPrice += totalPrice * item.Count;
            }

            return basketVM;
        }
        public IActionResult RemoveBasket(int id)
        {
            if (!_context.Products.Any(x => x.Id == id))
                return NotFound();

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["basket"];
                List<ProductToBasket> items = new List<ProductToBasket>();

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                    items = JsonConvert.DeserializeObject<List<ProductToBasket>>(basketItemsStr);

                ProductToBasket item = items.FirstOrDefault(x => x.ProductId == id);

                if (item.Count == 1)
                {
                    items.Remove(item);

                }
                else
                {
                    item.Count--;
                }



                basketItemsStr = JsonConvert.SerializeObject(items);

                HttpContext.Response.Cookies.Append("basket", basketItemsStr);

                return PartialView("_BasketPartialView", _getBasket(items));
            }
            else
            {
                BasketItem item = _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (item.Count == 1)
                {

                    _context.BasketItems.Remove(item);
                }
                else
                {
                    item.Count--;

                }

                _context.SaveChanges();

                var items = _context.BasketItems.Where(x => x.AppUserId == member.Id).ToList();
                return PartialView("_BasketPartialView", _getBasket(items));
            }
        }

        public IActionResult ProductDetail(int id)
        {
            Product product = _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Comments)
                .ThenInclude(x => x.AppUser)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color).FirstOrDefault(x => x.Id == id);
            ProductDetailViewModel prodVM = new ProductDetailViewModel
            {
                Product = product,
                RelatedProdcts = _context.Products.Include(x => x.ProductImages).Where(x => x.BrandId == product.BrandId).ToList(),
                Comment = new ProductComment { ProductId = id }
            };
            return View(prodVM);
        }
        [HttpPost]
        public IActionResult Comment(ProductComment comment)
        {
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            if (member is null)
            {
                return RedirectToAction("login", "account");
            }

            if (!ModelState.IsValid)
            {
                Product product = _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Comments)
                .ThenInclude(x => x.AppUser)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color).FirstOrDefault(x => x.Id == comment.ProductId);
                ProductDetailViewModel prodVM = new ProductDetailViewModel
                {
                    Product = product,
                    RelatedProdcts = _context.Products.Include(x => x.ProductImages).Where(x => x.BrandId == product.BrandId).ToList(),
                    Comment = comment
                };
                if (product is null)
                    return NotFound();


                return View("ProductDetail", prodVM);
            }
            comment.AppUserId = member.Id;
            comment.CreatedAt = DateTime.UtcNow.AddHours(4);
            _context.ProductComments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("ProductDetail", new { id = comment.ProductId });
        }

        public IActionResult WishList()
        {
            WishListViewModel wishVm = new WishListViewModel
            {
                Products = new List<WishListViewModelItem>(),
            };

            List<ProductToWishList> wishitems = new List<ProductToWishList>();
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName ==HttpContext.User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                var wishStr = HttpContext.Request.Cookies["wish"];

                if (!string.IsNullOrWhiteSpace(wishStr))
                    wishitems = JsonConvert.DeserializeObject<List<ProductToWishList>>(wishStr);

            }
            else
            {
                wishitems = _context.WishListItems.Where(x => x.AppUserId == member.Id).Select(b => new ProductToWishList { ProductId = b.ProductId}).ToList();
            }


            foreach (var item in wishitems)
            {
                Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                WishListViewModelItem productWishitems = new WishListViewModelItem
                {
                    Product = product,
                };

                wishVm.Products.Add(productWishitems);
            }

            return View(wishVm);
        }


        public IActionResult AddtoWishlist(int id)
        {
            if (!_context.Products.Any(x => x.Id == id))
                return NotFound();

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                string wishItemsStr = HttpContext.Request.Cookies["wish"];
                List<ProductToWishList> items = new List<ProductToWishList>();

                if (!string.IsNullOrWhiteSpace(wishItemsStr))
                    items = JsonConvert.DeserializeObject<List<ProductToWishList>>(wishItemsStr);

                ProductToWishList item = items.FirstOrDefault(x => x.ProductId == id);

                if (item == null)
                {
                    item = new ProductToWishList { ProductId = id };
                    items.Add(item);
                }


                wishItemsStr = JsonConvert.SerializeObject(items);

                HttpContext.Response.Cookies.Append("wish", wishItemsStr);

                return PartialView("_WishPartial", _getWish(items));
            }
            else
            {
                WishListItem item = _context.WishListItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (item == null)
                {
                    item = new WishListItem
                    {
                        AppUserId = member.Id,
                        ProductId = id,
                        CreatedAt = DateTime.UtcNow.AddHours(4),
                        Count = 1
                    };
                    _context.WishListItems.Add(item);
                }
                _context.SaveChanges();

                var items = _context.WishListItems.Where(x => x.AppUserId == member.Id).ToList();
                return PartialView("_WishPartial", _getWish(items));
            }
        }

        private WishListViewModel _getWish(List<ProductToWishList> wishitems)
        {
            WishListViewModel wishVM = new WishListViewModel
            {
                Products = new List<WishListViewModelItem>(),
            };

            foreach (var item in wishitems)
            {
                Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                WishListViewModelItem productWishItem = new WishListViewModelItem
                {
                    Product = product,
                };

                wishVM.Products.Add(productWishItem);
            }

            return wishVM;
        }

        private WishListViewModel _getWish(List<WishListItem> wishitems)
        {
            WishListViewModel wishVm = new WishListViewModel
            {
                Products = new List<WishListViewModelItem>(),
            };

            foreach (var item in wishitems)
            {
                Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                WishListViewModelItem productwishitem = new WishListViewModelItem
                {
                    Product = product,
                };

                wishVm.Products.Add(productwishitem);
            }

            return wishVm;
        }
        public IActionResult RemoveWish(int id)
        {

            if (!_context.Products.Any(x => x.Id == id))
                return NotFound();

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                string wishitemstr = HttpContext.Request.Cookies["wish"];
                List<ProductToWishList> items = new List<ProductToWishList>();

                if (!string.IsNullOrWhiteSpace(wishitemstr))
                    items = JsonConvert.DeserializeObject<List<ProductToWishList>>(wishitemstr);

                ProductToWishList item = items.FirstOrDefault(x => x.ProductId == id);
               
                items.Remove(item);

                wishitemstr = JsonConvert.SerializeObject(items);

                HttpContext.Response.Cookies.Append("wish", wishitemstr);
                

                return PartialView("_WishPartial", _getWish(items));
            }
            else
            {
                WishListItem item = _context.WishListItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                _context.WishListItems.Remove(item);

                _context.SaveChanges();

                var items = _context.WishListItems.Where(x => x.AppUserId == member.Id).ToList();
                return PartialView("_WishPartial", _getWish(items));
            }
        }
        public IActionResult Cart()
        {
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            CartViewModel cartVm = new CartViewModel
           {
               Basketitems=_getBasketCart(member)
           };
            
            
            return View(cartVm);
        }
        private BasketViewModel _getBasketCart(AppUser member)
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                Products = new List<BasketViewModelItem>()
            };

            List<ProductToBasket> items = new List<ProductToBasket>();

            if (member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["basket"];

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                    items = JsonConvert.DeserializeObject<List<ProductToBasket>>(basketItemsStr);
            }
            else
            {
                items = _context.BasketItems.Where(x => x.AppUserId == member.Id).Select(b => new ProductToBasket { ProductId = b.ProductId, Count = b.Count }).ToList();
            }

            foreach (var item in items)
            {
                Product product = _context.Products.Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                BasketViewModelItem productItem = new BasketViewModelItem
                {
                    Product = product,
                    Count = item.Count
                };

                decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                basketVM.TotalPrice += totalPrice * item.Count;

                basketVM.Products.Add(productItem);
            }

            return basketVM;
        }
       
    }
}
