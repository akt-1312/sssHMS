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
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext db;
        public ServiceController (ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Service Service { get; set; }

        public async Task<IActionResult> Index(string sortParam, string select, string searchParam, string searchParam1, int studentPage = 1, int PageSize = 3)
        {
            ServiceViewModel ServiceVM = new ServiceViewModel()
            {

                Services = new List<Models.Service>()
            };


            ServiceVM.Services = await db.Services.ToListAsync();
            if (ServiceVM.Services.Count == 0)
            {
                studentPage = 0;
            }


            if (searchParam != null)
            {

                ServiceVM.Services = ServiceVM.Services.Where(a => a.ServiceName.ToLower().Contains(searchParam.ToLower())).ToList();

            }



            StringBuilder param = new StringBuilder();
            param.Append("/Service?studentPage=:");


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

            var count = ServiceVM.Services.Count;

            if (count < 1)
            {
                studentPage = 0;
            }


            if (sortParam == "SortDec")
            {
                ServiceVM.Services = ServiceVM.Services.OrderByDescending(p => p.ServiceName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }


            else
            {
                ServiceVM.Services = ServiceVM.Services.OrderBy(p => p.ServiceName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            

            ServiceVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };



            return View(ServiceVM);
        }
        
        public IActionResult Create()
        {
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View();
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if(ModelState.IsValid)
            {
                await db.Services.AddAsync(Service);
                await db.SaveChangesAsync();
                
            }
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Update(int id)
        {
            var sercice = await db.Services.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(sercice);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Services.Update(Service);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(Service);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var sercice = await db.Services.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(sercice);
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
            return View(Service);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var sercice = await db.Services.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(sercice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var delId = db.Services.Find(id);
                db.Services.Remove(delId);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(Service);
        }

    }
}