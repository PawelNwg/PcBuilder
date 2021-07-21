using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Procent")]
        [Required(ErrorMessage = "Procent jest obowiązkowa")]
        public int Percent { get; set; }

        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }

        public virtual Product Product { get; set; }
    }
}