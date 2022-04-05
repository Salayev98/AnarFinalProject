using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using MyFinalProject.Utils;
using MyFinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly ProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ProjectContext context,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
       public async Task<IActionResult> Login(MemberLoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser member = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginVM.Username && !x.IsAdmin);

            if (member == null)
            {
                ModelState.AddModelError("", "UserName or Password is wrong");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(member, loginVM.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is wrong");
                return View();

            }
            return RedirectToAction("index", "home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(MemberRegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
                return View();
            AppUser member = await _userManager.FindByNameAsync(registerVM.Username);

            if (member != null)
            {
                ModelState.AddModelError("Username", "UserName is already exist");
                return View();
            }
            member = new AppUser
            {
                FullName = registerVM.FullName,
                UserName = registerVM.Username,
                Email = registerVM.Email
            };

            var result = await _userManager.CreateAsync(member, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }

            var roleresult = await _userManager.AddToRoleAsync(member, "Member");
            if (!roleresult.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            await _signInManager.SignInAsync(member, true);

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();
            var dbuser = await _userManager.FindByEmailAsync(email);
            if (dbuser is null)
            {
                return NotFound();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(dbuser);
            var link = Url.Action("ResetPassword", "Account", new { dbuser.Id, token }, protocol: HttpContext.Request.Scheme);

            var message = $"<a href='{link}'>Reset Password</a>";

            await EmailUtil.SendEmailAsync(email, "Reset Password", message);
            return RedirectToAction("login");

        }
        public async Task<IActionResult> ResetPassword(string id, string token)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            var dbuser = await _userManager.FindByIdAsync(id);
            if (dbuser is null)
            {
                return NotFound();
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id, string token, ResetPasswordViewModel resetPasswordVM)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View();
            var dbuser = await _userManager.FindByIdAsync(id);
            if (dbuser is null)
                return NotFound();
            var result = await _userManager.ResetPasswordAsync(dbuser, token, resetPasswordVM.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }

            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Member")]
        public IActionResult Profile()
        {
            AppUser member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            MemberProfileViewModel profileVM = new MemberProfileViewModel
            {
                Member = new MemberUpdateViewModel
                {
                    FullName = member.FullName,
                    Address = member.Address,
                    City = member.City,
                    Country = member.Country,
                    Email = member.Email,
                    Phone = member.PhoneNumber,
                    UserName = member.UserName
                },
                Orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).Where(x => x.AppUserId == member.Id).ToList()
            };
            return View(profileVM);
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Edit(MemberUpdateViewModel memberVM)
        {
            MemberProfileViewModel profileVM = new MemberProfileViewModel
            {
                Member = memberVM,

            };

            if (!ModelState.IsValid) {
                Console.WriteLine(ModelState);
                return View("Profile", profileVM);

            }

            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);

            if (member.UserName != memberVM.UserName && _userManager.Users.Any(x => x.NormalizedUserName == memberVM.UserName.ToUpper()))
            {
                ModelState.AddModelError("UserName", "UserName has already taken");
                return View("Profile", profileVM);
            }

            if (member.Email != memberVM.Email && _userManager.Users.Any(x => x.NormalizedEmail == memberVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Email has already taken");
                return View("Profile", profileVM);
            }

            member.Email = memberVM.Email;
            member.FullName = memberVM.FullName;
            member.PhoneNumber = memberVM.Phone;
            member.Country = memberVM.Country;
            member.City = memberVM.City;
            member.Address = memberVM.Address;

            var result = await _userManager.UpdateAsync(member);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("Profile", profileVM);
            }

            if (memberVM.Password != null)
            {
                if (string.IsNullOrWhiteSpace(memberVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "CurrentPassword is required!");
                    return View("Profile", profileVM);
                }

                if (!await _userManager.CheckPasswordAsync(member, memberVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "CurrentPassword is incorrect!");
                    return View("Profile", profileVM);
                }

                var passResult = _userManager.ChangePasswordAsync(member, memberVM.CurrentPassword, memberVM.Password);

                if (!passResult.Result.Succeeded)
                {
                    foreach (var item in passResult.Result.Errors)
                    {
                        ModelState.AddModelError("Password", item.Description);
                    }
                    return View("Profile", profileVM);
                }

            }
            return RedirectToAction("profile");
        }
    }
}
