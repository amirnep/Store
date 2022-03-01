using System.ComponentModel.DataAnnotations;

namespace Store.Models.Entities
{
    public partial class Product
    {
        public enum ColorType
        {
            [Display(Name = "آبی")]
            Blue = 1,

            [Display(Name = "سبز")]
            Green = 2,

            [Display(Name = "زرد")]
            Yellow = 3,

            [Display(Name = "نارنجی")]
            Orange = 4,

            [Display(Name = "قرمز")]
            Red = 5,

            [Display(Name = "سفید")]
            White = 6,

            [Display(Name = "صورتی")]
            Violet = 7,

            [Display(Name = "قهوه ای")]
            Brown = 8,

            [Display(Name = "هوایی")]
            Aqua = 9,

            [Display(Name = "سیاه")]
            Black = 10,

            [Display(Name = "نیلی")]
            NavyBlue = 11,

            [Display(Name = "بنفش")]
            Purple = 12
        }
    }
}
