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
            var account = _context.Accounts.Include(a => a.Transactions).Single(a => a.AccountId == request.Id);
            account.Transactions = account.Transactions.OrderByDescending(t => t.Date)
                .ThenByDescending(t => t.TransactionId).ToList();

            bool ismore = account.Transactions.Count > request.Pagenr * 20 ? true : false;

            account.Transactions = account.Transactions.Skip(20 * (request.Pagenr -1)).Take(20).ToList();

            return new TransactionHistoryViewModel(account, ismore, request.Pagenr);
        }
    }
}
