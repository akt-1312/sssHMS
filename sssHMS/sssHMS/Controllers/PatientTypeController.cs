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
    public class PatientTypeController : Controller
    {
        public readonly ApplicationDbContext db;
        public PatientTypeController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public PatientType PatientType { get; set; }
        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            PatientTypeViewModel PatientTypeVM = new PatientTypeViewModel()
            {
                PatientTypes = new List<Models.PatientType>()

            };
            PatientTypeVM.PatientTypes = await db.PatientTypes.ToListAsync();

            if (searchParam != null)
            {
                PatientTypeVM.PatientTypes = PatientTypeVM.PatientTypes.Where(a => a.PatientTypeName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/PatientType?studentPage=:");

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
            var count = PatientTypeVM.PatientTypes.Count;

            if (sortParam == "SortDec")
            {
                PatientTypeVM.PatientTypes = PatientTypeVM.PatientTypes.OrderByDescending(p => p.PatientTypeName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                PatientTypeVM.PatientTypes = PatientTypeVM.PatientTypes.OrderBy(p => p.PatientTypeName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            PatientTypeVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(PatientTypeVM);
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
                db.PatientTypes.Add(PatientType);
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
            var patientType = await db.PatientTypes.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(patientType);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.PatientTypes where data.PatientTypeID == int.Parse(iD) select data).FirstOrDefault();
                obj.PatientTypeID = int.Parse(iD);
                obj.PatientTypeName = PatientType.PatientTypeName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(PatientType);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var patientType = await db.PatientTypes.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(patientType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.PatientTypes.Remove(PatientType);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(PatientType);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var patientType = await db.PatientTypes.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(patientType);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.PatientTypes where data.PatientTypeID == int.Parse(iD) select data).FirstOrDefault();
                obj.PatientTypeID = int.Parse(iD);
                obj.PatientTypeName = PatientType.PatientTypeName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            //string qs = iD.ToString();

            return View(PatientType);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }
}