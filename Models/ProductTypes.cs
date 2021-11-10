using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Models
{
    public class ProductTypes
    {
        public int ID { get; set; }

        [Required]
        [Display(Name ="Product Type")]
        public string ProductType { get; set; }
    }
}
