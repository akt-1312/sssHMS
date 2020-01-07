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
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext db;

        public StatusController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Status Status { get; set; }


        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            StatusViewModel StatusVM = new StatusViewModel()
            {
                Statuses = new List<Models.Status>()
            };
            StatusVM.Statuses = await db.Statuses.ToListAsync();

            if (searchParam != null)
            {
                StatusVM.Statuses = StatusVM.Statuses.Where(a => a.StatusName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Status?studentPage=:");

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
            var count = StatusVM.Statuses.Count;

            if (sortParam == "SortDec")
            {
                StatusVM.Statuses = StatusVM.Statuses.OrderByDescending(p => p.StatusName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                StatusVM.Statuses = StatusVM.Statuses.OrderBy(p => p.StatusName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            StatusVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(StatusVM);
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
                db.Statuses.Add(Status);
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
            var status = await db.Statuses.FindAsync(id);
            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(status);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Statuses where data.StatusID == int.Parse(iD) select data).FirstOrDefault();
                obj.StatusID = int.Parse(iD);
                obj.StatusName = Status.StatusName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(Status);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var status = await db.Statuses.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(status);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Statuses.Remove(Status);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(Status);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var status = await db.Statuses.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(status);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Statuses where data.StatusID == int.Parse(iD) select data).FirstOrDefault();
                obj.StatusID = int.Parse(iD);
                obj.StatusName = Status.StatusName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            //string qs = iD.ToString();

            return View(Status);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }
}