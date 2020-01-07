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
    public class DepartmentController : Controller
    {
        public readonly ApplicationDbContext db;
        public DepartmentController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Department Department { get; set; }
        public async Task<IActionResult> Index(string sortParam, string searchParam, int studentPage = 1, int PageSize = 9)
        {
            DepartmentViewModel DepartmentVM = new DepartmentViewModel()
            {
                Departments = new List<Models.Department>()

            };
            DepartmentVM.Departments = await db.Departments.ToListAsync();

            if (searchParam != null)
            {
                DepartmentVM.Departments = DepartmentVM.Departments.Where(a => a.DepartmentName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Department?studentPage=:");

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
            var count = DepartmentVM.Departments.Count;

            if (sortParam == "SortDec")
            {
                DepartmentVM.Departments = DepartmentVM.Departments.OrderByDescending(p => p.DepartmentName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                DepartmentVM.Departments = DepartmentVM.Departments.OrderBy(p => p.DepartmentName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            DepartmentVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(DepartmentVM);
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
                db.Departments.Add(Department);
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
                var obj = (from data in db.Departments where data.DepartmentID == int.Parse(iD) select data).FirstOrDefault();
                obj.DepartmentID = int.Parse(iD);
                obj.DepartmentName = Department.DepartmentName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(Department);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await db.Departments.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Departments.Remove(Department);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(Department);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var department = await db.Departments.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(department);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Departments where data.DepartmentID == int.Parse(iD) select data).FirstOrDefault();
                obj.DepartmentID = int.Parse(iD);
                obj.DepartmentName = Department.DepartmentName;

                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }


            //string qs = iD.ToString();

            return View(Department);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }
}