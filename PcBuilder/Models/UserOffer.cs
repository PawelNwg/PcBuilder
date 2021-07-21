using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class UserOffer : Offer
    {
        public int UserOfferId { get; set; }
        public int UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}