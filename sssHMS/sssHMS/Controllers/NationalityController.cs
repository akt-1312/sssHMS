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
    public class NationalityController : Controller
    {
        public readonly ApplicationDbContext db;
        public NationalityController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Nationality Nationality { get; set; }
        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            NationalityViewModel NationalityVM = new NationalityViewModel()
            {
                Nationalities = new List<Models.Nationality>()

            };
            NationalityVM.Nationalities = await db.Nationalities.ToListAsync();

            if (searchParam != null)
            {
                NationalityVM.Nationalities = NationalityVM.Nationalities.Where(a => a.NationalityName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Nationality?studentPage=:");

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
            var count = NationalityVM.Nationalities.Count;

            if (sortParam == "SortDec")
            {
                NationalityVM.Nationalities = NationalityVM.Nationalities.OrderByDescending(p => p.NationalityName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                NationalityVM.Nationalities = NationalityVM.Nationalities.OrderBy(p => p.NationalityName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            NationalityVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(NationalityVM);
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
                db.Nationalities.Add(Nationality);
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
            var gender = await db.Genders.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(gender);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Nationalities where data.NationalityID == int.Parse(iD) select data).FirstOrDefault();
                obj.NationalityID = int.Parse(iD);
                obj.NationalityName = Nationality.NationalityName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(Nationality);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var nationality = await db.Nationalities.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(nationality);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Nationalities.Remove(Nationality);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(Nationality);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var nationality = await db.Nationalities.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(nationality);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Nationalities where data.NationalityID == int.Parse(iD) select data).FirstOrDefault();
                obj.NationalityID = int.Parse(iD);
                obj.NationalityName = Nationality.NationalityName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            //string qs = iD.ToString();

            return View(Nationality);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }
}