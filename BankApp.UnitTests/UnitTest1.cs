using BankApp.Application.Interfaces;
using BankApp.Application.Transactions.Commands;
using BankApp.Application.Transactions.Commands.CreateTransfer;
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
    }
}
