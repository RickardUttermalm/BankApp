using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankApp.Application.Infrastructure.TransactionResult;
using BankApp.Application.Interfaces;
using BankApp.Domain.Entities;
using MediatR;

namespace BankApp.Application.Transactions.Commands.CreateTransfer
{
    public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, TransactionResult>
    {
        private readonly IBankAppDataContext _context;
        public CreateTransferCommandHandler(IBankAppDataContext context)
        {
            _context = context;
        }

        public async Task<TransactionResult> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            var fromaccount = _context.Accounts.SingleOrDefault(a => a.AccountId == request.FromAccountId);
            var toaccount = _context.Accounts.SingleOrDefault(a => a.AccountId == request.ToAccountId);

            if (fromaccount == null) return new TransactionResult() { Success = false, Message = $"Kontonummer {request.FromAccountId} finns inte." };
            if (toaccount == null) return new TransactionResult() { Success = false, Message = $"Kontonummer {request.ToAccountId} finns inte." };
            if (fromaccount.Balance < request.Amount) return new TransactionResult() {Success = false, Message = "För lågt saldo" };

            var fromtransaction = new Transaction()
            {
                AccountId = request.FromAccountId,
                Account = request.ToAccountId.ToString(),
                Amount = -request.Amount,
                Balance = fromaccount.Balance - request.Amount,
                Bank = "Rickbanken",
                Date = DateTime.Now,
                Operation = "Transfer",
                Symbol = request.Symbol,
                Type = "Debit"
            };

            var totransaction = new Transaction()
            {
                AccountId =request.ToAccountId,
                Account = request.FromAccountId.ToString(),
                Amount = request.Amount,
                Balance = toaccount.Balance + request.Amount,
                Bank = "Rickbanken",
                Date = DateTime.Now,
                Operation = "Transfer from an other account",
                Symbol = request.Symbol,
                Type = "Credit"
            };

            fromaccount.Balance -= request.Amount;
            toaccount.Balance += request.Amount;

            await _context.Transactions.AddAsync(fromtransaction);
            await _context.Transactions.AddAsync(totransaction);
            await _context.SaveChangesAsync(cancellationToken);

            return new TransactionResult() {Success = true };
        }
    }
}
