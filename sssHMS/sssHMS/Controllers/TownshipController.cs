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
    public class TownshipController : Controller
    {
        private readonly ApplicationDbContext db;
        public TownshipController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Township Township { get; set; }

        public async Task<IActionResult> Index(int StateId,int CountryId, string sortParam, string select, string btnSearch, string searchParam, string searchParam1, int studentPage = 1, int PageSize = 3)
        {
            TownshipViewModel TownshipVM = new TownshipViewModel()
            {

                Townships = new List<Models.Township>()

            };

            FillCountries(CountryId);

            FillStates(CountryId);
            if (CountryId==0 || ViewBag.States.Count<=1)
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
            param.Append("/Township?studentPage=:");
          

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
            if(count==0)
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
        private void FillCountries2(int CountryId = 0)
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Insert(0, new SelectListItem { Value = "", Text = "Select Country" });
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
        private void FillStates2(int CountryId = 0)
        {

            List<SelectListItem> states = new List<SelectListItem>();
            states.Insert(0, new SelectListItem { Value = "", Text = "Select State" });

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
        public IActionResult GetStatesUpdate(int CountryId,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            FillCountries2(CountryId);
            FillStates2(CountryId);
            ModelState.ClearValidationState("");
            return View("Update");
        }        

        public IActionResult GetStates(int CountryId,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            FillCountries2(CountryId);
            FillStates2(CountryId);
            ModelState.ClearValidationState("");
            return View("Create");
        }

        
        public IActionResult Create()
        {

            FillCountries2();

            FillStates2();
            ViewBag.StateEnabled = false;
            ViewBag.returnUrl = Request.Headers["referer"].ToString();

            return View(Township);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(int CountryId, int StateId, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                //ViewBag.returnUrl = returnUrl;
                db.Townships.Add(Township);
                await db.SaveChangesAsync();

                //return RedirectToAction("Index");
                return Redirect(returnUrl);
            }
            else
            {
                ViewBag.StateEnabled = true;
                FillCountries2(CountryId);
                FillStates2(CountryId);
                return View();
            }

        }
        public async Task<IActionResult> Update(int id)
        {
            var a = db.Townships.Where(ax=>ax.TownshipId==id).FirstOrDefault();
            int b = a.StateId;
            var c = db.States.Where(ay => ay.StateId == b).FirstOrDefault();
            var d = c.CountryId;

            FillCountries2(d);
            FillStates2(d);

            var township = await db.Townships.FindAsync(id);

            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(township);
        }
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(string returnUrl, int CountryId, int StateId)
        {
            ViewBag.returnUrl = returnUrl;
            if (CountryId == 0)
            {
                ViewBag.ErrorMessage = "Please Select Country!";
            }
            if (StateId == 0)
            {
                ViewBag.ErrorMessage1 = "Please Select State!";
            }

            if (ModelState.IsValid)
            {
                db.Townships.Update(Township);
                await db.SaveChangesAsync();

                return Redirect(returnUrl);
            }
            else
            {
                FillCountries2(CountryId);
                FillStates2(CountryId);
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var a = db.Townships.Where(ax => ax.TownshipId == id).FirstOrDefault();
            int b = a.StateId;
            var c = db.States.Where(ay => ay.StateId == b).FirstOrDefault();
            var d = c.CountryId;

            FillCountries2(d);
            FillStates2(d);

            var township = await db.Townships.FindAsync(id);



            //ViewBag.StateEnabled = false;
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(township);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var delId = await db.Townships.FindAsync(id);
                db.Townships.Remove(delId);
                await db.SaveChangesAsync();

                return Redirect(returnUrl);
            }

            return View(Township);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var a = db.Townships.Where(ax => ax.TownshipId == id).FirstOrDefault();
            int b = a.StateId;
            var c = db.States.Where(ay => ay.StateId == b).FirstOrDefault();
            var d = c.CountryId;

            FillCountries2(d);
            FillStates2(d);

            var township = await db.Townships.FindAsync(id);



            //ViewBag.StateEnabled = false;
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(township);
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

            return View(Township);
        }

    }
}