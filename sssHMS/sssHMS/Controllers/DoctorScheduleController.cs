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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sssHMS.Controllers
{
    public class DoctorScheduleController : Controller
    {
        private readonly ApplicationDbContext db;
        public DoctorScheduleController(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public DoctorSchedule DoctorSchedule { get; set; }

        public async Task<IActionResult> Index(string sortParam,string searchField,string searchParam, int studentPage = 1, int PageSize = 3)
        {
            DoctorScheduleViewModel doctorScheduleVM = new DoctorScheduleViewModel()
            {

                DoctorSchedules = new List<Models.DoctorSchedule>()

            };
           if(searchField!=null)
            {
                ViewBag.DropDownColor = "abc";
            }

            FillSearchField(searchField);
            
            doctorScheduleVM.DoctorSchedules = await db.DoctorSchedules.ToListAsync();
            
            if(searchParam!=null)
            {
                if(searchField=="DoctorName")
                {
                    doctorScheduleVM.DoctorSchedules = (db.DoctorSchedules.Where(b => b.DoctorName.ToLower().Contains(searchParam.ToLower())).ToList());

                }
                else if (searchField == "DepartmentName")
                {
                    doctorScheduleVM.DoctorSchedules = (db.DoctorSchedules.Where(b => b.DepartmentName.ToLower().Contains(searchParam.ToLower())).ToList());

                }               
            }
            

            StringBuilder param = new StringBuilder();
            param.Append("/DoctorSchedule?studentPage=:");


            param.Append("&searchField=");
            if (searchField != null)
            {
                param.Append(searchField);     
            }

            param.Append("&sortParam=");
            if (sortParam != null)
            {
                param.Append(sortParam);
            }

            param.Append("&searchParam=");
            if (searchParam != null)
            {
                param.Append(searchParam);
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


            var count = doctorScheduleVM.DoctorSchedules.Count;
            if (count == 0)
            {
                studentPage = 0;
            }


            if (sortParam == "SortDec")
            {
                doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderByDescending(p => p.DoctorName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDec";
            }
            else if (sortParam == "SortDecDept")
            {
                doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderByDescending(p => p.DepartmentName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortDecDept";
            }
            else if (sortParam == "SortAscDept")
            {
                doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderBy(p => p.DepartmentName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAscDept";
            }

            else
            {
                doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderBy(p => p.DepartmentName)
                .Skip((studentPage - 1) * PageSize)
                .Take(PageSize).ToList();
                ViewBag.sortParamView = "SortAsc";
            }


            //doctorScheduleVM.DoctorSchedules = doctorScheduleVM.DoctorSchedules.OrderBy(p => p.DoctorName)
            //.Skip((studentPage - 1) * PageSize)
            //.Take(PageSize).ToList();
            ////ViewBag.sortParamView = "SortAsc";




            doctorScheduleVM.PagingInfo = new PagingInfo()
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

            return View(doctorScheduleVM);
        }

        //private void FillDoctorsID(int DoctorID = 0)
        //{
        //    List<SelectListItem> doctorsID = new List<SelectListItem>();
        //    doctorsID.Insert(0, new SelectListItem { Value = "", Text = "Select Doctor ID" });
        //    foreach (var temp in db.Doctors)
        //    {

        //        doctorsID.Add(new SelectListItem() { Text = temp.DoctorID.ToString(), Value = temp.DoctorID.ToString() });
        //    }

        //    SelectListItem selectedItem = (from i in doctorsID
        //                                   where i.Value == DoctorID.ToString()
        //                                   select i).SingleOrDefault();
        //    if (selectedItem != null)
        //    {
        //        selectedItem.Selected = true;
        //    }

        //    ViewBag.DoctorsID = doctorsID;

        //}


        private void FillDoctors(int DoctorID = 0/*,int DoctorName=0*/)
        {
            List<SelectListItem> doctors = new List<SelectListItem>();
            doctors.Insert(0, new SelectListItem { Value = "", Text = "Select Doctor Name" });

            if(DoctorID!=0)
            {
                var abc = (from data in db.Doctors where data.DoctorID == DoctorID select data).ToList();
                foreach(var temp in abc)
                {
                    doctors.Add(new SelectListItem() { Text = temp.FullName, Value = temp.DoctorID.ToString() });

                }
            }
            else
            {
                foreach (var temp in db.Doctors)
                {

                    doctors.Add(new SelectListItem() { Text = temp.FullName, Value = temp.DoctorID.ToString() });                   

                }

            }
            

            SelectListItem selectedItem = (from i in doctors
                                           where i.Value == DoctorID.ToString()
                                           select i).SingleOrDefault();
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }

            ViewBag.Doctors = doctors;

        }
        private void FillSearchField(string searchField)
        {
            List<SelectListItem> searchFields = new List<SelectListItem>();
            searchFields.Add(new SelectListItem() { Text = "Select One", Value = "" });
            searchFields.Add(new SelectListItem() { Text = "Doctor Name", Value = "DoctorName" });
            searchFields.Add(new SelectListItem() { Text = "Department Name", Value = "DepartmentName" });

            SelectListItem selectedItem = (from i in searchFields
                                           where i.Value == searchField
                                           select i).SingleOrDefault();
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }

            ViewBag.SearchFields = searchFields;
        }
        private void FillDepartment(int DoctorName=0)
        {
            if(DoctorName!=0)
            {
                var obj = (db.DoctorDeptUnits.Where(a => a.DoctorID == DoctorName));
                var mm = obj.FirstOrDefault().DepartmentID;
                var aa = (db.Departments.Where(b => b.DepartmentID == mm));
                ViewBag.DeptName = aa.FirstOrDefault().DepartmentName;
                ViewBag.DeptID = aa.FirstOrDefault().DepartmentID;
            }
            
        }
        private void FillUnits(int DoctorName = 0)
        {

            List<SelectListItem> units = new List<SelectListItem>();
            //units.Insert(0, new SelectListItem { Value = "", Text = "Select Unit" });
            var obj = (from data in db.DoctorDeptUnits where data.DoctorID == DoctorName select data).ToList();
            
            foreach (var item in obj)
            {
                var m = (db.Units.Where(a => a.UnitID == item.UnitID));
                //int id = m.FirstOrDefault().UnitID;
                string name = m.FirstOrDefault().UnitName;


                units.Add(new SelectListItem()
                {
                    Text = name,

                    Value = item.UnitID.ToString()
                });
            }


            ViewBag.Units = units;

        }

        //[HttpPost]
        public IActionResult GetDoctorName(int DoctorID,int DoctorName, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            //FillDoctorsID(/*DoctorID*/);
            weekDay();
            perPatientTime();
            FillDoctors(DoctorID);
            //FillDepartment(DoctorName);
            FillUnits(DoctorName);
            ModelState.ClearValidationState("");
            return View("Create");
        }

        //[HttpPost]
        public IActionResult GetDeptUnit(int DoctorID,int DoctorName,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            weekDay();
            perPatientTime();
            //FillDoctorsID(DoctorID);
            FillDoctors(DoctorID);
            FillDepartment(DoctorName);
            FillUnits(DoctorName);
            //ModelState.ClearValidationState("");

            return View("Create");
            //return GetDeptUnit(DoctorID, DoctorName, returnUrl);
            //return Json(DoctorSchedule);

        }
        
        private void weekDay()
        {
            List<SelectListItem> weekday = new List<SelectListItem>();
            weekday.Add(new SelectListItem() { Text = "Select Day of Week", Value = "" });
            weekday.Add(new SelectListItem() { Text = "Monday", Value = "Monday" });
            weekday.Add(new SelectListItem() { Text = "Tuesday", Value = "Tuesday" });
            weekday.Add(new SelectListItem() { Text = "Wednesday", Value = "Wednesday" });
            weekday.Add(new SelectListItem() { Text = "Thursday", Value = "Thursday" });
            weekday.Add(new SelectListItem() { Text = "Friday", Value = "Friday" });
            weekday.Add(new SelectListItem() { Text = "Saturday", Value = "Saturday" });
            weekday.Add(new SelectListItem() { Text = "Sunday", Value = "Sunday" });

            ViewBag.WeekDays = weekday;

            //ViewBag.WeekDays = new SelectList(weekday, "Value", "Text");
        }
        private void perPatientTime()
        {
            List<SelectListItem> perPatientTime = new List<SelectListItem>();
            perPatientTime.Add(new SelectListItem() { Text = "", Value = "" });
            perPatientTime.Add(new SelectListItem() { Text = "5", Value = "5" });
            perPatientTime.Add(new SelectListItem() { Text = "10", Value = "10" });
            perPatientTime.Add(new SelectListItem() { Text = "15", Value = "15" });
            perPatientTime.Add(new SelectListItem() { Text = "20", Value = "20" });
            perPatientTime.Add(new SelectListItem() { Text = "25", Value = "25" });
            perPatientTime.Add(new SelectListItem() { Text = "30", Value = "30" });
            //perPatientTime.Add(new SelectListItem() { Text = "Sunday", Value = "Sunday" });

            ViewBag.PerPatientTimes = perPatientTime;

            //ViewBag.WeekDays = new SelectList(weekday, "Value", "Text");
        }

        public IActionResult Create(string returnUrl)
        {
            perPatientTime();
            //FillDoctorsID();
            FillDoctors();
            FillDepartment();
            FillUnits();
            weekDay();
            

            //ViewBag.returnUrl = Request.Headers["referer"].ToString();
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            return View(DoctorSchedule);
        }

        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]       
        public IActionResult CreatePost(/*[FromBody]List<DoctorSchedule>*/string doctorSchedules,string returnUrl, int DoctorName)
        {
            var ds = JsonConvert.DeserializeObject<List<DoctorSchedule>>(doctorSchedules);
            ViewBag.returnUrl = returnUrl;
            if (doctorSchedules == null)
            {
               
                //ModelState.ClearValidationState("");
                return Json("Fill Data Correctly!");
                //return View();
            }

            else if (ds.Count < 1)
            {
                return Json("Fill Data Correctly!");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Loop and insert records.
                    foreach (DoctorSchedule customer in ds)
                    {
                        db.DoctorSchedules.Add(customer);
                    }
                    int insertedRecords = db.SaveChanges();
                    
                    return Json(new
                    {
                        records = insertedRecords,
                        backToUrl = returnUrl,
                        redirectUrl = Url.Action("Index", "DoctorSchedule"),
                        isRedirect = true
                    });
                }

            }
          
            perPatientTime();
            //FillDoctorsID();
            FillDoctors();
            FillDepartment(DoctorName);
            FillUnits(DoctorName);
            weekDay();

            //ModelState.ClearValidationState("");
            return View("Create");
        }

        public async Task<IActionResult> Update(int id)
        {
            var doctorSchedules = await db.DoctorSchedules.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            weekDay();
            perPatientTime();
            return View(doctorSchedules);
        }

        [HttpPost,ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                db.DoctorSchedules.Update(DoctorSchedule);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            return View(DoctorSchedule);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var doctorSchedules = await db.DoctorSchedules.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            //weekDay();
            //perPatientTime();
            return View(doctorSchedules);
        }

        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailPost(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                return Redirect(returnUrl);
            }
            return View(DoctorSchedule);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctorSchedules = await db.DoctorSchedules.FindAsync(id);
            ViewBag.returnUrl = Request.Headers["referer"].ToString();
            //weekDay();
            //perPatientTime();
            return View(doctorSchedules);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var delId = await db.DoctorSchedules.FindAsync(id);
                db.DoctorSchedules.Remove(delId);
                await db.SaveChangesAsync();
                return Redirect(returnUrl);

            }
            return View(DoctorSchedule);
        }
    }
}