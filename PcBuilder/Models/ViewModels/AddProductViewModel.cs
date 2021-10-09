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

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Cena jest obowiązkowa")]
        public decimal Price { get; set; }

        [Display(Name = "Opis")]
        [MaxLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}