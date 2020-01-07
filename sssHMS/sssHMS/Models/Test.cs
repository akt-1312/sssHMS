using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Test
    {
        [Key]
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}
