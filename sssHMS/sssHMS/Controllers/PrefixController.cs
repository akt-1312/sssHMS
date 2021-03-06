﻿using System;
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
    public class PrefixController : Controller
    {
        private readonly ApplicationDbContext db;

        public PrefixController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Prefix Prefix { get; set; }


        public async Task<IActionResult> Index(string sortParam, string searchParam ,int studentPage = 1, int PageSize = 9)
        {
            PrefixViewModel PrefixVM = new PrefixViewModel()
            {
                Prefixes = new List<Models.Prefix>()
            };
            PrefixVM.Prefixes = await db.Prefixes.ToListAsync();

            if (searchParam != null)
            {
                PrefixVM.Prefixes = PrefixVM.Prefixes.Where(a => a.PrefixName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            StringBuilder param = new StringBuilder();
            param.Append("/Prefix?studentPage=:");

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
            var count = PrefixVM.Prefixes.Count;

            if (sortParam == "SortDec")
            {
                PrefixVM.Prefixes = PrefixVM.Prefixes.OrderByDescending(p => p.PrefixName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else
            {
                PrefixVM.Prefixes = PrefixVM.Prefixes.OrderBy(p => p.PrefixName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }

            PrefixVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString(),
                //PagerSize = 5,

            };
            return View(PrefixVM);
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
                db.Prefixes.Add(Prefix);
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
            var prefix = await db.Prefixes.FindAsync(id);
            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(prefix);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Prefixes where data.PrefixID == int.Parse(iD) select data).FirstOrDefault();
                obj.PrefixID = int.Parse(iD);
                obj.PrefixName = Prefix.PrefixName;
                
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction("Index");
            }


            //string qs = iD.ToString();

            return View(Prefix);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var prefix = await db.Prefixes.FindAsync(id);

            //ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(prefix);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.Prefixes.Remove(Prefix);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }


            //string qs = iD.ToString();

            return View(Prefix);


            //return RedirectToAction("Index", new { studentId = iD });
        }

        //public IActionResult Search(string search)
        //{
        //    return RedirectToAction("Index", new { abc = search });
        //}

        public async Task<IActionResult> Details(int id)
        {

            var prefix = await db.Prefixes.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(prefix);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsData(string iD, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var obj = (from data in db.Prefixes where data.PrefixID == int.Parse(iD) select data).FirstOrDefault();
                obj.PrefixID = int.Parse(iD);
                obj.PrefixName = Prefix.PrefixName;
                
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
               
            }


            //string qs = iD.ToString();

            return View(Prefix);


            //return RedirectToAction("Index", new { studentId = iD });
        }


    }
}