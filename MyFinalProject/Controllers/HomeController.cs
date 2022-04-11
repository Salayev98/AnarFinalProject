using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using MyFinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectContext _context;

        public HomeController(ProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                Features = _context.Features.ToList(),
                Products=_context.Products.Include(x=>x.Category).Include(x=>x.ProductImages).Include(x=>x.Comments).ToList(),
                Categories=_context.Categories.Include(x=>x.Subcategories).ToList(),
                Blogs=_context.Blogs.ToList(),
            };
            return View(homeVM);
        }

        public IActionResult About()
        {
            AboutViewModel aboutVM = new AboutViewModel
            {
                Settings = _context.Settings.ToDictionary(x => x.Key, x => x.Value),
                OurTeams = _context.OurTeams.ToList()
            };
            return View(aboutVM);
        }

        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]

        public IActionResult Contact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Fags()
        {
            List<Fags> fags = _context.Fagses.ToList();
            return View(fags);
        }


    }
}
