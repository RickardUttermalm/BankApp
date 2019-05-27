using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Interfaces;
using BankApp.Persistence;
using BankApp.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly SignInManager<ApplicationUser> _signinmanager;

        public UserController(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinmanager)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinmanager.PasswordSignInAsync(user.Email, user.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Loginfail"] = "Användarnamn eller lösenord stämmer inte";
                    return View(user);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await _signinmanager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}