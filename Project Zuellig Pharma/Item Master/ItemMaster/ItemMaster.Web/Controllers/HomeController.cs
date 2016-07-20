using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using ItemMaster.Web.Models;
using LinqToExcel;
using Microsoft.AspNet.Identity;
using PagedList;

namespace ItemMaster.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly int _pageSize = 50;
        private readonly ApplicationDbContext _dataContext = new ApplicationDbContext();

        // GET: Home
        //public async Task<ActionResult> Index()
        //{
        //    return View(await _dataContext.ItemStocks.ToListAsync());
        //}

        public async Task<ActionResult> Index(string searchContent, FormCollection collection, int? page)
        {

            List<ZppStock> users =null;
            //string[] listSearch = null;
            List<string> listSearch;



            if ((!string.IsNullOrEmpty(collection["searchPRNCPL"])))
            {
                //listSearch = searchContent.Split(',');
                listSearch = searchContent.Split(';').Select(p => p.Trim()).ToList();
            
                users = await _dataContext.ItemStocks
                .OrderBy(x => x.ZpItemCode)
                .Where(x => listSearch.Contains(x.PRNCode)
                                               ).ToListAsync();

                Session["products"] = users;
                return View(users.ToPagedList(page ?? 1, 2000));
            }
            else if ((!string.IsNullOrEmpty(collection["searchStorage"])))
            {
                //listSearch = searchContent.Split(',');
                listSearch = searchContent.Split(';').Select(p => p.Trim()).ToList();

                users = await _dataContext.ItemStocks.Where(x => listSearch.Any(search => x.StorageCondition.Contains(search))).ToListAsync();

                Session["products"] = users;
                return View(users.ToPagedList(page ?? 1, 2000));
            }
            else if ((string.IsNullOrEmpty(searchContent)) )
            {
                users = await _dataContext.ItemStocks.OrderBy(x => x.ZpItemCode).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(searchContent))
            {
                users = await _dataContext.ItemStocks
                    .OrderBy(x => x.ZpItemCode)
                    .Where(x => x.ZpItemCode.Contains(searchContent)
                                                    || x.PRNCode.Contains(searchContent)
                                                    || x.ZpItemCode.Contains(searchContent)
                                                    || x.ItemDescription.Contains(searchContent)
                                                    || x.ItemStorageCode.Contains(searchContent)
                                                    || x.Template.Contains(searchContent)
                                                    || x.ControlledTemperture.Contains(searchContent)
                                                    || x.StorageCondition.Contains(searchContent)
                                                    || x.DeliveryMode.Contains(searchContent)).ToListAsync();
            }
            

            //luu ket qua vao session de dung` cho ham ExportData()
            Session["products"] = users;

            //var onePageOfProducts = users.ToPagedList(page ?? 1, 25);
            //ViewBag.OnePageOfProducts = onePageOfProducts;
            //return View();

            ViewBag.CurrentSearch = searchContent;
            

            return View(users.ToPagedList(page ?? 1, _pageSize));
        }

        // GET: Home/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZppStock zppStock = await _dataContext.ItemStocks
                                        .Include(x => x.Images)
                                        .Include(x => x.Histories)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            //var dir = new System.IO.DirectoryInfo(Server.MapPath("~/ItemMaster.Web/AttachFiles/"));
            //System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            //List<string> items = new List<string>();
            //foreach (var file in fileNames)
            //{
            //    items.Add(file.Name);
            //}
            //zppStock.Histories.Add(new ItemHistory(
                
            //    ));

            if (zppStock == null)
            {
                return HttpNotFound();
            }
            return View(zppStock);
        }

        // GET: Home/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ZpItemCode,ItemDescription,ItemStorageCode,Template,ControlledTemperture,DeliveryMode,StorageCondition,ChangeReason,Histories")] ZppStock zppStock, HttpPostedFileBase image, FormCollection fomr)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ImageFolder"]), $"{zppStock.ZpItemCode}.jpg");
                    image.SaveAs(path);
                    zppStock.Images = new List<ItemImage>
                    {
                        new ItemImage
                        {
                            Url = $"{ConfigurationManager.AppSettings["ImageFolder"]}/{zppStock.ZpItemCode}.jpg"
                        }
                    };
                }
                else
                {
                    zppStock.Images = new List<ItemImage>
                    {
                        new ItemImage
                        {
                            Url = $"{ConfigurationManager.AppSettings["ImageFolder"]}/{zppStock.ZpItemCode}.jpg"
                        }
                    };
                }


                //add history
                if (zppStock.Histories == null)
                {
                    zppStock.Histories = new List<ItemHistory>();
                }
                zppStock.Histories.Add(new ItemHistory
                {
                    UserId = User.Identity.GetUserId(),
                    UserName = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    Content = "Create new Item",
                    AttachfileName = "",
                    UrlAttachFile = "",
                    ReasonCode = ""
                });
                //zppStock.Active = true;
                zppStock.ChangeReason = "";
                zppStock.PRNCode = fomr["PRNCode"];
                try
                {
                    _dataContext.ItemStocks.Add(zppStock);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(zppStock);
        }

        // GET: Home/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZppStock zppStock = await _dataContext.ItemStocks
                                        .Include(x => x.Images)
                                        .Include(x => x.Histories).FirstOrDefaultAsync(x => x.Id == id);
            zppStock.ChangeReason = null;
            if (zppStock == null)
            {   
                return HttpNotFound();
            }
 
            return View(zppStock);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PRNCode,ZpItemCode,ItemDescription,ItemStorageCode,Template,ControlledTemperture,DeliveryMode,StorageCondition,Active,ChangeReason,StrActive")] ZppStock zppStock, HttpPostedFileBase image, HttpPostedFileBase attachFile, FormCollection fomr)
        {
            ZppStock dbStock = await _dataContext.ItemStocks
                                        .Include(x => x.Images)
                                        .Include(x => x.Histories)
                                        .FirstOrDefaultAsync(x => x.Id == zppStock.Id);
            dbStock.CopyFrom(zppStock);

            //add attach file
            string attachFilePath= "";
            string fileName = "";
            if (attachFile != null && attachFile.ContentLength > 0)
            {
                try
                {
                    fileName = Path.GetFileName(attachFile.FileName);
                    attachFilePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AttachFiles"]),
                                            Path.GetFileName(attachFile.FileName));
                    attachFile.SaveAs(attachFilePath);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }

            if (ModelState.IsValid)
            {
                //luu hinh vao thu muc va rename hinh moi thanh trung ten hình cũ => copy đè lên hình cũ và cùng tên hình cũ
                if (image != null && image.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ImageFolder"]), $"{dbStock.ZpItemCode}.jpg");
                    image.SaveAs(path);
                }

                if (!dbStock.Images.Any())
                {
                    dbStock.Images = new List<ItemImage>
                    {
                        new ItemImage
                        {
                            Url = $"{ConfigurationManager.AppSettings["ImageFolder"]}/{dbStock.ZpItemCode}.jpg"
                        }
                    };
                }

                
                if (dbStock.Histories == null)
                {
                    dbStock.Histories = new List<ItemHistory>();
                }
                dbStock.Histories.Add(new ItemHistory
                {
                    UserId = User.Identity.GetUserId(),
                    UserName = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    Content = dbStock.ChangeReason,
                    UrlAttachFile = attachFilePath,
                    AttachfileName = fileName,
                    ReasonCode = fomr["myListbox"]
                });
                //dbStock.Active = Convert.ToBoolean(fomr["Active"]);
 
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dbStock);
        }

        // GET: Home/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZppStock zppStock = _dataContext.ItemStocks.Find(id);
            if (zppStock == null)
            {
                return HttpNotFound();
            }
            return View(zppStock);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //ZppStock zppStock = _dataContext.ItemStocks.Find(id);
            ZppStock zppStock = _dataContext.ItemStocks.Include(x => x.Images).Include(x => x.Histories).FirstOrDefault(x => x.Id == id);
            _dataContext.ItemStocks.Remove(zppStock);
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

        [Authorize]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Import(HttpPostedFileBase file)
        {
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

            try
            {
                var excelFile = new ExcelQueryFactory(path);
                var items = excelFile.Worksheet<ZppStock>().ToList();
                foreach (ZppStock item in items)
                {
                    item.Images = new List<ItemImage>
                    {
                        new ItemImage
                        {
                            Url = $"{ConfigurationManager.AppSettings["ImageFolder"]}/{item.ZpItemCode}.jpg"
                        }
                    };
                    item.ChangeReason = " ";
                }

                _dataContext.ItemStocks.AddRange(items);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + ". Please contact with admin.";

                return View();
            }

            System.IO.File.Delete(path);

            return this.RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }


        public FileResult Download(string fileName, string filePatch)
        {
            try
            {
                string file = filePatch;
                byte[] fileBytes = System.IO.File.ReadAllBytes(file);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception exception)
            {
                
                return null;
            }
            
        }

        public ActionResult ExportData()
        {
            GridView gv = new GridView();

            // su dung session lu ket qua search de xuat ra file
            var products = Session["products"] as List<ZppStock>;
            gv.DataSource = products.ToList();
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

            return RedirectToAction("Index");
        }
    }
}
