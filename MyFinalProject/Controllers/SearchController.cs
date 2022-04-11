using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProjectContext _context;
        public SearchController(ProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult Search(string search)
        {
            IEnumerable<Product> list = new List<Product>();
            if (search != null)
            {
                list = _context.Products.Include(p => p.ProductImages)
                .Where(t => t.Name.ToLower().Contains(search.ToLower())).Take(5);
                return PartialView("_SearchPartialView", list);
            }
            return Ok();
        }
    }
}
