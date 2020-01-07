using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class HospitalInfo
    {
        [Key]
        public int HospitalId { get; set; }

        [Required]
        [Display(Name ="Hospital Name")]
        public string HospitalName { get; set; }

        [Required]
        [Display(Name = "CODE")]
        public string HospitalCode { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string HospitalAddress { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string HospitalPhoneNo1 { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string HospitalPhoneNo2 { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string HospitalPhoneNo3 { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string HospitalPhoneNo4 { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string HospitalEmail { get; set; }

        [Display(Name = "Website")]
        public string HospitalWebsite { get; set; }

        [Display(Name = "Logo")]
        public string HospitalImage { get; set; }


    }
}
