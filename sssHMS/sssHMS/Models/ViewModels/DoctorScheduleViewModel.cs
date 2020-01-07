using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class DoctorScheduleViewModel
    {
        public List<DoctorSchedule> DoctorSchedules { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
