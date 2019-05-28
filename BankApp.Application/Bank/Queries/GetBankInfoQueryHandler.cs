using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BankApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankApp.Application.Bank.Queries
{
    public class GetBankInfoQueryHandler : IRequestHandler<GetBankInfoQuery, BankInfoViewModel>
    {
        private IBankAppDataContext _context;
        public GetBankInfoQueryHandler(IBankAppDataContext context)
        {
            _context = context;
        }

        public async Task<BankInfoViewModel> Handle(GetBankInfoQuery request, CancellationToken cancellationToken)
        {
            return new BankInfoViewModel()
            {
                TotalCustomers = _context.Customers.Count(),
                TotalBalance = _context.Accounts.Sum(a => a.Balance),
                TotalAccounts = _context.Accounts.Count()
            };
            
        }
    }
}
