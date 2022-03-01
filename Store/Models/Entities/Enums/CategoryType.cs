using System.ComponentModel.DataAnnotations;

namespace Store.Models.Entities
{
    public partial class Product
    {
        public enum CategoryType
        {
            [Display(Name = "کالای دیجیتال")]
            Digital_goods = 1,

            [Display(Name = "مد و پوشاک")]
            Fashion_clothing = 2,

            [Display(Name = "اسباب بازی، کودک و نوزاد")]
            Toys_baby_infant = 3,

            [Display(Name = "زیبایی و سلامت")]
            Beauty_health = 4,

            [Display(Name = "کتاب، لوازم التحریر و هنر")]
            Books_stationery_art = 5,

            [Display(Name = "متفرقه")]
            Other = 6
        }
    }
}
