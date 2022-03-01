using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities.Products
{
    public class Sizes
    {
        public Sizes()
        {
            OtherSizes = new List<OtherSizes>();
        }

        [Column(TypeName = "int")]
        public int ID { get; set; }

        [Display(Name = "Size")]
        [Required]
        public string Size { get; set; }

        public ICollection<OtherSizes> OtherSizes { get; set; }
    }
}
