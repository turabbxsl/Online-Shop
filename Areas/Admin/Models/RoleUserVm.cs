using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Areas.Admin.Models
{
    public class RoleUserVm
    {

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }

        [Required]
        [Display(Name ="Role")]
        public string RoleName { get; set; }

    }
}
