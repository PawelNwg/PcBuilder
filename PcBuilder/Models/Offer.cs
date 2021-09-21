using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    [Serializable]
    public class Offer
    {
        public int OfferId { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Tytuł")]
        [MaxLength(50, ErrorMessage = "Tytuł może mieć maksymalnie 50 znaków")]
        [Required(ErrorMessage = "Tytuł jest obowiązkowy")]
        public string Title { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Data wygaśnięcia")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Opis")]
        [MaxLength(500, ErrorMessage = "Tytuł może mieć maksymalnie 50 znaków")]
        [Required(ErrorMessage = "Tytuł jest obowiązkowy")]
        public string Description { get; set; }

        public virtual ICollection<OrderOffers> OrdersOffers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}