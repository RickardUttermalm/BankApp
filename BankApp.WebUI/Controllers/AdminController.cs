using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BankApp.Application.Interfaces;
using BankApp.Persistence;
using BankApp.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WebUI.Controllers
{
    [Authorize(Policy = "Adminonly")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IBankAppDataContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IBankAppDataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(AppUserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleresult = await _userManager.AddToRoleAsync(user, model.Role);
                    if (roleresult.Succeeded)
                    {
                        TempData["success"] = "Användaren har skapats";
                        return View();
                    }
                }
            }
            return View(model);
        }

        public IActionResult ManageRoles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(RoleManagerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (model.Regular) await _userManager.AddClaimAsync(user, new Claim("Regular", "true"));
                    if (model.Cashier) await _userManager.AddClaimAsync(user, new Claim("Cashier", "true"));
                    if (model.Admin) await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));

                    TempData["Status"] = $"{model.Email}s roller är uppdaterade.";
                    return View();
                }
            }
            TempData["Status"] = $"Hittade ingen användare med email: {model.Email}";
            return View();
        }
    }
}