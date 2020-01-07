using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sssHMS.Models;
using sssHMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sssHMS.Data;

namespace sssHMS.Controllers
{
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CountryController(ApplicationDbContext context)
        {
            db = context;
        }
        [BindProperty]
        public Country Country { get; set; }

        public async Task<IActionResult> Index(string sortParam, string select, string select1, string searchParam, string searchParam1, int studentPage = 1, int PageSize = 3)
        {
            CountryViewModel CountryVM = new CountryViewModel()
            {

                Countries = new List<Models.Country>()
            };


            CountryVM.Countries = await db.Countries.ToListAsync();
            if(CountryVM.Countries.Count==0)
            {
                studentPage = 0;
            }


            if (searchParam != null)
            {

                CountryVM.Countries = CountryVM.Countries.Where(a => a.CountryName.ToLower().Contains(searchParam.ToLower())).ToList();

            }



            StringBuilder param = new StringBuilder();
            param.Append("/Country?studentPage=:");


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


            var count = CountryVM.Countries.Count;



            if (sortParam == "SortDec")
            {
                CountryVM.Countries = CountryVM.Countries.OrderByDescending(p => p.CountryName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }


            else
            {
                CountryVM.Countries = CountryVM.Countries.OrderBy(p => p.CountryName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }



            CountryVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };



            return View(CountryVM);
        }

        public IActionResult Create()
        {
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                db.Countries.Add(Country);
                await db.SaveChangesAsync();

                //return RedirectToAction("Index");
                return Redirect(returnUrl);
            }
            else
            {

                return View();
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var Country = await db.Countries.FindAsync(id);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(Country);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Countries.Update(Country);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }

            return View(Country);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var country = await db.Countries.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id, string returnUrl)
        {

            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var delId = await db.Countries.FindAsync(id);
                db.Countries.Remove(delId);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }

            return View(Country);
        }

        public async Task<IActionResult> Details(int id)
        {

            var country = await db.Countries.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(country);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                return Redirect(returnUrl);
            }

            return View(Country);


        }
    }
}