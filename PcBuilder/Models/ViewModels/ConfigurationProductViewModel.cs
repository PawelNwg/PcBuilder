using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models.ViewModels
{
    public class ConfigurationProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int SubCategoryId { get; set; }
        public virtual Subcategory Subcategory { get; set; }
    }
}
