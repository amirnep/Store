using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Entities.Users
{
    public class ChangeUsersRoles
    {
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
