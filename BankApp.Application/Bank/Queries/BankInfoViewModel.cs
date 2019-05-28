using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Bank.Queries
{
    public class BankInfoViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public Decimal TotalBalance { get; set; }
    }
}
