using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities.Contact_Us
{
    public class Contact
    {
        [Column(TypeName = "int")]
        public int ID { get; set; }

        [Display(Name = "Subject")]
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Enter Subject.")]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        [Column(TypeName = "nvarchar(Max)")]
        [Required(ErrorMessage = "Write your Message.")]
        public string Message { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        [Display(Name = "Seen")]
        public string Seen { get; set; }
    }
}
