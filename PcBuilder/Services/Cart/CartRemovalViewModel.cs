using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Cart
{
    public class CartRemovalViewModel
    {
        public decimal CartTotalPrice { get; set; }
        public int CartSize { get; set; }
        public int ProductIdToRemove { get; set; }
        public int ProductsCountToRemove { get; set; }
    }
}