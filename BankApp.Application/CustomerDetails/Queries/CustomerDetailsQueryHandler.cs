using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankApp.Application.Interfaces;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankApp.Domain.Entities;

namespace BankApp.Application.CustomerDetails
{
    public class CustomerDetailsQueryHandler : IRequestHandler<CustomerDetailsQuery, CustomerDetailsViewModel>
    {
        private IBankAppDataContext _context;
        public CustomerDetailsQueryHandler(IBankAppDataContext context)
        {
            _context = context;
        }
        public async Task<CustomerDetailsViewModel> Handle(CustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customer = _context.Customers.Include(c => c.Dispositions).ThenInclude(d => d.Account)
                                                    .SingleOrDefault(c => c.CustomerId == request.Id);
            var Total = customer.Dispositions.Sum(d => d.Account.Balance);

            return new CustomerDetailsViewModel() { Customer = new CustomerDetailDto(customer),
                                                    TotalBalance = Total};
                                                   
        }


    }
}
