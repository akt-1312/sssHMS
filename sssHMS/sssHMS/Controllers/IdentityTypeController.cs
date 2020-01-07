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
    public class IdentityTypeController : Controller
    {
        private readonly ApplicationDbContext db;
        public IdentityTypeController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public IdentityType IdentityType { get; set; }

        public async Task<IActionResult> Index(string sortParam, string select, string searchParam, string searchParam1, int studentPage = 1, int PageSize = 3)
        {
            IdentityTypeViewModel IdentityTypeVM = new IdentityTypeViewModel()
            {

                IdentityTypes = new List<Models.IdentityType>()
            };


            IdentityTypeVM.IdentityTypes = await db.IdentityTypes.ToListAsync();
            if (IdentityTypeVM.IdentityTypes.Count == 0)
            {
                studentPage = 0;
            }


            if (searchParam != null)
            {

                IdentityTypeVM.IdentityTypes = IdentityTypeVM.IdentityTypes.Where(a => a.IdentityTypeName.ToLower().Contains(searchParam.ToLower())).ToList();

            }



            StringBuilder param = new StringBuilder();
            param.Append("/IdentityType?studentPage=:");


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

            var count = IdentityTypeVM.IdentityTypes.Count;

            if (count < 1)
            {
                studentPage = 0;
            }


            if (sortParam == "SortDec")
            {
                IdentityTypeVM.IdentityTypes = IdentityTypeVM.IdentityTypes.OrderByDescending(p => p.IdentityTypeName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }


            else
            {
                IdentityTypeVM.IdentityTypes = IdentityTypeVM.IdentityTypes.OrderBy(p => p.IdentityTypeName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }



            IdentityTypeVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };



            return View(IdentityTypeVM);
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
                await db.IdentityTypes.AddAsync(IdentityType);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(IdentityType);
        }

        public async Task<IActionResult> Update(int id)
        {
            var identityType = await db.IdentityTypes.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(identityType);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.IdentityTypes.Update(IdentityType);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(IdentityType);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var identityType = await db.IdentityTypes.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(identityType);
        }

        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailPost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                //db.Services.Update(Service);
                //await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(IdentityType);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var identityType = await db.IdentityTypes.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(identityType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var delId = await db.IdentityTypes.FindAsync(id);
                db.IdentityTypes.Remove(delId);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(IdentityType);
        }
    }
}