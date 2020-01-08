using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sssHMS.Data;
using sssHMS.Models;
using sssHMS.Models.ViewModels;
using sssHMS.Utility;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace sssHMS.Controllers
{
    public class GeneralSettingController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly HostingEnvironment hostingEnvironment;

        public GeneralSettingController (ApplicationDbContext context,HostingEnvironment _hostingEnvironment)
        {
            db = context;
            hostingEnvironment = _hostingEnvironment;
        }

        [BindProperty]
        public sssHMSInfo sssHMSInfo { get; set; }


        public async Task<IActionResult> Index(string sortParam, int searchField, string searchParam, int studentPage = 1, int PageSize = 3)
        {
            sssHMSInfoViewModel sssHMSInfoVM = new sssHMSInfoViewModel()
            {

                sssHMSInfos = new List<Models.sssHMSInfo>()

            };
            if (searchField != 0)
            {
                ViewBag.DropDownColor = "abc";
            }

            FillsssHMSIndex(searchField);

            sssHMSInfoVM.sssHMSInfos = await db.sssHMSInfos.ToListAsync();

            if (searchField != 0)
            {
                sssHMSInfoVM.sssHMSInfos = db.sssHMSInfos.Where(a => a.sssHMSId == searchField).ToList();
            }


            StringBuilder param = new StringBuilder();
            param.Append("/GeneralSetting?studentPage=:");


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


            var count = sssHMSInfoVM.sssHMSInfos.Count;
            if (count == 0)
            {
                studentPage = 0;
            }


            if (sortParam == "SortDec")
            {
                sssHMSInfoVM.sssHMSInfos = sssHMSInfoVM.sssHMSInfos.OrderByDescending(p => p.sssHMSName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }           
            else
            {
                sssHMSInfoVM.sssHMSInfos = sssHMSInfoVM.sssHMSInfos.OrderBy(p => p.sssHMSName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }


            //doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderBy(p => p.DoctorName)
            //.Skip((studentPage - 1) * PageSize)
            //.Take(PageSize).ToList();
            ////ViewBag.sortParamView = "SortAsc";




            sssHMSInfoVM.PagingInfo = new PagingInfo()
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

            return View(sssHMSInfoVM);
        }

        public void FillsssHMSIndex(int searchField)
        {
            List<SelectListItem> sssHMSs = new List<SelectListItem>();
            sssHMSs.Insert(0, new SelectListItem() { Text = "Select sssHMS", Value = "0" });
            foreach (var obj in db.sssHMSInfos)
            {
                sssHMSs.Add(new SelectListItem() { Text = obj.sssHMSName, Value = obj.sssHMSId.ToString() });
            }
            SelectListItem selectedItem = (from i in sssHMSs
                                           where i.Value == searchField.ToString()
                                           select i).SingleOrDefault();
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }

            ViewBag.sssHMSs = sssHMSs;
        }


       

        public IActionResult Create()
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View();
        }
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if(ModelState.IsValid)
            {
                db.sssHMSInfos.Add(sssHMSInfo);
                await db.SaveChangesAsync();

                string webRootPath = hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var sssHMSFromDb = db.sssHMSInfos.Find(sssHMSInfo.sssHMSId);

                if(files.Count!=0)
                {
                    var uploads = Path.Combine(webRootPath, SD.sssHMSImageFolder);
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, sssHMSInfo.sssHMSId + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    sssHMSFromDb.sssHMSImage = @"\" + SD.sssHMSImageFolder + @"\" + sssHMSInfo.sssHMSId + extension;
                }
                else
                {
                    var uploads = Path.Combine(webRootPath, SD.sssHMSImageFolder + @"\" + SD.DefaultsssHMSImage);
                    System.IO.File.Copy(uploads, webRootPath + @"\" + SD.sssHMSImageFolder + @"\" + sssHMSInfo.sssHMSId + ".jpg");
                    sssHMSFromDb.sssHMSImage = @"\" + SD.sssHMSImageFolder + @"\" + sssHMSInfo.sssHMSId + ".jpg";

                }
                await db.SaveChangesAsync();


                return Redirect(returnUrl);
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var updatesssHMSInfo=await db.sssHMSInfos.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(updatesssHMSInfo);

        }
        [HttpPost,ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if(ModelState.IsValid)
            {
                string webRootPath = hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var sssHMSFromDb = db.sssHMSInfos.Where(a => a.sssHMSId == sssHMSInfo.sssHMSId).FirstOrDefault();

                if(files.Count>0 && files[0]!=null)
                {
                    var uploads = Path.Combine(webRootPath, SD.sssHMSImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(sssHMSFromDb.sssHMSImage);

                    if(System.IO.File.Exists(Path.Combine(uploads,sssHMSInfo.sssHMSId+extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, sssHMSInfo.sssHMSId + extension_old));

                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, sssHMSInfo.sssHMSId + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    sssHMSInfo.sssHMSImage = @"\" + SD.sssHMSImageFolder + @"\" + sssHMSInfo.sssHMSId + extension_new;

                }
                
                sssHMSFromDb.sssHMSName = sssHMSInfo.sssHMSName;
                sssHMSFromDb.sssHMSCode = sssHMSInfo.sssHMSCode;
                sssHMSFromDb.sssHMSAddress = sssHMSInfo.sssHMSAddress;
                sssHMSFromDb.sssHMSPhoneNo1 = sssHMSInfo.sssHMSPhoneNo1;
                sssHMSFromDb.sssHMSPhoneNo2 = sssHMSInfo.sssHMSPhoneNo2;
                sssHMSFromDb.sssHMSPhoneNo3 = sssHMSInfo.sssHMSPhoneNo3;
                sssHMSFromDb.sssHMSPhoneNo4 = sssHMSInfo.sssHMSPhoneNo4;
                sssHMSFromDb.sssHMSEmail = sssHMSInfo.sssHMSEmail;
                sssHMSFromDb.sssHMSWebsite = sssHMSInfo.sssHMSWebsite;
                if (sssHMSInfo.sssHMSImage != null)
                {
                    sssHMSFromDb.sssHMSImage = sssHMSInfo.sssHMSImage;
                }

                //db.sssHMSInfos.Update(sssHMSInfo);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(sssHMSInfo);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var sssHMS = await db.sssHMSInfos.FindAsync(id);
            //sssHMSInfo=db.sssHMSInfos.FirstOrDefault();
            //ViewBag.sssHMSId = sssHMSInfo.sssHMSId;
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(sssHMS);
        }

        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailPost(string returnUrl)
        {
            return Redirect(returnUrl);
        }

        public IActionResult Delete(int id)
        {
            var sssHMSInfo = db.sssHMSInfos.Find(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(sssHMSInfo);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if(ModelState.IsValid)
            {
                var delID = await db.sssHMSInfos.FindAsync(id);
                db.sssHMSInfos.Remove(delID);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(sssHMSInfo);
        }
    }
}