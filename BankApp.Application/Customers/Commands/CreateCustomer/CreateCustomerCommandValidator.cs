using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Givenname).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Streetaddress).NotEmpty().MaximumLength(100);
            RuleFor(x => x.City).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Zipcode).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Telephonecountrycode).NotEmpty();
            RuleFor(x => x.Telephonenumber).NotEmpty();
            RuleFor(x => x.Emailaddress).NotEmpty().EmailAddress();           
        }
    }
}
