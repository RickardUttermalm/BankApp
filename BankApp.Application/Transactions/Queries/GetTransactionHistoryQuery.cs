using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BankApp.Application.Transactions.Queries
{
    public class GetTransactionHistoryQuery : IRequest<TransactionHistoryViewModel>
    {
        public int Id { get; set; }
    }
}
