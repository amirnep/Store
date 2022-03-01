using Microsoft.AspNetCore.Http;
using Store.Models.Entities.Contact_Us;
using Store.Models.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities
{
    public class User
    {
        //Cascade On Delete...
        public User()
        {
            Contact = new List<Contact>();
        }

        [Column(TypeName = "int")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter Your Name.")]
        [Display(Name = "Name")]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Display(Name = "Phone")]
        [Column(TypeName = "nvarchar(13)")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your Email for login.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Your Password.")]

        [NotMapped]
        public IFormFile Images { get; set; }

        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Your Password.")]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Your Password Confirm.")]
        [Compare("Password", ErrorMessage = "Passwords does not match.")]
        public string ConfirmPassword { get; set; }

        public ICollection<Contact> Contact { get; set; }
        public ICollection<InvoiceHeader> InvoiceHeader { get; set; }
    }
}
