using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Ambulance
    {
        [Key]
        public int Amb_id { get; set; }
        public int Amb_num { get; set; }
        public int Capacity { get; set; }
    }
}
