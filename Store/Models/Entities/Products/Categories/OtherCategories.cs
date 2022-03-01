using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static Store.Models.Entities.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models.Entities.Products
{
    public class OtherCategories
    {
        [Column(TypeName = "int")]
        public int ID { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int CategoriesID { get; set; }
        public Categories Categories { get; set; }
    }
}
