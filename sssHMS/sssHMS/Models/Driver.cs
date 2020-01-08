using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Driver
    {
        [Key]
        public int Dr_id { get; set; }
        public string Dr_name { get; set; }
        public string MOB { get; set; }
        public string Address { get; set; }
        public string Shift { get; set; }
        public double Salary { get; set; }
    }
}
