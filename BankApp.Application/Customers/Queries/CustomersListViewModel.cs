using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Customers.Queries
{
    public class CustomersListViewModel
    {
        public List<CustomerListedDto> Customers { get; set; } = new List<CustomerListedDto>();
        public bool CanShowMore { get; set; } 
        public int PageNumber { get; set; }
        public string SearchName { get; set; }
        public string SearchCity { get; set; }

        public CustomersListViewModel()
        {

        }

        public CustomersListViewModel(List<Customer> CustomerList, bool canshowmore, int pagenr, string name, string city)
        {
            foreach (var item in CustomerList)
            {
                Customers.Add(new CustomerListedDto(item));
            }

            CanShowMore = canshowmore;
            PageNumber = pagenr;
            SearchName = name;
            SearchCity = city;

        }
    }
}
