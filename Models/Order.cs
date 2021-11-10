using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Models
{
    public class Order
    {

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public int ID { get; set; }

        [Display(Name = "Order No")]
        public string OrderNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Phone No")]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }


    }
}
