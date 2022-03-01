using Store.Models.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Store.Models.Entities.Product;

namespace Store.Models.Entities
{
    public class OtherColors
    {
        [Column(TypeName = "int")]
        public int ID { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int ColorsID { get; set; }
        public Colors Colors { get; set; }
    }
}
