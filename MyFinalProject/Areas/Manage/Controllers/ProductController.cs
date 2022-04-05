using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Dal;
using MyFinalProject.Helpers;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(ProjectContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(bool? isdeleted,int page=1)
        {
            ViewBag.IsDeleted = isdeleted;
            var product = _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.Color)
                .Include(x => x.Category)
                .Include(x => x.Brand).AsQueryable();
            if (isdeleted != null)
            {
                product = product.Where(x => x.IsDeleted == isdeleted);
            }
            return View(PaginatedList<Product>.Create(product,4,page));
        }

        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Brand = _context.Brands.ToList();
            ViewBag.Color = _context.Colors.Include(x => x.ProductColors).ThenInclude(x => x.Product).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Brand = _context.Brands.ToList();
            ViewBag.Color = _context.Colors.ToList();

            if (product.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "Invalid PosterFile");
                return View();
            }
            else
            {
                if (product.PosterFile.ContentType != "image/jpeg" && product.PosterFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("PosterFile", "file type must be image/jpeg or image/png");
                    return View();
                }

                if (product.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "file size must be less than 2mb");
                    return View();
                }
            }

            if (product.Photos != null)
            {
                foreach (var photo in product.Photos)
                {
                    if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Photos", "file type must be image/jpeg or image/png");
                        return View();
                    }

                    if (photo.Length > 2097152)
                    {
                        ModelState.AddModelError("Photos", "file size must be less than 2mb");
                        return View();
                    }
                }

            }
            if (!_context.Brands.Any(x => x.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Invalid BrandId ");
                return View();
            }
            if (!_context.Categories.Any(x => x.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Invalid CategoryId ");
                return View();
            }

            product.ProductImages = new List<ProductImage>();
            ProductImage posterimage = new ProductImage
            {
                PosterStatus = true,
                ImageUrl = FileManager.Save("upload/product", product.PosterFile, _env.WebRootPath)
            };
            product.ProductImages.Add(posterimage);

            if (product.Photos != null)
            {
                foreach(var photo in product.Photos)
                {
                    ProductImage images = new ProductImage
                    {
                        ImageUrl =FileManager.Save("upload/product",photo,_env.WebRootPath)
                    };
                    product.ProductImages.Add(images);
                }
            }

            product.ProductColors = new List<ProductColor>();
            if (product.ColorIds != null)
            {
                foreach (var colorid in product.ColorIds)
                {
                    if (_context.Colors.Any(x => x.Id == colorid))
                    {
                        ProductColor color = new ProductColor { ColorId = colorid };
                        product.ProductColors.Add(color);
                    }
                    else
                    {
                        ModelState.AddModelError("ColorIds", "Color not found");
                        return View();
                    }

                }
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Brand = _context.Brands.ToList();
            ViewBag.Color = _context.Colors.ToList();
            Product product = _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.Color)
                .Include(x => x.Category)
                .Include(x => x.Brand).Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);
            product.ColorIds = product.ProductColors.Select(x => x.ColorId).ToList();

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]

        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            if (product.PosterFile != null && product.PosterFile.ContentType != "image/jpeg" && product.PosterFile.ContentType != "image/png")
            {
                ModelState.AddModelError("PosterFile", "file type must be image/jpeg or image/png");
                return View();
            }

            if (product.PosterFile != null && product.PosterFile.Length > 2097152)
            {
                ModelState.AddModelError("PosterFile", "file size must be less than 2mb");
                return View();
            }

            if (product.Photos != null)
            {
                foreach (var photo in product.Photos)
                {
                    if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Photos", "file type must be image/jpeg or image/png");
                        return View();
                    }

                    if (photo.Length > 2097152)
                    {
                        ModelState.AddModelError("Photos", "file size must be less than 2mb");
                        return View();
                    }
                }

            }
            if (!_context.Brands.Any(x => x.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Invalid GenreID ");
                return View();
            }
            if (!_context.Categories.Any(x => x.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Invalid AuthorId ");
                return View();
            }
            Product existproduct = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductColors).ThenInclude(x => x.Color).FirstOrDefault(x => x.Id == product.Id);
            ProductImage posterimg = _context.ProductImages.FirstOrDefault(x => x.PosterStatus == true);
            if (product.PosterFile != null)
            {
                string newimg = FileManager.Save("upload/product", product.PosterFile, _env.WebRootPath);
                if (posterimg != null)
                {
                    FileManager.Delete("upload/product", posterimg.ImageUrl, _env.WebRootPath);
                    posterimg.ImageUrl = newimg;
                }
                existproduct.ProductImages.Add(posterimg);
            }
            existproduct.ProductImages.RemoveAll(x => x.PosterStatus == null && !product.PhotosIds.Contains(x.Id));


            if (product.Photos != null)
            {
                foreach (var photo in product.Photos)
                {
                    ProductImage newimage = new ProductImage
                    {
                      
                        ImageUrl = FileManager.Save("upload/product", photo, _env.WebRootPath)
                    };
                    existproduct.ProductImages.Add(newimage);
                }
            }
            existproduct.ProductColors = new List<ProductColor>();
            if (product.ColorIds != null)
            {
                foreach (var colorid in product.ColorIds)
                {
                    ProductColor newcolor = new ProductColor
                    {
                        ColorId = colorid
                    };
                    existproduct.ProductColors.Add(newcolor);
                }
            }

            existproduct.Name = product.Name;
            existproduct.SalePrice = product.SalePrice;
            existproduct.Material = product.Material;
            existproduct.EngineType = product.EngineType;
            existproduct.BatteryVoltage = product.BatteryVoltage;
            existproduct.NumberofSpeeds = product.NumberofSpeeds;
            existproduct.ChargeTime = product.ChargeTime;
            existproduct.Weight = product.Weight;
            existproduct.Height = product.Height;
            existproduct.Width = product.Width;
            existproduct.Length = product.Length;
            existproduct.CostPrice = product.CostPrice;
            existproduct.DiscountPercent = product.DiscountPercent;
            existproduct.BrandId = product.BrandId;
            existproduct.CategoryId = product.CategoryId;
            existproduct.IsBestseller = product.IsBestseller;

            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductColors).ThenInclude(x => x.Color).FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {

            Product existproduct = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductColors).ThenInclude(x => x.Color).FirstOrDefault(x => x.Id == product.Id && !x.IsDeleted);
            if (existproduct == null)
            {
                return NotFound();
            }
            existproduct.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Restore(int id)
        {

            Product existproduct = _context.Products.FirstOrDefault(x => x.Id == id && x.IsDeleted);
            if (existproduct == null)
            {
                return NotFound();
            }
            existproduct.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
