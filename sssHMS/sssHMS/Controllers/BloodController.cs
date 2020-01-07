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
    public class BloodController : Controller
    {
        private readonly ApplicationDbContext db;

        public BloodController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Blood Blood { get; set; }


        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            BloodViewModel BloodVM = new BloodViewModel()
            {
                Bloods = new List<Models.Blood>()
            };
            BloodVM.Bloods = await db.Bloods.ToListAsync();

            if (searchParam != null)
            {
                BloodVM.Bloods = BloodVM.Bloods.Where(a => a.BloodType.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Blood?studentPage=:");

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
            var count = BloodVM.Bloods.Count;

            if (sortParam == "SortDec")
            {
                BloodVM.Bloods = BloodVM.Bloods.OrderByDescending(p => p.BloodType)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                BloodVM.Bloods = BloodVM.Bloods.OrderBy(p => p.BloodType)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            BloodVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(BloodVM);
        }

        public IActionResult Create()
        {
            ViewBag.returnUrl = (Request.Headers["Referer"].ToString());
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                db.Bloods.Add(Blood);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            var blood = await db.Bloods.FindAsync(id);
            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(blood);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Bloods where data.BloodID == int.Parse(iD) select data).FirstOrDefault();
                obj.BloodID = int.Parse(iD);
                obj.BloodType = Blood.BloodType;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(Blood);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var blood = await db.Bloods.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(blood);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Bloods.Remove(Blood);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(Blood);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var blood = await db.Bloods.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(blood);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Bloods where data.BloodID == int.Parse(iD) select data).FirstOrDefault();
                obj.BloodID = int.Parse(iD);
                obj.BloodType = Blood.BloodType;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            //string qs = iD.ToString();

            return View(Blood);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }
}