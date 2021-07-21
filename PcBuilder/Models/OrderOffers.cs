using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class OrderOffers
    {
        public int OrderOfferId { get; set; }
        public int OfferId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Offer Offer { get; set; }
    }
}