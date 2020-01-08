using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class sssHMSInfoViewModel
    {
        public List<sssHMSInfo> sssHMSInfos { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
