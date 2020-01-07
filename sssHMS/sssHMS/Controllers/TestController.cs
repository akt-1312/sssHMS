using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sssHMS.Data;
using sssHMS.Models;
using sssHMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace sssHMS.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext db;
        public TestController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Township Township { get; set; }

        public async Task<IActionResult> Index(int StateId, int CountryId, string sortParam, string select, string searchParam, string searchParam1, int studentPage = 1, int PageSize = 3)
         {
            TownshipViewModel TownshipVM = new TownshipViewModel()
            {

                Townships = new List<Models.Township>()

            };

            FillCountries(CountryId);

            FillStates(CountryId);
            if (CountryId == 0 || ViewBag.States.Count<=1)
            {
                ViewBag.StateEnabled = false;
                StateId = 0;
            }

            TownshipVM.Townships = await db.Townships.ToListAsync();

            if (StateId != 0)
            {
                ViewBag.selectStateId = "StateId";
            }

            if (searchParam == null)
            {
                if (CountryId != 0 && StateId == 0)
                {
                    TownshipVM.Townships = (db.Townships.Where(a => a.State.CountryId == CountryId).ToList());
                }
                if (CountryId == 0 && StateId != 0)
                {
                    TownshipVM.Townships = (db.Townships.Where(a => a.StateId == StateId).ToList());
                }
                if (CountryId != 0 && StateId != 0)
                {
                    TownshipVM.Townships = (db.Townships.Where(a => a.State.CountryId == CountryId).ToList()).Where(b => b.StateId == StateId).ToList();
                }
            }
            else
            {
                if (CountryId != 0 && StateId == 0)
                {
                    TownshipVM.Townships = (db.Townships.Where(a => a.State.CountryId == CountryId).ToList()).Where(b => b.TownshipName.ToLower().Contains(searchParam.ToLower())).ToList();
                    if (TownshipVM.Townships.Count == 0)
                    {
                        studentPage = 0;
                    }
                }
                if (CountryId != 0 && StateId != 0)
                {
                    TownshipVM.Townships = ((db.Townships.Where(a => a.State.CountryId == CountryId).ToList()).Where(b => b.StateId == StateId).ToList()).Where(b => b.TownshipName.ToLower().Contains(searchParam.ToLower())).ToList();
                    if (TownshipVM.Townships.Count == 0)
                    {
                        studentPage = 0;
                    }
                }
                if (CountryId == 0 && StateId == 0)
                {
                    TownshipVM.Townships = db.Townships.ToList().Where(b => b.TownshipName.ToLower().Contains(searchParam.ToLower())).ToList();
                }
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Test?studentPage=:");


            param.Append("&select=");
            if (select != null)
            {
                param.Append(select);
                ViewBag.selectParam = select;

            }
            param.Append("&CountryId=");
            if (CountryId != 0)
            {
                param.Append(CountryId);
            }

            param.Append("&StateId=");
            if (StateId != 0)
            {
                param.Append(StateId);
            }

            param.Append("&searchParam=");
            if (searchParam != null)
            {
                param.Append(searchParam);

            }


            param.Append("&sortParam=");

            if (sortParam != null)
            {
                param.Append(sortParam);
            }
            //param.Append("&CountryId=");
            //if (CountryId != 0)
            //{
            //    param.Append(CountryId);
            //}

            if (PageSize <= 0)
            {
                PageSize = 9;
            }

            ViewBag.PageSize = PageSize;


            param.Append("&PageSize=");
            if (PageSize != 0)
            {
                param.Append(PageSize);
            }


            var count = TownshipVM.Townships.Count;
            if (count == 0)
            {
                studentPage = 0;
            }


            if (sortParam == "SortDec")
            {
                TownshipVM.Townships = TownshipVM.Townships.OrderByDescending(p => p.TownshipName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }


            else
            {
                TownshipVM.Townships = TownshipVM.Townships.OrderBy(p => p.TownshipName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }



            TownshipVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };

            //if(country!=0)
            //{
            //    return View("Index");
            //}

            return View(TownshipVM);
        }

        private void FillCountries(int CountryId = 0)
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Insert(0, new SelectListItem { Value = "0", Text = "Select Country" });
            foreach (var temp in db.Countries)
            {

                countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
            }

            SelectListItem selectedItem = (from i in countries
                                           where i.Value == CountryId.ToString()
                                           select i).SingleOrDefault();
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }

            ViewBag.Countries = countries;

        }
        private void FillStates(int CountryId = 0)
        {

            List<SelectListItem> states = new List<SelectListItem>();
            states.Insert(0, new SelectListItem { Value = "0", Text = "Select State" });

            var obj = (from data in db.States where data.CountryId == CountryId select data).ToList();
            foreach (var item in obj)
            {
                states.Add(new SelectListItem()
                {
                    Text = item.StateName,
                    Value = item.StateId.ToString()
                });
            }


            ViewBag.States = states;           

        }
    }
}