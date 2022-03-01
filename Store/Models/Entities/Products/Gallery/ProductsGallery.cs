using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities
{
    public class ProductsGallery
    {
        [Column(TypeName = "int")]
        public int ID { get; set; }

        [NotMapped]
        public IFormFile Images { get; set; }

        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
