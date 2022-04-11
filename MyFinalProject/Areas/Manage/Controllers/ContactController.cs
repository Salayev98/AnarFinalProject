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
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class ContactController : Controller
    {
        private readonly ProjectContext _context;

        public ContactController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var contact = _context.Contacts.AsQueryable();
            return View(PaginatedList<Contact>.Create(contact,4,page));
        }
    }
}
