using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Infrastructure
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public int CurrentYear => DateTime.Now.Year;
    }
}
