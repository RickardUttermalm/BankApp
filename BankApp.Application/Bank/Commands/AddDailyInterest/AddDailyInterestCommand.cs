﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Bank.Commands.AddDailyInterest
{
    public class AddDailyInterestCommand : IRequest<bool>
    {
        public int AccountId { get; set; }
        public DateTime LatestInterest { get; set; }
        public decimal YearlyInterest { get; set; }
    }
}