using AutoMapper;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BankApp.Application.CustomerDetails
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }

        public List<AccountDto> Accounts { get; set; }

        public CustomerDetailDto(Customer c)
        {
            CustomerId = c.CustomerId;
            Gender = c.Gender;
            Givenname = c.Givenname;
            Surname = c.Surname;
            Streetaddress = c.Streetaddress;
            City = c.City;
            Zipcode = c.Zipcode;
            Country = c.Country;
            CountryCode = c.CountryCode;
            Birthday = c.Birthday.Value.ToString("yyyy-MM-dd");
            NationalId = c.NationalId;
            Telephonecountrycode = c.Telephonecountrycode;
            Telephonenumber = c.Telephonenumber;
            Emailaddress = c.Emailaddress;

            Accounts = new List<AccountDto>();

            foreach (var item in c.Dispositions)
            {
                Accounts.Add(new AccountDto()
                {
                    AccountId = item.AccountId,
                    Balance = item.Account.Balance,
                    Created = item.Account.Created.ToString("yyyy-MM-dd"),
                    Type = item.Type
                });
            }
        }
    }
}
