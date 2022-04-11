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
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class ProfileController : Controller
    {
        private readonly ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(ProjectContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            AppUser admin = _context.AppUsers.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ProfileUpdateViewModel proVm = new ProfileUpdateViewModel
            {
                FullName=admin.FullName,
                Username=admin.UserName,
                Email=admin.Email,
                Country=admin.Country,
                City=admin.City,
                Street=admin.Address,
                Phone=admin.PhoneNumber
            };
            return View(proVm);
        }

        public IActionResult Edit(string username)
        {
            AppUser existuser = _context.AppUsers.FirstOrDefault(x => x.UserName == username);
            if (existuser is null)
                return NotFound();
            ProfileUpdateViewModel proVm = new ProfileUpdateViewModel
            {
                FullName = existuser.FullName,
                Username = existuser.UserName,
                Email = existuser.Email,
                Country = existuser.Country,
                City = existuser.City,
                Street = existuser.Address,
                Phone = existuser.PhoneNumber
            };

            return View(proVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileUpdateViewModel proVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);


            if (member.UserName != proVM.Username && _userManager.Users.Any(x => x.NormalizedUserName == proVM.Username.ToUpper()))
            {
                ModelState.AddModelError("UserName", "UserName has already taken");
                return View();
            }

            if (member.Email != proVM.Email && _userManager.Users.Any(x => x.NormalizedEmail == proVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Email has already taken");
                return View();
            }

            member.Email = proVM.Email;
            member.FullName = proVM.FullName;
            member.PhoneNumber = proVM.Phone;
            member.Country = proVM.Country;
            member.City = proVM.City;
            member.Address = proVM.Street;
            member.UserName = proVM.Username;

            var result = await _userManager.UpdateAsync(member);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();

                }
            }

            if (proVM.NewPassword != null)
            {
                if (string.IsNullOrWhiteSpace(proVM.OldPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "CurrentPassword is required!");
                    return View();
                }

                if (!await _userManager.CheckPasswordAsync(member, proVM.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "OldPassword is incorrect!");
                    return View();
                }

                var passResult = _userManager.ChangePasswordAsync(member, proVM.OldPassword, proVM.NewPassword);

                if (!passResult.Result.Succeeded)
                {
                    foreach (var item in passResult.Result.Errors)
                    {
                        ModelState.AddModelError("NewPassword", item.Description);
                    }
                    return View();
                }
            }
            return RedirectToAction("index");
        }

    }
}
