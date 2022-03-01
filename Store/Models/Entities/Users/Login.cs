using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities
{
    public class Login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your Email for login.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Your Password.")]
        public string Password { get; set; }
    }
}
