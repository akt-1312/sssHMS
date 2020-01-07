using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class BloodViewModel
    {
        public List<Blood> Bloods { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
