using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly ProjectContext _context;

        public BlogController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.Page = page;

            var blogs = _context.Blogs.AsQueryable();
            ViewBag.TotalPage = (int)Math.Ceiling(blogs.Count() / 4d);

            return View(PaginatedList<Blog>.Create(blogs,4,page));
        }
        public IActionResult BlogDetail(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            return View(blog);
        }
    }
}
