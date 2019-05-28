using BankApp.Application.Interfaces;
using BankApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Bank.Commands.AddInterest
{
    public class AddInterestCommandHandler : IRequestHandler<AddInterestCommand, bool>
    {
        private IBankAppDataContext _context;
        private IDateTime _datetime;
        
        public AddInterestCommandHandler(IBankAppDataContext context, IDateTime datetime)
        {
            _context = context;
            _datetime = datetime;
        }

        public async Task<bool> Handle(AddInterestCommand request, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.AccountId == request.AccountId);
            if (account == null) return false;
            if (request.YearlyInterest < 0) return false;

            //decimal dailyinterest = request.YearlyInterest / 365;
            //var days = (_datetime.Now.Date - request.LatestInterest).TotalDays;
            //var interest = (dailyinterest * (decimal)days) * account.Balance;

            var interest = account.Balance * request.YearlyInterest;

            var transaction = new Transaction()
            {
                AccountId = account.AccountId,
                Amount = interest,
                Balance = account.Balance + interest,
                Date = _datetime.Now,
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
