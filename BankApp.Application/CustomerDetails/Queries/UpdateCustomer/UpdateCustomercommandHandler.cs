using BankApp.Application.CustomerDetails.Queries.UpdateCustomer;
using BankApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.CustomerDetails.Queries
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private IBankAppDataContext _context;
        public UpdateCustomerCommandHandler(IBankAppDataContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerId == request.customer.CustomerId);

            if (customer == null) return false;

            customer.City = request.customer.City;
            customer.Country = request.customer.Country;
            customer.CountryCode = request.customer.CountryCode;
            customer.Emailaddress = request.customer.Emailaddress;
            customer.Gender = request.customer.Gender;
            customer.Givenname = request.customer.Givenname;
            customer.NationalId = request.customer.NationalId;
            customer.Streetaddress = request.customer.Streetaddress;
            customer.Surname = request.customer.Surname;
            customer.Telephonecountrycode = request.customer.Telephonecountrycode;
            customer.Telephonenumber = request.customer.Telephonenumber;
            customer.Zipcode = request.customer.Zipcode;

            if (request.customer.Birthday != null)
            {
                var bd = DateTime.Parse(request.customer.Birthday);
                customer.Birthday = bd;
            }

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
