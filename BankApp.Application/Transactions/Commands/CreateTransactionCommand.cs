using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Transactions.Commands
{
    public class CreateTransactionCommand : IRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
