using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BankApp.Application.Customers.Queries
{
    public class GetCustomersListQuery : IRequest<CustomersListViewModel>
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int Offset { get; set; }
    }
}
