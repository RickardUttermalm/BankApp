using BankApp.Application.Bank.Commands.AddInterest;
using BankApp.Application.Interfaces;
using BankApp.Application.Transactions.Commands;
using BankApp.Application.Transactions.Commands.CreateTransfer;
using BankApp.Domain.Entities;
using BankApp.Infrastructure;
using BankApp.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace BankApp.UnitTests
{
    public class TransactionTests
    {
        [Theory]
        [InlineData(-100, "Debit")]
        [InlineData(-100, "Credit")]
        [InlineData(999999999, "Debit")]
        public async Task CreateTransaction_Tests(decimal amount, string type)
        {
            var command = new CreateTransactionCommand() {AccountId = 1, Amount = amount, Type = type };
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
            .UseInMemoryDatabase(databaseName: "CreateTransaction_Tests")
            .Options;

            using (var context = new BankAppDataContext(options))
            {
                context.Accounts.Add(new Account() { AccountId = 1, Balance = 1000 });

                var handler = new CreateTransactionCommandHandler(context);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.False(result.Success);
            }
        }

        [Theory]
        [InlineData(100, "Credit")]
        public async Task CreateTransactionsRight_Tests(decimal amount, string type)
        {
            var command = new CreateTransactionCommand() { AccountId = 1, Amount = amount, Type = type };
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
            .UseInMemoryDatabase(databaseName: "CreateTransactionsRight_Tests")
            .Options;

            using (var context = new BankAppDataContext(options))
            {
                await context.AddAsync(new Account() { AccountId = 1, Balance = 1000 });
                await context.SaveChangesAsync(CancellationToken.None);
                var handler = new CreateTransactionCommandHandler(context);

                var result = await handler.Handle(command, CancellationToken.None);

                decimal expected = 1100;
                var account = await context.Accounts.SingleOrDefaultAsync(a => a.AccountId == 1);
                var transactions = await context.Accounts.Include(a => a.Transactions).SingleOrDefaultAsync(a => a.AccountId == 1);
                var count = transactions.Transactions.Count();
                var actual = account.Balance;
                Assert.Equal(expected, actual);
                Assert.Equal(1, count);
            }
        }

        [Theory]
        [InlineData(-100, 1,3 )]
        [InlineData(134145100, 1,3 )]
        [InlineData(100, 131, 0 )]
        public async Task CreateTransfer_Tests(decimal amount, int fromacc, int toacc)
        {
            var command = new CreateTransferCommand() { Amount = amount, FromAccountId = fromacc, ToAccountId = toacc };
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
            .UseInMemoryDatabase(databaseName: "CreateTransfer_Tests")
            .Options;

            using (var context = new BankAppDataContext(options))
            {
                var handler = new CreateTransferCommandHandler(context);
                var result = await handler.Handle(command, CancellationToken.None);

                Assert.False(result.Success);
            }
        }

        [Theory]
        [InlineData(0.05, 1)]
        public async Task AddInterest_Tests(decimal yearlyint, int accountid)
        {
            var account = new Account() { AccountId = 1, Balance = 10000 };
            var command = new AddInterestCommand() { AccountId = accountid, 
                            YearlyInterest = yearlyint, LatestInterest = DateTime.Now};

            var options = new DbContextOptionsBuilder<BankAppDataContext>()
            .UseInMemoryDatabase(databaseName: "AddInterest_Tests")
            .Options;

            decimal expected;

            using (var context = new BankAppDataContext(options))
            {
                context.Add(new Account() { AccountId = 1, Balance = 10000 });
                context.SaveChanges();
                var machine = new MachineDateTime() {};
                machine.Now = machine.Now.AddDays(30);
                var handler = new AddInterestCommandHandler(context, machine);

                expected = 10039.73m;

                await handler.Handle(command, CancellationToken.None);

                var newbalance = await context.Accounts.SingleOrDefaultAsync(a => a.AccountId == 1);
                var test = Math.Round(newbalance.Balance, 2);
                Assert.Equal(expected, test);
            }

        }
    }
}
