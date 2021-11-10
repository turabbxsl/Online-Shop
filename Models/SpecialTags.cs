using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Models
{
    public class SpecialTags
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Special Tag")]
        public string SpecialTag { get; set; }
    }
}
