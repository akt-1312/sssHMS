using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class ServicePriceViewModel
    {
        public List<ServicePrice> ServicePrices { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
