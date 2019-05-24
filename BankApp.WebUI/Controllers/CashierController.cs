using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Customers.Commands.CreateCustomer;
using BankApp.Application.Transactions.Commands;
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

        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(CreateTransactionCommand command)
        {
            if (ModelState.IsValid)
            {
                command.Operation = "Credit in Cash";
                command.Type = "Credit";
                var success = await _mediator.Send(command);
            }


            return View(command);
        }
        
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(CreateTransactionCommand command)
        {
            if (ModelState.IsValid)
            {
                command.Operation = "Withdrawal in Cash";
                command.Type = "Debit";
                var success = await _mediator.Send(command);
            }


            return View(command);
        }
    }
}