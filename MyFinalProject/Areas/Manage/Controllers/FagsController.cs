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
    public class FagsController : Controller
    {
        private readonly ProjectContext _context;

        public FagsController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var fags = _context.Fagses.AsQueryable();
            return View(PaginatedList<Fags>.Create(fags,4,page));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Fags fags)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _context.Fagses.Add(fags);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            
            Fags fags = _context.Fagses.FirstOrDefault(x => x.Id == id);
            if (fags == null)
            {
                return NotFound();
            }
            return View(fags);
        }
        [HttpPost]
        public IActionResult Edit(Fags fags)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            Fags existfags = _context.Fagses.FirstOrDefault(x => x.Id == fags.Id);
            if (existfags == null)
            {
                return NotFound();
            }
            existfags.Information = fags.Information;
            existfags.Title = fags.Title;
            existfags.isShipping = fags.isShipping;
            existfags.isPayment = fags.isPayment;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Fags fags = _context.Fagses.FirstOrDefault(x => x.Id == id);
            if (fags is null)
            {
                return NotFound();
            }
            return View(fags);
        }
        [HttpPost]
        public IActionResult Delete(Fags fags)
        {
            Fags existfags = _context.Fagses.FirstOrDefault(x => x.Id == fags.Id);
            if (existfags == null)
            {
                return NotFound();
            }
            _context.Fagses.Remove(existfags);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
