using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class sssHMSInfo
    {
        [Key]
        public int sssHMSId { get; set; }

        [Required]
        [Display(Name ="sssHMS Name")]
        public string sssHMSName { get; set; }

        [Required]
        [Display(Name = "CODE")]
        public string sssHMSCode { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string sssHMSAddress { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string sssHMSPhoneNo1 { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string sssHMSPhoneNo2 { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string sssHMSPhoneNo3 { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string sssHMSPhoneNo4 { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string sssHMSEmail { get; set; }

        [Display(Name = "Website")]
        public string sssHMSWebsite { get; set; }

        [Display(Name = "Logo")]
        public string sssHMSImage { get; set; }


    }
}
