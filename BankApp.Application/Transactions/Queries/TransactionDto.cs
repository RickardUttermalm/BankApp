using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Transactions.Queries
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }

        public TransactionDto(Transaction t)
        {
            TransactionId = t.TransactionId;
            AccountId = t.AccountId;
            Date = t.Date.ToShortDateString();
            Type = t.Type;
            Operation = t.Operation;
            Amount = t.Amount;
            Balance = t.Balance;
            Symbol = t.Symbol;
            Bank = t.Bank;
            Account = t.Account;

        }
    }
}
