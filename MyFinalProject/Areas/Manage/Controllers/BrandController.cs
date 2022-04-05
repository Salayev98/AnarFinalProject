
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinal.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BrandController : Controller
    {
        private readonly ProjectContext _context;

        public BrandController(ProjectContext context)
        {
            _context = context;

        }
        public IActionResult Index(int page = 1)
        {
            var brand = _context.Brands.AsQueryable();
            return View(PaginatedList<Brand>.Create(brand, 2, page));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            var existslider = _context.Sliders.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var brand = _context.Brands.FirstOrDefault(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var existbrand = _context.Brands.FirstOrDefault(x => x.Id == brand.Id);
            if (existbrand == null)
            {
                return View();
            }

            existbrand.Name = brand.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        public IActionResult Delete(Brand brand)
        {
            var existbrand = _context.Brands.FirstOrDefault(x => x.Id == brand.Id);
            
            _context.Brands.Remove(existbrand);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
