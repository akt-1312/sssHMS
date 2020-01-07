using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required(ErrorMessage ="Country Name is Required!")]
        [Display(Name = "Country")]
        public string CountryName { get; set; }

        public ICollection<State> States { get; set; }

    }
}
