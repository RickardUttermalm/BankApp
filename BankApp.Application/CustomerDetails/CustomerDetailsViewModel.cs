using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.CustomerDetails
{
    public class CustomerDetailsViewModel
    {
        public CustomerDetailDto Customer { get; set; }
        public Decimal TotalBalance { get; set; }
    }
}
