
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ColorController : Controller
    {
        private readonly ProjectContext _context;

        public ColorController(ProjectContext context)
        {
            _context = context;

        }
        public IActionResult Index(int page = 1)
        {
            var color = _context.Colors.Include(x=>x.ProductColors).ThenInclude(x=>x.Product).AsQueryable();
            return View(PaginatedList<Color>.Create(color, 2, page));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var color = _context.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }
        [HttpPost]
        public IActionResult Edit(Color color)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var existcolor = _context.Colors.FirstOrDefault(x => x.Id == color.Id);
            if (existcolor == null)
            {
                return View();
            }

            existcolor.Name = color.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var color = _context.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }
        [HttpPost]
        public IActionResult Delete(Brand brand)
        {
            var existcolor = _context.Colors.FirstOrDefault(x => x.Id == brand.Id);

            _context.Colors.Remove(existcolor);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
