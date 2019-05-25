using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.CustomerDetails
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public String Created { get; set; }
        public decimal Balance { get; set; }
    }
}
