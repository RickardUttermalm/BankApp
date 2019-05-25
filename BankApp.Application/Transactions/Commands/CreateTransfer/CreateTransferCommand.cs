using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Application.Infrastructure.OperationResults;
using MediatR;

namespace BankApp.Application.Transactions.Commands.CreateTransfer
{
    public class CreateTransferCommand : IRequest<TransactionResult>
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        
    }
}
