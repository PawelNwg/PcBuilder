using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models.ViewModels
{
    public class AddProductViewModel
    {
        [Display(Name = "Nazwa")]
        [MaxLength(50, ErrorMessage = "Nazwa może mieć maksymalnie 50 znaków")]
        [Required(ErrorMessage = "Nazwa jest obowiązkowa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^(\d*\,\d{1,2}|\d+)$", ErrorMessage = "Zły format ceny")]
        public string PriceString { get; set; }

        [Display(Name = "Opis")]
        [MaxLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Kategoria jest wymagana")]
        [Range(1, 99999, ErrorMessage = "Wybierz kategorię")]
        public int CategoryId { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}