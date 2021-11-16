using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class DetailedDataProduct
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public virtual Product  Product { get; set; }
    }
}