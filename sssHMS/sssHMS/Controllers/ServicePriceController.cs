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
    public class ServicePriceController : Controller
    {
        private readonly ApplicationDbContext db;
        public ServicePriceController (ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public ServicePrice ServicePrice { get; set; }

        public async Task<IActionResult> Index(string sortParam, int searchField, string searchParam, int studentPage = 1, int PageSize = 3)
        {
            ServicePriceViewModel ServicePriceVM = new ServicePriceViewModel()
            {

                ServicePrices = new List<Models.ServicePrice>()

            };
            if (searchField != 0)
            {
                ViewBag.DropDownColor = "abc";
            }

            FillServiceIndex(searchField);

            ServicePriceVM.ServicePrices = await db.ServicePrices.ToListAsync();

            if(searchField!=0)
            {
                ServicePriceVM.ServicePrices = db.ServicePrices.Where(a => a.ServiceID == searchField).ToList();
            }


            StringBuilder param = new StringBuilder();
            param.Append("/ServicePrice?studentPage=:");


            param.Append("&searchField=");
            if (searchField != 0)
            {
                param.Append(searchField);
            }

            param.Append("&sortParam=");
            if (sortParam != null)
            {
                param.Append(sortParam);
            }
           

            if (PageSize <= 0)
            {
                PageSize = 3;
            }

            ViewBag.PageSize = PageSize;


            param.Append("&PageSize=");
            if (PageSize != 0)
            {
                param.Append(PageSize);
            }


            var count = ServicePriceVM.ServicePrices.Count;
            if (count == 0)
            {
                studentPage = 0;
            }


            if (sortParam == "SortDec")
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderByDescending(p => p.Service.ServiceName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else if (sortParam == "SortDecLocalPrice")
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderByDescending(p => p.LocalPrice)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDecLocalPrice";
            }
            else if (sortParam == "SortAscLocalPrice")
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderBy(p => p.LocalPrice)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAscLocalPrice";
            }
            else if (sortParam == "SortDecFormula")
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderByDescending(p => p.ForeignerFormulaPrice)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDecFormula";
            }
            else if (sortParam == "SortAscFormula")
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderBy(p => p.ForeignerFormulaPrice)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAscFormula";
            }
            else if (sortParam == "SortDecFixed")
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderByDescending(p => p.ForeignerFixedPrice)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDecFixed";
            }
            else if (sortParam == "SortAscFixed")
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderBy(p => p.ForeignerFixedPrice)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAscFixed";
            }

            else
            {
                ServicePriceVM.ServicePrices = ServicePriceVM.ServicePrices.OrderBy(p => p.Service.ServiceName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }


            //doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderBy(p => p.DoctorName)
            //.Skip((studentPage - 1) * PageSize)
            //.Take(PageSize).ToList();
            ////ViewBag.sortParamView = "SortAsc";




            ServicePriceVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };

            //if(country!=0)
            //{
            //    return View("Index");
            //}

            return View(ServicePriceVM);
        }
        public void FillServiceIndex(int searchField)
        {
            List<SelectListItem> services = new List<SelectListItem>();
            services.Insert(0, new SelectListItem() { Text = "Select Service", Value = "0" });
            foreach (var obj in db.Services)
            {
                services.Add(new SelectListItem() { Text = obj.ServiceName, Value = obj.ServiceID.ToString() });
            }
            SelectListItem selectedItem = (from i in services
                                           where i.Value == searchField.ToString()
                                           select i).SingleOrDefault();
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }

            ViewBag.Services = services;
        }
        public void FillService()
        {
            List<SelectListItem> services = new List<SelectListItem>();
            services.Insert(0, new SelectListItem() { Text = "Select Service", Value = "" });
            foreach (var obj in db.Services)
            {
                services.Add(new SelectListItem() { Text = obj.ServiceName, Value = obj.ServiceID.ToString() });
            }
            ViewBag.Services = services;
        }
        public IActionResult Create()
        {
            FillService();
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
                db.ServicePrices.Add(ServicePrice);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            FillService();
            return View(ServicePrice);
        }

        public IActionResult Update(int id)
        {
            FillService();
            var servicePrice = db.ServicePrices.Find(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(servicePrice);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.ServicePrices.Update(ServicePrice);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            FillService();
            return View(ServicePrice);
        }

        public IActionResult Detail(int id)
        {
            FillService();
            var servicePrice = db.ServicePrices.Find(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(servicePrice);
        }

        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailPost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                //db.ServicePrices.Update(ServicePrice);
                //await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            FillService();
            return View(ServicePrice);
        }

        public IActionResult Delete(int id)
        {
            FillService();
            var servicePrice = db.ServicePrices.Find(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(servicePrice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var delID = db.ServicePrices.Find(id);
                db.ServicePrices.Remove(delID);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            FillService();
            return View(ServicePrice);
        }
    }
}