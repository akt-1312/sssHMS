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
    public class UnitController : Controller
    {
        private readonly ApplicationDbContext db;
        public UnitController(ApplicationDbContext context)
        {
            db = context;
        }
        [BindProperty]
        public Unit Unit { get; set; }

        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            UnitViewModel UnitVM = new UnitViewModel()
            {
                Units = new List<Models.Unit>()


            };
            UnitVM.Units = await db.Units.ToListAsync();

            if (searchParam != null)
            {
                UnitVM.Units = UnitVM.Units.Where(a => a.UnitName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Unit?studentPage=:");

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
            var count = UnitVM.Units.Count;

            if (sortParam == "SortDec")
            {
                UnitVM.Units = UnitVM.Units.OrderByDescending(p => p.UnitName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                UnitVM.Units = UnitVM.Units.OrderBy(p => p.UnitName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            UnitVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(UnitVM);
        }

        public IActionResult Create()
        {
            ViewBag.returnUrl = (Request.Headers["Referer"].ToString());
            List<Department> DepList = new List<Department>();
            DepList = (from a in db.Departments select a).ToList();
            DepList.Insert(0, new Department { DepartmentID = 0, DepartmentName = "Select Departments" });
            ViewBag.DepList = DepList;

            
            
            return View();


        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(int DepartmentID, string returnUrl)
        {

            
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Units.Add(Unit);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            else
            {
                return View();
            }
            
            
         

        }

        public async Task<IActionResult> Update(int ID)
        {
            var unit = await db.Units.FindAsync(ID);
            ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            List<Department> DepList = new List<Department>();
            DepList = (from a in db.Departments select a).ToList();
            DepList.Insert(0, new Department { DepartmentID = 0, DepartmentName = "Select Departments" });
            ViewBag.DepList = DepList;
            return View(unit);
            //return View();
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(int iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Units where data.UnitID == iD select data).FirstOrDefault();
                obj.UnitID = iD;
                obj.UnitName = Unit.UnitName;
                //obj.DepartmentID = Unit.DepartmentID;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            string qs = iD.ToString();

            return View(Unit);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var unit = await db.Units.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            List<Department> DepList = new List<Department>();
            DepList = (from a in db.Departments select a).ToList();
            DepList.Insert(0, new Department { DepartmentID = 0, DepartmentName = "Select Departments" });
            ViewBag.DepList = DepList;
            return View(unit);
           
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Units.Remove(Unit);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }

            //string qs = iD.ToString();

            return View(Unit);
            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Details(int id)
        {
            var unit = await db.Units.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            List<Department> DepList = new List<Department>();
            DepList = (from a in db.Departments select a).ToList();
            DepList.Insert(0, new Department { DepartmentID = 0, DepartmentName = "Select Departments" });
            ViewBag.DepList = DepList;
            return View(unit);
            //return View();
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(int iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Units where data.UnitID == iD select data).FirstOrDefault();
                obj.UnitID = iD;
                obj.UnitName = Unit.UnitName;
                //obj.DepartmentID = Unit.DepartmentID;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            string qs = iD.ToString();

            return View(Unit);


           // return RedirectToAction("Index", new { studentId = iD });
        }
    }
}