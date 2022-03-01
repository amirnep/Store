using Microsoft.AspNetCore.Http;
using Store.Models.Entities.Contact_Us;
using Store.Models.Entities.Invoice;
using Store.Models.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities
{
    public partial class Product
    {
        //Cascade On Delete...
        public Product()
        { 
            OtherColors = new List<OtherColors>();
            OtherSizes = new List<OtherSizes>();
            OtherCategories = new List<OtherCategories>();
            ProductsGallery = new List<ProductsGallery>();
        }

        [Column(TypeName = "int")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter Product Name.")]
        [Display(Name = "Name")]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Product Id.")]
        [Display(Name = "Product Id")]
        [Column(TypeName = "nvarchar(20)")]
        public string ProductCode { get; set; }

        [Display(Name = "Weight")]
        [Column(TypeName = "nvarchar(8)")]
        public string Wieght { get; set; }

        [Display(Name = "Content")]
        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }

        [Display(Name = "Content")]
        [Column(TypeName = "nvarchar(40)")]
        public string Content { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Enter Quantity.")]
        public long Quantity { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Enter Price.")]
        public string Price { get; set; }

        [NotMapped]
        public IFormFile Images { get; set; }

        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        public ICollection<ProductsGallery> ProductsGallery { get; set; }
        public ICollection<OtherColors> OtherColors { get; set; }
        public ICollection<OtherCategories> OtherCategories { get; set; }
        public ICollection<OtherSizes> OtherSizes { get; set; }
        public ICollection<InvoiceSub> InvoiceSub { get; set; }
    }
}
