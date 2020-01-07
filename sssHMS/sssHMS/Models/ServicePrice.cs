using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class ServicePrice
    {
        [Key]
        public int ServicePriceID { get; set; }

        [Display(Name ="Service Name")]
        public int ServiceID { get; set; }

        [ForeignKey("ServiceID")]
        public Service Service { get; set; }

        [Display(Name ="Local Price")]
        public double? LocalPrice { get; set; }

        [Display(Name = "Formula Rate")]
        public double? ForeignerFormulaPrice { get; set; }

        [Display(Name ="Fixed Rate")]
        public double? ForeignerFixedPrice { get; set; }


    }
}
