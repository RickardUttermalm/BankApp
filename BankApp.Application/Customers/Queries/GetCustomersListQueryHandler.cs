using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BankApp.Domain;
using BankApp.Application.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Customers.Queries
{
    class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, CustomersListViewModel>
    {
        private readonly IBankAppDataContext _context;
        public GetCustomersListQueryHandler(IBankAppDataContext context)
        {
            _context = context;
        }
        public async Task<CustomersListViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            if (request.City == null) request.City = "";
            if (request.Name == null) request.Name = "";
            var customers = _context.Customers.Where(c => (c.Givenname.Contains(request.Name) ||
                                                     c.Surname.Contains(request.Name)) && 
                                                     c.City.Contains(request.City));
            var count = customers.Count();

            var customerspage = customers.Skip(request.Offset * 50).Take(50).ToList();

            bool ismore = count > (request.Offset + 1) * 50 ? true : false;
          
            return new CustomersListViewModel(customerspage, ismore, request.Offset, request.Name, request.City); 
        }
    }
}
