using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class AppUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        [EmailAddress(ErrorMessage = "aaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [Required]
        [MaxLength(20, ErrorMessage = "Imię może mieć maksymalnie 20 znaków")]
        [Display(Name = "Imię")]
        [RegularExpression(@"^([A-Za-zzżźćńółęąśŻŹĆĄŚĘŁÓŃ]+)$", ErrorMessage = "Niepoprawne imię")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków")]
        [Display(Name = "Nazwisko")]
        [RegularExpression(@"^([A-Za-zzżźćńółęąśŻŹĆĄŚĘŁÓŃ]+)$", ErrorMessage = "Niepoprawne nazwisko")]
        public string Surname { get; set; }

        [MaxLength(50, ErrorMessage = "Miasto może mieć maksymalnie 50 znaków")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [MaxLength(50, ErrorMessage = "Ulica może mieć maksymalnie 50 znaków")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [MaxLength(13, ErrorMessage = "Telefon może mieć maksymalnie 13 znaków")]
        [Display(Name = "Numer Telefonu")]
        public string Phone { get; set; }

        [Display(Name = "Numer budynku")]
        public string BuldingNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserOffer> UserOffers { get; set; }
    }
}