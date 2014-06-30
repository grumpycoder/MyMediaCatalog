using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMediaCatalog.Data;
using MyMediaCatalog.Domain;
using MyMediaCatalog.Models;

namespace MyMediaCatalog.Controllers
{
    public class PeopleController : Controller
    {
        private AppContext db = new AppContext();

        // GET: People
        public ActionResult Index()
        {
            return View(db.Persons.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Firstname,Lastname")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Firstname,Lastname")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public ActionResult Delete(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                var list = db.Persons;
                return PartialView("_PersonList", list);
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult CreatePhone(int personId)
        {
            ViewBag.PhoneTypeId = new SelectList(db.PhoneTypes, "Id", "Name");
            var phone = new PersonPhoneViewModel() { PersonId = personId };

            return PartialView("_CreatePersonPhone", phone);
        }

        [HttpPost]
        public ActionResult CreatePhone([Bind(Include = "PersonId,PhoneTypeId,Number")] PersonPhoneViewModel phoneViewModel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //TODO: Automapper Here
            var phone = new PersonPhone()
            {
                PhoneTypeId = phoneViewModel.PhoneTypeId,
                PersonId = phoneViewModel.PersonId,
                Phone = new Phone()
                {
                    Number = phoneViewModel.Number
                }
            };

            db.PersonPhones.Add(phone);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.PersonPhones.Where(c => c.PersonId == phone.PersonId).Include(p => p.PhoneType);
            return PartialView("_PhoneListView", list);
        }

        public ActionResult DeletePhone(int id)
        {
            var phone = db.PersonPhones.Find(id);
            db.PersonPhones.Remove(phone);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.PersonPhones.Where(c => c.PersonId == phone.PersonId);
            return PartialView("_PhoneListView", list);

        }

        public ActionResult CreateAddress(int personId)
        {
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "Name");
            ViewBag.StateId = new SelectList(db.States, "Id", "Abbr");
            var addr = new PersonAddressViewModel() { PersonId = personId };

            return PartialView("_CreatePersonAddress");
        }

        [HttpPost]
        public ActionResult CreateAddress([Bind(Include = "PersonId,AddressTypeId,Street,Street2,City,StateId,PostalCode")] PersonAddressViewModel addressViewModel)
        {
            var address = new PersonAddress()
            {
                PersonId = addressViewModel.PersonId,
                AddressTypeId = addressViewModel.AddressTypeId,
                Address = new Address()
                {
                    Street = addressViewModel.Street,
                    Street2 = addressViewModel.Street2,
                    City = addressViewModel.City,
                    StateId = addressViewModel.StateId,
                    PostalCode = addressViewModel.PostalCode
                }
            };
            db.PersonAddresses.Add(address);
            db.SaveChanges();
            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.PersonAddresses.Where(a => a.PersonId == address.PersonId).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);

        }

        public ActionResult DeleteAddress(int id)
        {
            var address = db.PersonAddresses.Find(id);
            db.PersonAddresses.Remove(address);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.PersonAddresses.Where(a => a.PersonId == address.PersonId).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);
        }

        public ActionResult GetPersonList(string term)
        {
            if(term == null) term = Request.Params["filter[filters][0][value]"];

            var list = db.Persons.Where(x => x.Lastname.Contains(term) || x.Firstname.Contains(term));
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
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
