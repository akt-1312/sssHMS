using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Township
    {
        [Key]
        public int TownshipId { get; set; }
        [Required(ErrorMessage = "Township Name is Required!")]        
        [Display(Name = "Township")]
        public string TownshipName { get; set; }

        [Required(ErrorMessage = "Please Select State!")]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

    }
}
