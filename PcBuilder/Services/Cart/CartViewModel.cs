using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Cart
{
    public class CartViewModel
    {
        public List<CartPosition> CartPositions { get; set; }
        public decimal TotalPrice { get; set; }
    }
}