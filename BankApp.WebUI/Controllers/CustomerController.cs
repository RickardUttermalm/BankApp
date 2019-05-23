using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BankApp.Application.CustomerDetails;
using BankApp.Application.Customers.Queries;

namespace BankApp.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult CustomerSummary()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerSummary(int id)
        {
            return PartialView("_CustomerDetails", await _mediator.Send(new CustomerDetailsQuery() { Id = id}));
        }

        public IActionResult SearchCustomers()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchCustomers(GetCustomersListQuery model)
        {
            return PartialView("_CustomersList", 
            await _mediator.Send(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowMore(CustomersListViewModel model)
        {
            return PartialView("_CustomerListRows",
                await _mediator.Send(new GetCustomersListQuery()
                { City = model.SearchCity, Name = model.SearchName, Offset = model.PageNumber + 1 }));
        }
    }
}