using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Infrastructure.OperationResults
{
    public class TransactionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
