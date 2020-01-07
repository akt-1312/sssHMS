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

namespace sssHMS.Controllers
{
    public class StateController : Controller
    {
        private readonly ApplicationDbContext db;
        public StateController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public State State { get; set; }

        public async Task<IActionResult> Index(int CountryId, string sortParam, string select, string select1, string searchParam, string searchParam1, int studentPage = 1, int PageSize = 3)
        {
            StateViewModel StateVM = new StateViewModel()
            {

                States = new List<Models.State>()

            };
            //List<Country> lstState = new List<Country>();
            //lstState = (from data in db.Countries select data).ToList();
            //lstState.Insert(0, new Country { CountryId = 0, CountryName = "Select Country" });
            //ViewBag.ListOfCountry = lstState;
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Insert(0, new SelectListItem { Value = "0", Text = "Select Country" });
            foreach (var temp in db.Countries)
            {

                countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
            }
            ViewBag.Countries = countries;

            StateVM.States = await db.States.ToListAsync();

            if (CountryId != 0)
            {
                ViewBag.selectCountryId = "CountryId";
            }
            if (CountryId != 0 && searchParam == null)
            {
                StateVM.States = db.States.Where(a => a.CountryId == CountryId).ToList();
                if (StateVM.States.Count == 0)
                {
                    studentPage = 0;
                }

            }

            if (CountryId == 0 && searchParam != null)
            {
                StateVM.States = StateVM.States.Where(a => a.StateName.ToLower().Contains(searchParam.ToLower())).ToList();
                if (StateVM.States.Count == 0)
                {
                    studentPage = 0;
                }
            }
            if (CountryId != 0 && searchParam == null)
            {
                StateVM.States = StateVM.States.Where(a => a.CountryId == CountryId).ToList();
                if (StateVM.States.Count == 0)
                {
                    studentPage = 0;
                }
            }
            if (CountryId != 0 && searchParam != null)
            {
                StateVM.States = (StateVM.States.Where(a => a.CountryId == CountryId).ToList()).Where(b => b.StateName.ToLower().Contains(searchParam.ToLower())).ToList();
            }
            StringBuilder param = new StringBuilder();
            param.Append("/State?studentPage=:");


            param.Append("&select=");
            if (select != null)
            {
                param.Append(select);
                ViewBag.selectParam = select;

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
            param.Append("&CountryId=");
            if (CountryId != 0)
            {
                param.Append(CountryId);
            }

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


            var count = StateVM.States.Count;

            if(count==0)
            {
                studentPage = 0;
            }

            if (sortParam == "SortDec")
            {
                StateVM.States = StateVM.States.OrderByDescending(p => p.StateName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }


            else
            {
                StateVM.States = StateVM.States.OrderBy(p => p.StateName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }



            StateVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };



            return View(StateVM);
        }


        public IActionResult Create()
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Insert(0, new SelectListItem { Value = "", Text = "Select Country" });
            foreach (var temp in db.Countries)
            {
                countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
            }
            ViewBag.Countries = countries;
                              

            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(int CountryId, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;


            if (ModelState.IsValid)
            {
                db.States.Add(State);
                await db.SaveChangesAsync();



                //return RedirectToAction("Index");
                return Redirect(returnUrl);

            }
            else
            {
                List<SelectListItem> countries = new List<SelectListItem>();
                countries.Insert(0, new SelectListItem { Value = "", Text = "Select Country" });
                foreach (var temp in db.Countries)
                {
                    countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
                }
                ViewBag.Countries = countries;
                return View();
            }
            
            //return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var state = await db.States.FindAsync(id);
            //string stName = state.Country.CountryName;

            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Insert(0, new SelectListItem { Value = "", Text = "Select Country" });
            foreach (var temp in db.Countries)
            {

                countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
            }
            ViewBag.Countries = countries;
           

            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(state);
        }
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                db.States.Update(State);
                await db.SaveChangesAsync();

                //return RedirectToAction("Index");
                return Redirect(returnUrl);
            }
            else
            {
                List<SelectListItem> countries = new List<SelectListItem>();
                countries.Insert(0, new SelectListItem { Value = "", Text = "Select Country" });
                foreach (var temp in db.Countries)
                {

                    countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
                }
                ViewBag.Countries = countries;
                return View();
            }
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var state = await db.States.FindAsync(id);

            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Insert(0, new SelectListItem { Value = "", Text = "Select Country" });
            foreach (var temp in db.Countries)
            {

                countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
            }
            ViewBag.Countries = countries;


            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(state);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var delId = await db.States.FindAsync(id);
                db.States.Remove(delId);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }

            return Redirect(returnUrl);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var state = await db.States.FindAsync(id);

            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Insert(0, new SelectListItem { Value = "", Text = "Select Country" });
            foreach (var temp in db.Countries)
            {

                countries.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });
            }
            ViewBag.Countries = countries;
          

            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(state);
        }
        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailPost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                return Redirect(returnUrl);
            }

            return View(State);
        }
    }
}