using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BankApp.Domain.Entities;

namespace BankApp.Application.Interfaces
{
    public interface IBankAppDataContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Card> Cards { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Disposition> Dispositions { get; set; }
        DbSet<Loan> Loans { get; set; }
        DbSet<PermenentOrder> PermenentOrders { get; set; }
        DbSet<Transaction> Transactions { get; set; }
    }
}
