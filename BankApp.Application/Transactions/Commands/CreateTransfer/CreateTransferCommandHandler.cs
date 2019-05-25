using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankApp.Application.Infrastructure.TransactionResult;
using BankApp.Application.Interfaces;
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

            return new TransactionResult() { };
        }
    }
}
