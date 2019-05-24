using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BankApp.Application.Transactions.Queries;

namespace BankApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> TransactionHistory(int id, int pagenr)
        {
            return View(await _mediator.Send(new GetTransactionHistoryQuery() {Id = id, Pagenr = pagenr}));
        }
    }
}