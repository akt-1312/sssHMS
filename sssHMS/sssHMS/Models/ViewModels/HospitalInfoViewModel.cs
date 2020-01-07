using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class HospitalInfoViewModel
    {
        public List<HospitalInfo> HospitalInfos { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
