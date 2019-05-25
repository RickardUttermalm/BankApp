using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace BankApp.Application.Transactions.Commands.CreateTransfer
{
    public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
    {
        public CreateTransferCommandValidator()
        {
            RuleFor(x => x.FromAccountId).NotEmpty();
            RuleFor(x => x.ToAccountId).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            
        }
    }
}
