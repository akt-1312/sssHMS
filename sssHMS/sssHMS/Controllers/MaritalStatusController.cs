using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sssHMS.Data;
using sssHMS.Models;
using sssHMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace sssHMS.Controllers
{
    public class MaritalStatusController : Controller
    {
        public ApplicationDbContext db;

        public MaritalStatusController(ApplicationDbContext context)
        {
            db = context;
        }
        [BindProperty]
        public MaritalStatus MaritalStatus { get; set; }
        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            MaritalStatusViewModel MaritalStatusVM = new MaritalStatusViewModel()
            {
                MaritalStatuses = new List<Models.MaritalStatus>()

            };
            MaritalStatusVM.MaritalStatuses = await db.MaritalStatuses.ToListAsync();

            if (searchParam != null)
            {
                MaritalStatusVM.MaritalStatuses = MaritalStatusVM.MaritalStatuses.Where(a => a.MaritalStatusName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/MartialStatus?studentPage=:");

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
            var count = MaritalStatusVM.MaritalStatuses.Count;

            if (sortParam == "SortDec")
            {
                MaritalStatusVM.MaritalStatuses = MaritalStatusVM.MaritalStatuses.OrderByDescending(p => p.MaritalStatusName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                MaritalStatusVM.MaritalStatuses = MaritalStatusVM.MaritalStatuses.OrderBy(p => p.MaritalStatusName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            MaritalStatusVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(MaritalStatusVM);
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
                db.MaritalStatuses.Add(MaritalStatus);
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
            var martialstauts = await db.MaritalStatuses.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(martialstauts);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.MaritalStatuses where data.MaritalStatusID == int.Parse(iD) select data).FirstOrDefault();
                obj.MaritalStatusID = int.Parse(iD);
                obj.MaritalStatusName = MaritalStatus.MaritalStatusName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(MaritalStatus);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var maritalStatus = await db.MaritalStatuses.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(maritalStatus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.MaritalStatuses.Remove(MaritalStatus);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(MaritalStatus);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var martialstatus = await db.MaritalStatuses.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(martialstatus);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.MaritalStatuses where data.MaritalStatusID == int.Parse(iD) select data).FirstOrDefault();
                obj.MaritalStatusID = int.Parse(iD);
                obj.MaritalStatusName = MaritalStatus.MaritalStatusName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            //string qs = iD.ToString();

            return View(MaritalStatus);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }

}
