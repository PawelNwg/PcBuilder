using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}