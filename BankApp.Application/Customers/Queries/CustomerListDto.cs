using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Domain.Entities;

namespace BankApp.Application.Customers.Queries
{
    public class CustomerListedDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }

        public CustomerListedDto(Customer c)
        {
            CustomerId = c.CustomerId;
            Name = c.Givenname + " " + c.Surname;
            Address = c.Streetaddress + ", " + c.Zipcode + ", " + c.City;
            DateOfBirth = c.Birthday.Value.ToString("yyyy-MM-dd");
        }
    }
}
