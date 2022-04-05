using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
    public class CategoryController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(ProjectContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var category = _context.Categories.AsQueryable();
            return View(PaginatedList<Category>.Create(category, 2, page));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            FileManager.Save("upload/category", category.ImageFile, _env.WebRootPath);
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var existcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (existcategory == null)
            {
                return View();
            }
            if (category.ImageFile != null && category.ImageFile.ContentType != "image/jpeg" && category.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("PosterFile", "file type must be image/jpeg or image/png");
                return View();
            }

            if (category.ImageFile != null && category.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("PosterFile", "file size must be less than 2mb");
                return View();
            }
            if (category.ImageFile != null)
            {
                string newimg = FileManager.Save("upload/category", category.ImageFile, _env.WebRootPath);
                if (existcategory.Image != null)
                {
                    FileManager.Delete("upload/category", existcategory.Image, _env.WebRootPath);
                }
                existcategory.Image = newimg;
            }

            existcategory.Name = category.Name;
            existcategory.isPopular = category.isPopular;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            var existcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            _context.Categories.Remove(existcategory);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
