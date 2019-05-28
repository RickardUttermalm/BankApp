using BankApp.Application.Bank.Commands.AddInterest;
using BankApp.Application.Interfaces;
using BankApp.Application.Transactions.Commands;
using BankApp.Application.Transactions.Commands.CreateTransfer;
using BankApp.Infrastructure;
using BankApp.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace BankApp.UnitTests
{
    public class UnitTest1
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
                var handler = new CreateTransactionCommandHandler(context);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.False(result.Success);
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
        [InlineData(1, 0.05)]
        public async Task AddInterest_Tests(int accountid, decimal yearlyint)
        {
            var command = new AddInterestCommand() {AccountId = accountid,
                            YearlyInterest = yearlyint, LatestInterest = DateTime.Now.AddYears(-1)};

            var options = new DbContextOptionsBuilder<BankAppDataContext>()
            .UseInMemoryDatabase(databaseName: "AddInterest_Tests")
            .Options;

            using (var context = new BankAppDataContext(options))
            {
                var machine = new MachineDateTime();
                var handler = new AddInterestCommandHandler(context, machine);

                var account = await context.Accounts.SingleOrDefaultAsync(a => a.AccountId == accountid);
                var expected = account.Balance + (account.Balance * yearlyint);

                await handler.Handle(command, CancellationToken.None);

                var newbalance = await context.Accounts.SingleOrDefaultAsync(a => a.AccountId == accountid);
                Assert.Equal(expected, newbalance.Balance);
            }
        }
    }
}
