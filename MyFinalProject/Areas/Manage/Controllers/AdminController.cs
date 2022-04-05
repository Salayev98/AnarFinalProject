using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Areas.Manage.ViewModels;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles ="SuperAdmin")]

    public class AdminController : Controller
    {
        private readonly ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ProjectContext context,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminRegisterViewModel adminVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.FindByNameAsync(adminVm.Username);
            if (member != null)
            {
                ModelState.AddModelError("Username", "UserName is already exist");
                return View();
            }
            member = new AppUser
            {
                FullName = adminVm.FullName,
                UserName=adminVm.Username,
                Email=adminVm.Email,
                IsAdmin=true
            };
            var result = await _userManager.CreateAsync(member, adminVm.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            return RedirectToAction("RoleToAdmin",member);
        }
        public async Task<IActionResult> RoleToAdmin(AppUser member)
        {
            var admin = _context.AppUsers.FirstOrDefault(x => x.UserName == member.UserName);
            //var result1 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            AppUser adminrole = await _userManager.FindByNameAsync(admin.UserName);
            await _userManager.AddToRoleAsync(adminrole, "Admin");
            return RedirectToAction("index", "dashboard");
        }
    }
}
