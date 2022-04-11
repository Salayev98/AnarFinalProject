using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
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
        public async Task<IActionResult> Register(AdminRegisterViewModel adminVm)
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
        public IActionResult Index(int page=1)
        {
            
            var admin = _context.AppUsers
                .Where(x=>x.IsAdmin==true)
                .AsQueryable();
            return View(PaginatedList<AppUser>.Create(admin, 3, page));
        }
        public IActionResult Edit(string id)
        {
            AppUser admin = _context.AppUsers
               .Where(x => x.IsAdmin == true).FirstOrDefault(x => x.Id == id);

            if (admin == null)
            {
                return NotFound();
            }
            AdminUpdateViewModel adminVm = new AdminUpdateViewModel
            {
                FullName = admin.FullName,
                Email = admin.Email,
                Username = admin.UserName
            };
        
            


            return View(adminVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AdminUpdateViewModel proVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.FindByNameAsync(proVM.Username);

            if (member.Email != proVM.Email && _userManager.Users.Any(x => x.NormalizedEmail == proVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Email has already taken");
                return View();
            }

            member.Email = proVM.Email;
            member.FullName = proVM.FullName;


            if (proVM.Password != null)
            {
                var passResult = _userManager.RemovePasswordAsync(member);

                if (passResult.Result.Succeeded)
                {
                    passResult =  _userManager.AddPasswordAsync(member, proVM.Password);
                    if (passResult.Result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {

                    foreach (var item in passResult.Result.Errors)
                    {
                        ModelState.AddModelError("Password", item.Description);
                    }
                    return View();
                }
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(string id)
        {
            AppUser admin = _context.AppUsers.FirstOrDefault(x => x.Id == id);
            return View(admin);
        }

        [HttpPost]
        public IActionResult Delete(AppUser admin)
        {

            AppUser existappuser = _context.AppUsers.FirstOrDefault(x => x.Id == admin.Id);
            if (existappuser == null)
            {
                return NotFound();
            }
            _context.AppUsers.Remove(existappuser);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
