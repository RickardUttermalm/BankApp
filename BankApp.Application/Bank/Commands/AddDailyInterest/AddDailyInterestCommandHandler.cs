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
            var Accounts = _context.Accounts.Where(a => a.AccountId < 4);
            var TransactionList = new List<Transaction>();
            var date = DateTime.Now;
            foreach (var item in Accounts)
            {
                decimal interest = (item.Balance * (decimal)0.05) / 12;
                TransactionList.Add(new Transaction() {
                    AccountId = item.AccountId,
                    Amount = interest,
                    Balance = item.Balance + interest,
                    Date = date,
                    Operation = "Monthly interest",
                    Type = "Credit"
                });
                item.Balance += interest;
            }

            await _context.Transactions.AddRangeAsync(TransactionList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
