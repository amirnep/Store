using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Store.Models.Entities.Invoices;

namespace Store.Models.Entities.Invoice
{
    public class InvoiceSub
    {
        [Column(TypeName = "int")]
        public int ID { get; set; }

        public int InvoiceHeaderID { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Fee")]
        [Required(ErrorMessage = "Enter Correct Fee.")]
        public float Fee { get; set; }

        [Display(Name = "Mount")]
        [Required(ErrorMessage = "Enter Mount.")]
        public int Mount { get; set; }

        public float Price { get; set; }

        public string DisCount { get; set; }
    }
}
