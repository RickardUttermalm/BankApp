using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Transactions.Queries
{
    public class TransactionHistoryViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();

        public int PageNumber { get; set; }

        public bool IsMorePages { get; set; }

        public TransactionHistoryViewModel(Account a, bool ismore, int pagenr)
        {
            AccountId = a.AccountId;
            Balance = a.Balance;

            foreach (var item in a.Transactions)
            {
                Transactions.Add(new TransactionDto(item));
            }

            IsMorePages = ismore;
            PageNumber = pagenr;
        }


    }
}
