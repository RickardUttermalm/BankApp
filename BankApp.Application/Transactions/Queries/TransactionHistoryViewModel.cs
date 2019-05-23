using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Transactions.Queries
{
    public class TransactionHistoryViewModel
    {
        public int AccountId { get; set; }
        public Decimal Balance { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
