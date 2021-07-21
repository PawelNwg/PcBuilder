using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class Subcategory
    {
        public int SubcategoryId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual Category Category { get; set; }
    }
}