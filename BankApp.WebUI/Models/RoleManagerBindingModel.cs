using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.WebUI.Models
{
    public class RoleManagerBindingModel
    {
        [Required]
        public string Email { get; set; }

        public bool Regular { get; set; }
        public bool Cashier { get; set; }
        public bool Admin { get; set; }
    }
}
