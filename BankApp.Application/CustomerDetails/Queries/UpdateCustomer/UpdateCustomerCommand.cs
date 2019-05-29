using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.CustomerDetails.Queries.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public CustomerDetailDto customer { get; set; }
    }
}
