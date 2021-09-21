using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Cart
{
    [Serializable]
    public class CartPosition
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public decimal sum { get; set; }
    }
}