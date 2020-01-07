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
        public HospitalInfo HospitalInfo { get; set; }


        public async Task<IActionResult> Index(string sortParam, int searchField, string searchParam, int studentPage = 1, int PageSize = 3)
        {
            HospitalInfoViewModel HospitalInfoVM = new HospitalInfoViewModel()
            {

                HospitalInfos = new List<Models.HospitalInfo>()

            };
            if (searchField != 0)
            {
                ViewBag.DropDownColor = "abc";
            }

            FillHospitalIndex(searchField);

            HospitalInfoVM.HospitalInfos = await db.HospitalInfos.ToListAsync();

            if (searchField != 0)
            {
                HospitalInfoVM.HospitalInfos = db.HospitalInfos.Where(a => a.HospitalId == searchField).ToList();
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


            var count = HospitalInfoVM.HospitalInfos.Count;
            if (count == 0)
            {
                studentPage = 0;
            }


            if (sortParam == "SortDec")
            {
                HospitalInfoVM.HospitalInfos = HospitalInfoVM.HospitalInfos.OrderByDescending(p => p.HospitalName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }           
            else
            {
                HospitalInfoVM.HospitalInfos = HospitalInfoVM.HospitalInfos.OrderBy(p => p.HospitalName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }


            //doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderBy(p => p.DoctorName)
            //.Skip((studentPage - 1) * PageSize)
            //.Take(PageSize).ToList();
            ////ViewBag.sortParamView = "SortAsc";




            HospitalInfoVM.PagingInfo = new PagingInfo()
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

            return View(HospitalInfoVM);
        }

        public void FillHospitalIndex(int searchField)
        {
            List<SelectListItem> hospitals = new List<SelectListItem>();
            hospitals.Insert(0, new SelectListItem() { Text = "Select Hospital", Value = "0" });
            foreach (var obj in db.HospitalInfos)
            {
                hospitals.Add(new SelectListItem() { Text = obj.HospitalName, Value = obj.HospitalId.ToString() });
            }
            SelectListItem selectedItem = (from i in hospitals
                                           where i.Value == searchField.ToString()
                                           select i).SingleOrDefault();
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }

            ViewBag.Hospitals = hospitals;
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
                db.HospitalInfos.Add(HospitalInfo);
                await db.SaveChangesAsync();

                string webRootPath = hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var HospitalFromDb = db.HospitalInfos.Find(HospitalInfo.HospitalId);

                if(files.Count!=0)
                {
                    var uploads = Path.Combine(webRootPath, SD.HospitalImageFolder);
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, HospitalInfo.HospitalId + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    HospitalFromDb.HospitalImage = @"\" + SD.HospitalImageFolder + @"\" + HospitalInfo.HospitalId + extension;
                }
                else
                {
                    var uploads = Path.Combine(webRootPath, SD.HospitalImageFolder + @"\" + SD.DefaultHospitalImage);
                    System.IO.File.Copy(uploads, webRootPath + @"\" + SD.HospitalImageFolder + @"\" + HospitalInfo.HospitalId + ".jpg");
                    HospitalFromDb.HospitalImage = @"\" + SD.HospitalImageFolder + @"\" + HospitalInfo.HospitalId + ".jpg";

                }
                await db.SaveChangesAsync();


                return Redirect(returnUrl);
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var updateHospitalInfo=await db.HospitalInfos.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(updateHospitalInfo);

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
                var HospitalFromDb = db.HospitalInfos.Where(a => a.HospitalId == HospitalInfo.HospitalId).FirstOrDefault();

                if(files.Count>0 && files[0]!=null)
                {
                    var uploads = Path.Combine(webRootPath, SD.HospitalImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(HospitalFromDb.HospitalImage);

                    if(System.IO.File.Exists(Path.Combine(uploads,HospitalInfo.HospitalId+extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, HospitalInfo.HospitalId + extension_old));

                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, HospitalInfo.HospitalId + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    HospitalInfo.HospitalImage = @"\" + SD.HospitalImageFolder + @"\" + HospitalInfo.HospitalId + extension_new;

                }
                
                HospitalFromDb.HospitalName = HospitalInfo.HospitalName;
                HospitalFromDb.HospitalCode = HospitalInfo.HospitalCode;
                HospitalFromDb.HospitalAddress = HospitalInfo.HospitalAddress;
                HospitalFromDb.HospitalPhoneNo1 = HospitalInfo.HospitalPhoneNo1;
                HospitalFromDb.HospitalPhoneNo2 = HospitalInfo.HospitalPhoneNo2;
                HospitalFromDb.HospitalPhoneNo3 = HospitalInfo.HospitalPhoneNo3;
                HospitalFromDb.HospitalPhoneNo4 = HospitalInfo.HospitalPhoneNo4;
                HospitalFromDb.HospitalEmail = HospitalInfo.HospitalEmail;
                HospitalFromDb.HospitalWebsite = HospitalInfo.HospitalWebsite;
                if (HospitalInfo.HospitalImage != null)
                {
                    HospitalFromDb.HospitalImage = HospitalInfo.HospitalImage;
                }

                //db.HospitalInfos.Update(HospitalInfo);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(HospitalInfo);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var hospital = await db.HospitalInfos.FindAsync(id);
            //HospitalInfo=db.HospitalInfos.FirstOrDefault();
            //ViewBag.HospitalId = HospitalInfo.HospitalId;
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(hospital);
        }

        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailPost(string returnUrl)
        {
            return Redirect(returnUrl);
        }

        public IActionResult Delete(int id)
        {
            var hospitalInfo = db.HospitalInfos.Find(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(hospitalInfo);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if(ModelState.IsValid)
            {
                var delID = await db.HospitalInfos.FindAsync(id);
                db.HospitalInfos.Remove(delID);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(HospitalInfo);
        }
    }
}