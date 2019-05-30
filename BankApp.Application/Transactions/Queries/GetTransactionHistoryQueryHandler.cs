using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Transactions.Queries
{
    public class GetTransactionHistoryQueryHandler : IRequestHandler<GetTransactionHistoryQuery, TransactionHistoryViewModel>
    {
        private readonly IBankAppDataContext _context;
        public GetTransactionHistoryQueryHandler(IBankAppDataContext context)
        {
            _context = context;
        }

        public async Task<TransactionHistoryViewModel> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            var transactions = _context.Transactions.Where(t => t.AccountId == request.Id).OrderByDescending(t => t.TransactionId);
            bool ismore = await transactions.CountAsync() > request.Pagenr * 20 ? true : false;
           
            var account = _context.Accounts.SingleOrDefault(a => a.AccountId == request.Id);
            account.Transactions = transactions.Skip(20 * (request.Pagenr - 1)).Take(20).ToList();
            return new TransactionHistoryViewModel(account , ismore, request.Pagenr);
        }
    }
}
