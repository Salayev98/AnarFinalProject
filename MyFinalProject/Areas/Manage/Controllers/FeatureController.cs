using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class FeatureController : Controller
    {
        private readonly ProjectContext _context;

        public FeatureController(ProjectContext context)
        {
           _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var feature = _context.Features.AsQueryable();
            return View(PaginatedList<Feature>.Create(feature,3,page));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            var existfeture = _context.Features.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Features.Add(feature);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var feature = _context.Features.FirstOrDefault(x => x.Id == id);
            if (feature == null)
            {
                return NotFound();
            }
            return View(feature);
        }
        [HttpPost]
        public IActionResult Edit(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var existfeature = _context.Features.FirstOrDefault(x => x.Id == feature.Id);
            if (existfeature == null)
            {
                return View();
            }

            existfeature.Desc = feature.Desc;
            existfeature.Title = feature.Title;
            existfeature.Icon = feature.Icon;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var feature = _context.Features.FirstOrDefault(x => x.Id == id);
            if (feature == null)
            {
                return NotFound();
            }
            return View(feature);
        }
        [HttpPost]
        public IActionResult Delete(Feature feature)
        {
            var existfeature = _context.Features.FirstOrDefault(x => x.Id == feature.Id);

            _context.Features.Remove(existfeature);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
