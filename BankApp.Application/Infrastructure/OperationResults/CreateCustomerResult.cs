using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Infrastructure.OperationResults
{
    public class CreateCustomerResult
    {
        public bool Success { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
    }
}
