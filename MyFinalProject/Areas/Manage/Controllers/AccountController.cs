using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
      public class AccountController : Controller
        {
            private readonly ProjectContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public AccountController(ProjectContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
            {
                _context = context;
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
            }

        //public async Task<IActionResult> test()
        //{
        //    AppUser superadmin = new AppUser
        //    {
        //        Email = "anar-salayev99@mail.ru",
        //        UserName = "AnarSuper98",
        //        FullName = "Anar Salayev",
        //        IsAdmin = true
        //    };
        //    var result = await _userManager.CreateAsync(superadmin, "Admin123");
        //    return Ok(result.Errors);
        //}
        public async Task<IActionResult> Test()
        {
            //var result1 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //var result2 = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            //var result3 = await _roleManager.CreateAsync(new IdentityRole("Member"));
            AppUser admin = await _userManager.FindByNameAsync("AnarSuper98");
            await _userManager.AddToRoleAsync(admin, "SuperAdmin");
            return Ok();
        }

        public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Login(AdminLoginViewModel adminVm)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == adminVm.Username && x.IsAdmin);

            if (admin == null)
                {
                     ModelState.AddModelError("", "Invalid Email or Username");
                     return View();
                }
                var result = await _signInManager.PasswordSignInAsync(admin, adminVm.Password, true, false);

                 if (!result.Succeeded)
                 {
                   ModelState.AddModelError("", "Invalid Email or Username");
                   return View();
                 }

                 return RedirectToAction("index", "dashboard");
            }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }
    }
        
        

    
}
