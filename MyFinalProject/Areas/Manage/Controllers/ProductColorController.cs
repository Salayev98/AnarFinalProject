using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductColorController : Controller
    {
        private readonly ProjectContext _context;
        public ProductColorController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var productcolor = _context.ProductColors.Include(x=>x.Product).Include(x=>x.Color).AsQueryable();
            return View(PaginatedList<ProductColor>.Create(productcolor,4,page));
        }

        public IActionResult Create()
        {
            ViewBag.Product = _context.Products.ToList();
            ViewBag.Color=_context.Colors.Include(x => x.ProductColors).ThenInclude(x => x.Product).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductColor productColor)
        {
            ViewBag.Product = _context.Products.ToList();
            ViewBag.Color = _context.Colors.Include(x => x.ProductColors).ThenInclude(x => x.Product).ToList();
            if (!_context.Products.Any(x => x.Id == productColor.ProductId))
            {
                ModelState.AddModelError("ProductId", "Invalid ProductId ");
                return View();
            }
            if (!_context.Colors.Any(x => x.Id == productColor.ColorId))
            {
                ModelState.AddModelError("ColorId", "Invalid ColorId ");
                return View();
            }
            _context.ProductColors.Add(productColor);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Edit(int id)
        {
            ViewBag.Color = _context.Colors.Include(x => x.ProductColors).ThenInclude(x => x.Product).ToList();
            ProductColor productColor = _context.ProductColors.Include(x=>x.Product).FirstOrDefault(x => x.Id == id);
            if (productColor == null)
            {
                return NotFound();
            }
            return View(productColor);
        }

        [HttpPost]
        
        public IActionResult Edit(ProductColor productColor)
        {
            ViewBag.Color = _context.Colors.Include(x => x.ProductColors).ThenInclude(x => x.Product).ToList();
            var existproductcolor = _context.ProductColors.Include(x=>x.Product).FirstOrDefault(x => x.Id == productColor.Id);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (!_context.Colors.Any(x => x.Id == productColor.ColorId))
            {
                ModelState.AddModelError("ColorId", "Invalid ColorId ");
                return View();
            }

            existproductcolor.ColorId = productColor.ColorId;     
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        
    }
}
