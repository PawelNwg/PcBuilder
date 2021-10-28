using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    [Serializable]
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name = "Nazwa")]
        [MaxLength(50, ErrorMessage = "Nazwa może mieć maksymalnie 50 znaków")]
        [Required(ErrorMessage = "Nazwa jest obowiązkowa")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Cena jest obowiązkowa")]
        public decimal Price { get; set; }

        [Display(Name = "Opis")]
        [MaxLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków")]
        public string Description { get; set; }

        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        public int SubCategoryId { get; set; }

        public string File { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public virtual ICollection<DetailedDataProduct> DetailedDataProducts { get; set; }
    }
}