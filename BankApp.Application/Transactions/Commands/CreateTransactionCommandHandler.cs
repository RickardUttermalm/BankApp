using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BankApp.Application.Interfaces;
using BankApp.Domain.Entities;
using System.Linq;

namespace BankApp.Application.Transactions.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand>
    {
        private readonly IBankAppDataContext _context;
        public CreateTransactionCommandHandler(IBankAppDataContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            decimal balance;
            if(request.Type == "Credit")
            {
                balance = _context.Accounts.Single(a => a.AccountId == request.AccountId).Balance + request.Amount;
            }
            else
            {
                balance = _context.Accounts.Single(a => a.AccountId == request.AccountId).Balance - request.Amount;
            }
            
            var transaction = new Transaction()
            {
                AccountId = request.AccountId,
                Amount = -request.Amount,
                Date = DateTime.Now,
                Operation = request.Operation,
                Symbol = request.Symbol,
                Type = request.Type,
                Balance = balance
            };

            _context.Accounts.Single(a => a.AccountId == request.AccountId).Balance = balance;
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
