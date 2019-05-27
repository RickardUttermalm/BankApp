using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BankApp.Application.CustomerDetails;
using BankApp.Application.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDetailsViewModel>>Get(int id)
        {
            return Ok(await _mediator.Send(new CustomerDetailsQuery() { Id = id }));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDetailsViewModel>> Search([FromQuery]string name, string city, int page = 0)
        {
            return Ok(await _mediator.Send(new GetCustomersListQuery() { Name = name, City = city, Offset = page  }));
        }

        [Authorize]
        [HttpGet]
        [Route("/api/me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDetailsViewModel>>Me()
        {
            var currentuser = HttpContext.User;
            if (currentuser.HasClaim(c => c.Type == "userid"))
            {
                var id = currentuser.Claims.SingleOrDefault(c => c.Type == "userid").Value;
                   
                return Ok(await _mediator.Send(new CustomerDetailsQuery() { Id = int.Parse(id)}));
            }
            return NotFound();
        }
    }
}