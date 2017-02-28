using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmployeeSurvey.Web.Models;
using LinqToExcel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace EmployeeSurvey.Web.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private static readonly int _pageSize = 50;
        private readonly ApplicationDbContext _dataContext = new ApplicationDbContext();

        // GET: UserManagement
        public async Task<ActionResult> Index(string searchContent, int? page)
        {
            var id = User.Identity.GetUserId();
            var userLogin = _dataContext.Users.FirstOrDefault(x => x.Id == id);
            //var result = _dataContext.Users.Include(x => x.Survey)
            //    .Where(x => x.UserName != "Admin" && x.Country == userLogin.Country);
            List<ApplicationUser> users;
            if (!string.IsNullOrEmpty(searchContent))
            {
                users = await _dataContext.Users
                    .OrderBy(x => x.EmployeeId)
                    .Where(x => (x.EmployeeId.Contains(searchContent)
                                                    || x.FullName.Contains(searchContent)
                                                    || x.PayrollGroup.Contains(searchContent)
                                                    || x.Department.Contains(searchContent)
                                                    || x.CostLocation.Contains(searchContent)
                                                    || x.Position.Contains(searchContent)) && x.Country == userLogin.Country && x.Level != "99").ToListAsync();
            }
            else
            {
                users = await _dataContext.Users.OrderBy(x => x.EmployeeId).Where(x => x.Country == userLogin.Country && x.Level != "99").ToListAsync();
            }

            //List<ApplicationUser> users;
            //if (!string.IsNullOrEmpty(searchContent))
            //{
            //    users = await _dataContext.Users
            //        .OrderBy(x => x.EmployeeId)
            //        .Where(x => x.EmployeeId.Contains(searchContent)
            //                                        || x.FullName.Contains(searchContent)
            //                                        || x.PayrollGroup.Contains(searchContent)
            //                                        || x.Department.Contains(searchContent)
            //                                        || x.CostLocation.Contains(searchContent)
            //                                        || x.Position.Contains(searchContent)).ToListAsync();
            //}
            //else
            //{
            //    users = await _dataContext.Users.OrderBy(x => x.EmployeeId).ToListAsync();
            //}

            ViewBag.CurrentSearch = searchContent;

            return View(users.ToPagedList(page ?? 1, _pageSize));
        }

        // GET: UserManagement/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: UserManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeId,FullName,PayrollGroup,Department,CostLocation,Position,StartingDate,IsActive,PasswordBeforeHash")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dataContext));
                //await userManager.CreateAsync(applicationUser, applicationUser.PasswordBeforeHash);

                var id = User.Identity.GetUserId();
                var userLogin = _dataContext.Users.FirstOrDefault(x => x.Id == id);
                var importUser = new ApplicationUser
                {
                    UserName = applicationUser.EmployeeId,
                    EmployeeId = applicationUser.EmployeeId,
                    FullName = applicationUser.FullName,
                    PayrollGroup = applicationUser.PayrollGroup,
                    Department = applicationUser.Department,
                    CostLocation = applicationUser.CostLocation,
                    Position = applicationUser.Position,
                    StartingDate = applicationUser.StartingDate,
                    Email = "",
                    PasswordBeforeHash = applicationUser.PasswordBeforeHash,
                    Level = applicationUser.Level,
                    //admin nước nào thì sẽ tạo ra user country tương ứng nước đó
                    Country = userLogin.Country
                };
                await userManager.CreateAsync(importUser, applicationUser.PasswordBeforeHash);

                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: UserManagement/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: UserManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,FullName,PayrollGroup,Department,CostLocation,Position,StartingDate,IsActive,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed")] ApplicationUser applicationUser)
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,FullName,PayrollGroup,Department,CostLocation,Position,StartingDate,CreatedDate,UpdatedDate,IsActive,PasswordBeforeHash,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                //_dataContext.Entry(applicationUser).State = EntityState.Modified;
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == applicationUser.Id);
                user.FullName = applicationUser.FullName;
                user.PayrollGroup = applicationUser.PayrollGroup;
                user.Department = applicationUser.Department;
                user.CostLocation = applicationUser.CostLocation;
                user.Position = applicationUser.Position;
                user.StartingDate = applicationUser.StartingDate;
                user.IsActive = applicationUser.IsActive;
                user.Email = applicationUser.Email;
                user.PhoneNumber = applicationUser.PhoneNumber;

                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: UserManagement/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: UserManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.Users.Remove(applicationUser);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult ImportUsers()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> ImportUsers(FormCollection collection)
        {
            HttpPostedFileBase file = Request.Files["UploadedFile"];

            if (file == null || file.ContentLength == 0)
            {
                ViewBag.Error = "Please select file to import";
                return View();
            }

            var extension = Path.GetExtension(file.FileName);
            if (extension != null && !extension.Contains("xls"))
            {
                ViewBag.Error = "Only support excel file type (*.xls|*.xlsx)";
                return View();
            }

            // Read excel file and save to database
            string path = Path.Combine(Server.MapPath("~/FilesUpload"), file.FileName);
            file.SaveAs(path);

            //add by Phuc
            var id = User.Identity.GetUserId();
            var userLogin = _dataContext.Users.FirstOrDefault(x => x.Id == id);

            try
            {
                var excelFile = new ExcelQueryFactory(path);
                var usersInFile = excelFile.Worksheet<UploadUserModel>();

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dataContext));

                foreach (var user in usersInFile)
                {
                    if (userLogin.Country != user.Country)
                    {
                        TempData["error"] = "Column Country was wrong. Please edit column Country ";
                        ViewBag.Error = TempData["error"];
                        return View();
                    }
                }

                foreach (var user in usersInFile)
                {
                    if (userLogin.Country == user.Country)
                    {
                        var importUser = new ApplicationUser
                        {
                            UserName = user.EmployeeId,
                            EmployeeId = user.EmployeeId,
                            FullName = user.FullName,
                            PayrollGroup = user.PayrollGroup,
                            Department = user.Department,
                            CostLocation = user.CostLocation,
                            Position = user.Position,
                            StartingDate = user.StartingDate,
                            Email = "",
                            PasswordBeforeHash = user.Password,
                            Level = user.Level,
                            Country = user.Country
                        };
                        await userManager.CreateAsync(importUser, user.Password);
                    }
                    else
                    {
                        TempData["error"] = "Column Country was wrong.";
                        ViewBag.Error = TempData["error"];
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + "<br/> Please contact with admin.";

                return View();
            }

            System.IO.File.Delete(path);

            return this.RedirectToAction("Index");
        }

        //public void ExportUsers()
        //{
        //    var sw = new StringWriter();

        //    string[] header = new[] { "Employee Id", "Full Name", "Department", "Payroll Group","Level",
        //                              "Option1", "Content1", "Option2", "Content2", "Option3", "Content3",
        //                              "Option4", "Content4", "Option5", "Content5", "Option6", "Content6",
        //                              "Option7", "Content7", "Option8", "Content8", "Option9", "Content9",
        //                              "Option10", "Content10", "Option11", "Content11","SurveyUpdateDate" };

        //    sw.WriteLine(string.Join(",", header));

        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}_Users.xls", DateTime.Now.ToString("yyyyMMdd")));
        //    Response.ContentType = "application/ms-excel";

        //    Response.ContentEncoding = System.Text.Encoding.Unicode;
        //    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


        //    var db = new ApplicationDbContext();
        //    var id = User.Identity.GetUserId();
        //    var usertmp = db.Users.FirstOrDefault(x => x.Id == id);
        //    if (usertmp.Country == "ZPVN")
        //    {
        //        foreach (var user in _dataContext.Users.Include(x => x.Survey).Where(x => x.UserName != "Admin" && (x.Country == "ZPVN")))
        //        {
        //            sw.WriteLine(string.Join(",",
        //                user.EmployeeId, user.FullName, user.Department, user.PayrollGroup, user.Level,
        //                user.Survey?.YesNoOption1, user.Survey?.Content1,
        //                user.Survey?.YesNoOption2, user.Survey?.Content2,
        //                user.Survey?.YesNoOption3, user.Survey?.Content3,
        //                user.Survey?.YesNoOption4, user.Survey?.Content4,
        //                user.Survey?.YesNoOption5, user.Survey?.Content5,
        //                user.Survey?.YesNoOption6, user.Survey?.Content6,
        //                user.Survey?.YesNoOption7, user.Survey?.Content7,
        //                user.Survey?.YesNoOption8, user.Survey?.Content8,
        //                user.Survey?.YesNoOption9, user.Survey?.Content9,
        //                user.Survey?.YesNoOption10, user.Survey?.Content10,
        //                user.Survey?.YesNoOption11, user.Survey?.Content11,
        //                user.Survey?.SurveyUpdateDate.ToString("dd/MM/yyyy hh:mm tt")
        //                ));
        //        }
        //    }

        //    //foreach (var user in _dataContext.Users.Include(x => x.Survey).Where(x => x.UserName != "Admin"))
        //    //{
        //    //    sw.WriteLine(string.Join(",",
        //    //        user.EmployeeId, user.FullName, user.Department, user.PayrollGroup, user.Level,
        //    //        user.Survey?.YesNoOption1, user.Survey?.Content1,
        //    //        user.Survey?.YesNoOption2, user.Survey?.Content2,
        //    //        user.Survey?.YesNoOption3, user.Survey?.Content3,
        //    //        user.Survey?.YesNoOption4, user.Survey?.Content4,
        //    //        user.Survey?.YesNoOption5, user.Survey?.Content5,
        //    //        user.Survey?.YesNoOption6, user.Survey?.Content6,
        //    //        user.Survey?.YesNoOption7, user.Survey?.Content7,
        //    //        user.Survey?.YesNoOption8, user.Survey?.Content8,
        //    //        user.Survey?.YesNoOption9, user.Survey?.Content9,
        //    //        user.Survey?.YesNoOption10, user.Survey?.Content10,
        //    //        user.Survey?.YesNoOption11, user.Survey?.Content11,
        //    //        user.Survey?.SurveyUpdateDate.ToString("dd/MM/yyyy hh:mm tt")
        //    //        ));
        //    //}



        //    Response.Write(sw.ToString());
        //    Response.End();
        //}

        public void ExportUsers()
        {
            GridView gv = new GridView();


            var db = new ApplicationDbContext();
            var id = User.Identity.GetUserId();
            var userLogin = db.Users.FirstOrDefault(x => x.Id == id);

            // su dung session lu ket qua search de xuat ra file
            //var result = _dataContext.Users.Include(x => x.Survey)
            //    .Where(x => x.Country == userLogin.Country && x.Level != "99")
            //    .Select(x => new
            //    {
            //        EmployeeId = x.EmployeeId,
            //        FullName = x.FullName,
            //        Department = x.Department,
            //        PayrollGroup = x.PayrollGroup,
            //        Level = x.Level,
            //        Option1 = x.Survey != null ? x.Survey.YesNoOption1 : string.Empty,
            //        Option2 = x.Survey != null ? x.Survey.YesNoOption2 : string.Empty,
            //        Option3 = x.Survey != null ? x.Survey.YesNoOption3 : string.Empty,
            //        Option4 = x.Survey != null ? x.Survey.YesNoOption4 : string.Empty,
            //        Option5 = x.Survey != null ? x.Survey.YesNoOption5 : string.Empty,
            //        Option6 = x.Survey != null ? x.Survey.YesNoOption6 : string.Empty,
            //        Option7 = x.Survey != null ? x.Survey.YesNoOption7 : string.Empty,
            //        Option8 = x.Survey != null ? x.Survey.YesNoOption8 : string.Empty,
            //        Option9 = x.Survey != null ? x.Survey.YesNoOption9 : string.Empty,
            //        Option10 = x.Survey != null ? x.Survey.YesNoOption10 : string.Empty,
            //        Option11 = x.Survey != null ? x.Survey.YesNoOption11 : string.Empty,

            //        Content1 = x.Survey != null ? x.Survey.Content1 : string.Empty,
            //        Content2 = x.Survey != null ? x.Survey.Content2 : string.Empty,
            //        Content3 = x.Survey != null ? x.Survey.Content3 : string.Empty,
            //        Content4 = x.Survey != null ? x.Survey.Content4 : string.Empty,
            //        Content5 = x.Survey != null ? x.Survey.Content5 : string.Empty,
            //        Content6 = x.Survey != null ? x.Survey.Content6 : string.Empty,
            //        Content7 = x.Survey != null ? x.Survey.Content7 : string.Empty,
            //        Content8 = x.Survey != null ? x.Survey.Content8 : string.Empty,
            //        Content9 = x.Survey != null ? x.Survey.Content9 : string.Empty,
            //        Content10 = x.Survey != null ? x.Survey.Content10 : string.Empty,
            //        Content11 = x.Survey != null ? x.Survey.Content11 : string.Empty,

            //        SurveyUpdateDate = x.Survey != null ? x.Survey.SurveyUpdateDate.ToString() : string.Empty
            //    }).ToList();

            // su dung session lu ket qua search de xuat ra file
            var result = _dataContext.Users.Include(x => x.Survey)
                .Where(x => x.Country == userLogin.Country && x.Level != "99")
                .Select(x => new
                {
                    EmployeeId = x.EmployeeId,
                    FullName = x.FullName,
                    Department = x.Department,
                    PayrollGroup = x.PayrollGroup,
                    Level = x.Level,
                    Q1_Are_you_currently_a_Government_Official = x.Survey != null ? x.Survey.YesNoOption1 : string.Empty,
                    Q2_Are_any_of_your_Family_Members_currently_a_Government_Official = x.Survey != null ? x.Survey.YesNoOption2 : string.Empty,
                    C = x.Survey != null ? x.Survey.YesNoOption3 : string.Empty,
                    D = x.Survey != null ? x.Survey.YesNoOption4 : string.Empty,
                    E = x.Survey != null ? x.Survey.YesNoOption5 : string.Empty,
                    Option6 = x.Survey != null ? x.Survey.YesNoOption6 : string.Empty,
                    Option7 = x.Survey != null ? x.Survey.YesNoOption7 : string.Empty,
                    Option8 = x.Survey != null ? x.Survey.YesNoOption8 : string.Empty,
                    Option9 = x.Survey != null ? x.Survey.YesNoOption9 : string.Empty,
                    Option10 = x.Survey != null ? x.Survey.YesNoOption10 : string.Empty,
                    Option11 = x.Survey != null ? x.Survey.YesNoOption11 : string.Empty,

                    Content1 = x.Survey != null ? x.Survey.Content1 : string.Empty,
                    Content2 = x.Survey != null ? x.Survey.Content2 : string.Empty,
                    Content3 = x.Survey != null ? x.Survey.Content3 : string.Empty,
                    Content4 = x.Survey != null ? x.Survey.Content4 : string.Empty,
                    Content5 = x.Survey != null ? x.Survey.Content5 : string.Empty,
                    Content6 = x.Survey != null ? x.Survey.Content6 : string.Empty,
                    Content7 = x.Survey != null ? x.Survey.Content7 : string.Empty,
                    Content8 = x.Survey != null ? x.Survey.Content8 : string.Empty,
                    Content9 = x.Survey != null ? x.Survey.Content9 : string.Empty,
                    Content10 = x.Survey != null ? x.Survey.Content10 : string.Empty,
                    Content11 = x.Survey != null ? x.Survey.Content11 : string.Empty,

                    SurveyUpdateDate = x.Survey != null ? x.Survey.SurveyUpdateDate.ToString() : string.Empty
                }).ToList();

            gv.DataSource = result;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Marklist.xls");
            Response.ContentType = "application/ms-excel";

            //xuat ra file tieng viet
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }

    }
}
