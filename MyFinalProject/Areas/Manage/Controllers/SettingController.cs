
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
    public class SettingController : Controller
    {
        private readonly ProjectContext _context;

        public SettingController(ProjectContext context)
        {
            _context = context;

        }
        public IActionResult Index(int page = 1)
        {
            var setting = _context.Settings.AsQueryable();
            return View(PaginatedList<Setting>.Create(setting, 2, page));
        }
       
        public IActionResult Edit(int id)
        {
            var setting = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }
        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var existsetting = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);
            if (existsetting == null)
            {
                return View();
            }

            existsetting.Key = setting.Key;
            existsetting.Value = setting.Value;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        
    }
}
