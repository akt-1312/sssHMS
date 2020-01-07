using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class StateViewModel
    {
        public List<State> States { get; set; }
        public PagingInfo PagingInfo { get; set; }
        
    }
}
