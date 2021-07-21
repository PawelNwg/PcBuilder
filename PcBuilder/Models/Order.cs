using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Data złożenia")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public virtual ICollection<OrderOffers> OrdersOffers { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}