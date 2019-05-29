using AutoMapper;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace BankApp.Application.CustomerDetails
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Givenname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Streetaddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string CountryCode { get; set; }
        public string Birthday { get; set; }
        public string NationalId { get; set; }
        [Required]
        public string Telephonecountrycode { get; set; }
        [Required]
        public string Telephonenumber { get; set; }
        [Required]
        public string Emailaddress { get; set; }

        public List<AccountDto> Accounts { get; set; }

        public CustomerDetailDto()
        {

        }

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
            Telephonecountrycode = c.Telephonecountrycode;
            Telephonenumber = c.Telephonenumber;
            Emailaddress = c.Emailaddress;

            if (c.Birthday != null)
            {
                Birthday = c.Birthday.Value.ToString("yyyy-MM-dd");
            }
            if(c.NationalId != null)
            {
                NationalId = c.NationalId;
            }


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
