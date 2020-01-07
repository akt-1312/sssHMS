using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int PagerSize {
            get { return 5; }
                
                }
        //public int totalPage => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        public int totalPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }

        }
        public int FirstPage
        {
            get
            {
                return 1;
            }
        }
        public int LastPage
        {
            get
            {
                return totalPage;
            }
        }
        public string urlParam { get; set; }
    }
}
