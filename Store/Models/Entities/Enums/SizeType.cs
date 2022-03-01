using System.ComponentModel.DataAnnotations;

namespace Store.Models.Entities
{
    public partial class Product
    {
        public enum SizeType
        {
            [Display(Name = "موجود نیست.")]
            Null = 0,
            
            [Display(Name = "S")]
            S = 1,

            [Display(Name = "M")]
            M = 2,

            [Display(Name = "L")]
            L = 3,

            [Display(Name = "XL")]
            XL = 4,

            [Display(Name = "XXL")]
            XXL = 5,

            [Display(Name = "XXXL")]
            XXXL = 6,

            [Display(Name = "XXXXL")]
            XXXXL = 7,

            [Display(Name = "XXXXXL")]
            XXXXXL = 8
        }
    }
}
