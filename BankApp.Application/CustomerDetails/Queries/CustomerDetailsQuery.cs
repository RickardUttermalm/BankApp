using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.CustomerDetails
{
    public class CustomerDetailsQuery : IRequest<CustomerDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
