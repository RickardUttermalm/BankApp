using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankApp.Application.Interfaces;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BankApp.Domain.Entities;

namespace BankApp.Application.CustomerDetails
{
    public class CustomerDetailsQueryHandler : IRequestHandler<CustomerDetailsQuery, CustomerDetailsViewModel>
    {
        private IBankAppDataContext _context;
        private IMapper _mapper;
        public CustomerDetailsQueryHandler(IBankAppDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
