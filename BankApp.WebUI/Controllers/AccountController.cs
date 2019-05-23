using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BankApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult GetTransactionHistory(int id)
        {
            return View();
        }
    }
}