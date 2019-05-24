using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace BankApp.Application.Transactions.Commands
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            
        }
    }
}
