using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BankApp.Application.Bank.Queries
{
    public class GetBankInfoQuery : IRequest<BankInfoViewModel>
    {
    }
}
