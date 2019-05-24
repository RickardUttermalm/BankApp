using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Customers.Commands.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WebUI.Controllers
{
    public class CashierController : Controller
    {
        private readonly IMediator _mediator;
        public CashierController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            if (ModelState.IsValid)
            {
                var succes = await _mediator.Send(command);

                TempData["success"] = $"kunden {command.Givenname} har skapats.";

                return View();
            }

            return View(command);
        }
    }
}