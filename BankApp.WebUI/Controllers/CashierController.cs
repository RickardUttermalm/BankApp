using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Customers.Commands.CreateCustomer;
using BankApp.Application.Transactions.Commands;
using BankApp.Application.Transactions.Commands.CreateTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WebUI.Controllers
{
    [Authorize(Policy = "Cashieronly")]
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
                var Result = await _mediator.Send(command);

                if (Result.Success)
                {
                    TempData["success"] = $"kunden {command.Givenname} har skapats med kundId {Result.CustomerId}. " +
                                          $"Konto med kontonummer {Result.AccountId} har skapats med {command.Givenname} som ägare.s";
                    return View();
                }
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
                var result = await _mediator.Send(command);

                if (result.Success)
                {
                    return View("TransactionSucces");
                }

                TempData["Error"] = result.Message;
                return View(command);
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
                var result = await _mediator.Send(command);
                if (result.Success)
                {
                    return View("TransactionSucces");
                }
                TempData["Error"] = result.Message;
                return View(command);
            }
            return View(command);
        }

        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(CreateTransferCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);

                if (result.Success)
                {
                    return View("TransactionSuccess");
                }
                TempData["Error"] = result.Message;
                return View(command);
            }
            return View(command);
        }

        public IActionResult TransactionSucces()
        {
            return View();
        }
    }
}