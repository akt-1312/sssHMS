using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Medicine
    {
        [Key]
        public int Mdcn_id { get; set; }
        public string Mdcn_name{ get; set; }
        public string company { get; set; }
        public DateTime m_date { get; set; }
        public DateTime e_date { get; set; }
        public double price { get; set; }

    }
}
