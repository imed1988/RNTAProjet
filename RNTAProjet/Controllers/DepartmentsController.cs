using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using RNTAProjet.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RNTAProjet.Controllers
{
    public class DepartmentsController : Controller
    {
        private BDEntities db = new BDEntities();

        // GET: Departments
        public ActionResult Index(string search, int? i)
        {
            List<Department> ListDepts = db.Department.ToList();
            return View(db.Department.Where(x => x.DepartmentName.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 3));
        }






        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Department.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Department.Find(id);
            db.Department.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ExportToExcel()
        {

            //get data from db
            var data = db.Department.ToList();
            GridView grid = new GridView();
            //assign data to gridview
            grid.DataSource = data;
            //bind data
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            //Adding name to excel file
            Response.AddHeader("content-disposition", "attachment;filename=Department.xls");
            //specify content type of file
            //Here i specified "ms-excel" format
            //you can also specify it "ms-word" to get word document
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htwriter = new HtmlTextWriter(sw))
                {
                    grid.RenderControl(htwriter);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.Close();
                }
            }
            return RedirectToActionPermanent("Index");
        }


        public ActionResult ExportToWord()
        {

            //get data from db
            var data = db.Department.ToList();
            GridView grid = new GridView();
            //assign data to gridview
            grid.DataSource = data;
            //bind data
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            //Adding name to excel file
            Response.AddHeader("content-disposition", "attachment;filename=Department.doc");
            //specify content type of file
            //Here i specified "ms-excel" format
            //you can also specify it "ms-word" to get word document
            Response.ContentType = "application/ms-word";
            Response.Charset = "";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htwriter = new HtmlTextWriter(sw))
                {
                    grid.RenderControl(htwriter);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.Close();
                }
            }
            return RedirectToActionPermanent("Index");
        }

       


    }
}    

