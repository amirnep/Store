using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities
{
    public class ChangeUserProfile
    {
        [NotMapped]
        public IFormFile Images { get; set; }
    }
}
