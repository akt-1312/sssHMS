using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class NationalityViewModel
    {
        public List<Nationality> Nationalities { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
