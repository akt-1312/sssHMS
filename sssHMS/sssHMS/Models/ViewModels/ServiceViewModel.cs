using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class ServiceViewModel
    {
        public List<Service> Services { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
