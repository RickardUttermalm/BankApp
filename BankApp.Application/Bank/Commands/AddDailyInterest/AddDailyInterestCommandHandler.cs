using BankApp.Application.Interfaces;
using BankApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Bank.Commands.AddDailyInterest
{
    public class AddDailyInterestCommandHandler : IRequestHandler<AddDailyInterestCommand, bool>
    {
        private IBankAppDataContext _context;
        
        public AddDailyInterestCommandHandler(IBankAppDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddDailyInterestCommand request, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.AccountId == request.AccountId);
            if (account == null) return false;
            if (request.YearlyInterest < 0) return false;

            decimal dailyinterest = request.YearlyInterest / 365;
            var days = (DateTime.Now - request.LatestInterest).TotalDays;
            var interest = dailyinterest * (decimal)days;

            var transaction = new Transaction()
            {
                AccountId = account.AccountId,
                Amount = interest,
                Balance = account.Balance + interest,
                Date = DateTime.Now,
                Operation = "SavingsInterest",
                Type = "Credit"
            };

            account.Balance += interest;
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
