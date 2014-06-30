using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyMediaCatalog.Data;
using MyMediaCatalog.Domain;
using MyMediaCatalog.Models;

namespace MyMediaCatalog.Controllers
{
    public class StaffMembersController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult CreateNewStaffMember(int mediaId)
        {
            var staff = new StaffMemberViewModel()
            {
                MediaId = mediaId
            };

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");

            return PartialView("_CreateStaffMember");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStaffMember([Bind(Include = "PersonId,RoleId,MediaId,Name,Title")] StaffMemberViewModel model)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var staff = new StaffMember()
            {
                Person = new Person()
                {
                    Firstname = model.Name.Split(new[] { ' ', '\t' }, StringSplitOptions.None)[0],
                    Lastname = model.Name.Split(new[] { ' ', '\t' }, StringSplitOptions.None)[1],
                    Title = model.Title
                },
                RoleId = model.RoleId,
                MediaId = model.MediaId
            };

            db.StaffMembers.Add(staff);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);
            var list =
                db.StaffMembers.Where(x => x.MediaId == model.MediaId)
                    .Include(x => x.Person)
                    .Include(x => x.Role);
            return PartialView("_StaffMembers", list);
        }

        public ActionResult DeleteStaffMember(int id)
        {
            var mediaStaffMember = db.StaffMembers.Find(id);
            if (mediaStaffMember == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.StaffMembers.Remove(mediaStaffMember);
            db.SaveChanges();
            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);
            var list =
                db.StaffMembers.Where(x => x.MediaId == mediaStaffMember.MediaId)
                    .Include(x => x.Person)
                    .Include(x => x.Role);
            return PartialView("_StaffMembers", list);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var staff = db.StaffMembers.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }


        




        public ActionResult GetStaffMembers(int id)
        {
            var list = db.StaffMembers.Where(x => x.MediaId == id)
                .Include(x => x.Person).Include(x => x.Role);
            return PartialView("_StaffMembers", list);
        }


        // GET: StaffMembers
        public ActionResult Index()
        {
            var staff = db.StaffMembers.Include(m => m.Media).Include(m => m.Person);
            return View(staff.ToList());
        }


        // GET: StaffMembers/Create
        public ActionResult Create()
        {
            //ViewBag.MediaId = new SelectList(db.Media, "Id", "Title");
            //ViewBag.StaffMemberId = new SelectList(db.StaffMembers, "Id", "Id");
            //return View();

            ViewBag.PersonId = new SelectList(db.Persons, "Id", "Lastname");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            ViewBag.MediaId = new SelectList(db.Media, "Id", "Title");

            return View();
        }

        // POST: StaffMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,StaffMemberId,MediaId")] StaffMemberViewModel model)
        public ActionResult Create([Bind(Include = "PersonId,RoleId,MediaId")] StaffMember model)
        {

            if (ModelState.IsValid)
            {
                var staff = new StaffMember()
                {
                    PersonId = model.PersonId,
                    RoleId = model.RoleId
                };
                db.StaffMembers.Add(staff);
                db.SaveChanges();

                //var mediaStaffMember = new StaffMember()
                //{
                //    StaffMemberId = staff.Id,
                //    MediaId = model.MediaId
                //};
                //db.MediaStaffMembers.Add(mediaStaffMember);
                //db.SaveChanges();

                //db.MediaStaffMembers.Add(mediaStaffMember);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MediaId = new SelectList(db.Media, "Id", "Title", mediaStaffMember.MediaId);
            //ViewBag.StaffMemberId = new SelectList(db.StaffMembers, "Id", "Id", mediaStaffMember.StaffMemberId);
            //return View(mediaStaffMember);

            ViewBag.PersonId = new SelectList(db.Persons, "Id", "Lastname");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            ViewBag.MediaId = new SelectList(db.Media, "Id", "Title");
            return View(model);


        }

        // GET: StaffMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.MediaId = new SelectList(db.Media, "Id", "Title", staffMember.MediaId);
            ViewBag.StaffMemberId = new SelectList(db.StaffMembers, "Id", "Id", staffMember.Id);
            return View(staffMember);
        }

        // POST: StaffMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StaffMemberId,Firstname,Lastname,Title")] StaffMember mediaStaffMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediaStaffMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MediaId = new SelectList(db.Media, "Id", "Title", mediaStaffMember.MediaId);
            ViewBag.StaffMemberId = new SelectList(db.StaffMembers, "Id", "Id", mediaStaffMember.Id);
            return View(mediaStaffMember);
        }

        // GET: StaffMembers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StaffMember mediaStaffMember = db.StaffMembers.Find(id);
        //    if (mediaStaffMember == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mediaStaffMember);
        //}

        // POST: StaffMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffMember mediaStaffMember = db.StaffMembers.Find(id);
            db.StaffMembers.Remove(mediaStaffMember);
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
    }
}
