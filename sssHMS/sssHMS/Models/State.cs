using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "State Name is Required!")]
        [Display(Name = "State")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Select Country!")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public ICollection<Township> Townships { get; set; }
    }
}
