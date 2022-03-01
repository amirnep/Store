using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Store.Models.Entities.Product;

namespace Store.Models.Entities.Products
{
    public class Categories
    {
        public Categories()
        {
            OtherCategories = new List<OtherCategories>();
        }

        [Column(TypeName = "int")]
        public int ID { get; set; }

        [Display(Name = "Category")]
        [Required]
        public string Category { get; set; }

        public ICollection<OtherCategories> OtherCategories { get; set; }
    }
}
