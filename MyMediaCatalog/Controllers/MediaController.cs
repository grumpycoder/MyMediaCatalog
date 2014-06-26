using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyMediaCatalog.Data;
using MyMediaCatalog.Domain;

namespace MyMediaCatalog.Controllers
{
    public class MediaController : Controller
    {
        private readonly AppContext db = new AppContext();

        public ActionResult Index()
        {
            var media = db.Media.Where(m => m.DateDeleted == null).Include(m => m.Company).Include(m => m.MediaType);
            return View(media.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            ViewBag.MediaTypeId = new SelectList(db.MediaTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ISBN,Summary,ReceiptDate,Review,Purchased,Donate,Active,CompanyId,MediaTypeId")] Media media)
        {
            if (ModelState.IsValid)
            {
                db.Media.Add(media);
                db.SaveChanges();

                if (!Request.IsAjaxRequest()) return RedirectToAction("Index");

                var list = db.Media.Where(m => m.DateDeleted == null).Include(m => m.Company).Include(m => m.MediaType);
                return PartialView("_MediaListView", list);
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", media.CompanyId);
            ViewBag.MediaTypeId = new SelectList(db.MediaTypes, "Id", "Name", media.MediaTypeId);
            return View(media);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = db.Media.Find(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", media.CompanyId);
            ViewBag.MediaTypeId = new SelectList(db.MediaTypes, "Id", "Name", media.MediaTypeId);
            return View(media);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ISBN,Summary,ReceiptDate,Review,Purchased,Donate,Active,CompanyId,MediaTypeId")] Media media)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", media.CompanyId);
            ViewBag.MediaTypeId = new SelectList(db.MediaTypes, "Id", "Name", media.MediaTypeId);
            return View(media);
        }
        
        public ActionResult Delete(int id)
        {
            var media = db.Media.Find(id);
            media.DateDeleted = DateTime.Now.Date;
            //db.Media.Remove(media);
            //db.Media.Attach(media);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return RedirectToAction("Index");

            var list = db.Media.Where(m => m.DateDeleted == null).Include(m => m.Company).Include(m => m.MediaType).ToList();
            return PartialView("_MediaListView", list);
        }

        public ActionResult CreateNewMedia(int companyId)
        {
            var media = new Media()
            {
                CompanyId = companyId
            };
            ViewBag.MediaTypeId = new SelectList(db.MediaTypes, "Id", "Name");
            return PartialView("_CreateMedia", media);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
