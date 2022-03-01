using Store.Models.Entities.Invoice;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities.Invoices
{
    public class InvoiceHeader
    {
        [Column(TypeName = "int")]
        public int ID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        [Display(Name = "Date Shamsi")]
        [Column(TypeName = "nvarchar(16)")]
        [Required(ErrorMessage = "Enter Date in Shamsi.")]
        public string Date { get; set; }

        [Display(Name = "Date Miladi")]
        public DateTime DateTime { get; set; }

        public int InvoiceNumber { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "nvarchar(Max)")]
        public string Description { get; set; }

        public ICollection<InvoiceSub> InvoiceSub { get; set; }
    }
}
