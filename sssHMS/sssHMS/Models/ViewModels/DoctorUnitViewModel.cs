using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class DoctorUnitViewModel
    {
        [Key]
        public int Unitid { get; set; }
        public string Unitname { get; set; }
        public bool IsChecked { get; set; }
    }
}
