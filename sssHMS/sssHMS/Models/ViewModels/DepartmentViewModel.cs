using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public List<Department> Departments { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
