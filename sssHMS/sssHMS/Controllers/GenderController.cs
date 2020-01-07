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
    public class GenderController : Controller
    {
        public readonly ApplicationDbContext db;
        public GenderController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Gender Gender { get; set; }
        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            GenderViewModel GenderVM = new GenderViewModel()
            {
                Genders = new List<Models.Gender>()

            };
            GenderVM.Genders = await db.Genders.ToListAsync();

            if (searchParam != null)
            {
                GenderVM.Genders = GenderVM.Genders.Where(a => a.GenderName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Gender?studentPage=:");

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
            var count = GenderVM.Genders.Count;

            if (sortParam == "SortDec")
            {
                GenderVM.Genders = GenderVM.Genders.OrderByDescending(p => p.GenderName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                GenderVM.Genders = GenderVM.Genders.OrderBy(p => p.GenderName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            GenderVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(GenderVM);
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
                db.Genders.Add(Gender);
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
                var obj = (from data in db.Genders where data.GenderID == int.Parse(iD) select data).FirstOrDefault();
                obj.GenderID = int.Parse(iD);
                obj.GenderName = Gender.GenderName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(Gender);


            //return RedirectToAction("Index", new { studentId = iD });
        }


        public async Task<IActionResult> Delete(int id)
        {
            var gender = await db.Genders.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(gender);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Genders.Remove(Gender);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(Gender);


            //return RedirectToAction("Index", new { studentId = iD });
        }

       

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var gender = await db.Genders.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(gender);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Genders where data.GenderID == int.Parse(iD) select data).FirstOrDefault();
                obj.GenderID = int.Parse(iD);
                obj.GenderName = Gender.GenderName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            //string qs = iD.ToString();

            return View(Gender);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }
}
