using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BankApp.Application.Interfaces;
using BankApp.Domain.Entities;
using System.Linq;
using BankApp.Application.Infrastructure.OperationResults;

namespace BankApp.Application.Transactions.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResult>
    {
        private readonly IBankAppDataContext _context;
        public CreateTransactionCommandHandler(IBankAppDataContext context)
        {
            _context = context;
        }
        public async Task<TransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.AccountId == request.AccountId);

            if (account == null)
            {
                return new TransactionResult() {Success = false, Message = "Felaktigt kontonummer."};
            }

            if (request.Type == "Debit" && account.Balance < request.Amount)
            {
                return new TransactionResult() { Success = false, Message = "För lågt saldo." };
            }

            decimal newbalance;
            if(request.Type == "Credit")
            {
                newbalance = account.Balance + request.Amount;
            }
            else
            {
                newbalance = account.Balance - request.Amount;
                request.Amount = -request.Amount;
            }
            
            var transaction = new Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Date = DateTime.Now,
                Operation = request.Operation,
                Symbol = request.Symbol,
                Type = request.Type,
                Balance = newbalance
            };

            account.Balance = newbalance;
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return new TransactionResult() { Success = false};
        }
    }
}
