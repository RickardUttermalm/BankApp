using BankApp.Application.Interfaces;
using BankApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private  IBankAppDataContext _context;
        public CreateCustomerCommandHandler(IBankAppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            string coutrycode;
            if (request.Country == "Sweden") coutrycode = "SE";
            else if (request.Country == "Norway") coutrycode = "NO";
            else if (request.Country == "Finland") coutrycode = "FI";
            else coutrycode = "DK";

            var customer = new Customer()
            {
                Gender = request.Gender,
                Givenname = request.Givenname,
                Surname = request.Surname,
                Streetaddress = request.Streetaddress,
                City = request.City,
                Zipcode = request.Zipcode,
                Country = request.Country,
                CountryCode = coutrycode,
                Birthday = request.Birthday,
                NationalId = request.NationalId,
                Telephonecountrycode = request.Telephonecountrycode,
                Telephonenumber = request.Telephonenumber,
                Emailaddress = request.Emailaddress
            };
            await _context.Customers.AddAsync(customer);

            var account = new Account()
            {
                Balance = 0,
                Created = DateTime.Now,
                Frequency = "Monthly",
            };
            await _context.Accounts.AddAsync(account);

            var disposition = new Disposition()
            {
                Account = account,
                Customer = customer,
                Type = "OWNER"
            };

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
