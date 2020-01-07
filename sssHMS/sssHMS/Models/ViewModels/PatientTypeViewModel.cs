using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class PatientTypeViewModel
    {
        public List<PatientType> PatientTypes { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
