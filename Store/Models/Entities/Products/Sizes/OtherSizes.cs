using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities.Products
{
    public class OtherSizes
    {
        [Column(TypeName = "int")]
        public int ID { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int SizesID { get; set; }
        public Sizes Sizes { get; set; }
    }
}
