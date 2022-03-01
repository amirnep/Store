using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities.Products
{
    public class Colors
    {
        public Colors()
        {
            OtherColors = new List<OtherColors>();
        }

        [Column(TypeName = "int")]
        public int ID { get; set; }

        [Display(Name = "Color")]
        [Required]
        public string Color { get; set; }

        public ICollection<OtherColors> OtherColors { get; set; }
    }
}
