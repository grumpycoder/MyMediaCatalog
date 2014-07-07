using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MyMediaCatalog.Data;
using MyMediaCatalog.Domain;
using MyMediaCatalog.Models;

namespace MyMediaCatalog.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly AppContext db = new AppContext();

        public ActionResult Index()
        {
            return View(db.Persons.Where(x => x.IsDeleted == false).ToList());
        }

        public ActionResult GetGridPersonList([DataSourceRequest] DataSourceRequest request)
        {
            var list = db.Persons.Where(x => x.IsDeleted == false).Project().To<PersonViewModel>();
            return Json(list.ToDataSourceResult(request));
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Firstname,Lastname")] Person person)
        {
            if (!ModelState.IsValid) return View(person);
            person.ModifiedUser = User.Identity.Name;
            person.CreatedUser = User.Identity.Name;
            person.DateCreated = DateTime.Now;
            person.DateModified = DateTime.Now;

            db.Persons.Add(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var person = db.Persons.Find(id);

            if (person == null) { return HttpNotFound(); }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Firstname,Lastname")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.ModifiedUser = User.Identity.Name;
                person.DateModified = DateTime.Now;
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public ActionResult Delete(int id)
        {
            var person = db.Persons.Find(id);
            if (person == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Persons.Remove(person);
            db.SaveChanges();
            if (!Request.IsAjaxRequest()) return RedirectToAction("Index");

            var list = db.Persons.Where(x => x.IsDeleted == false);
            return PartialView("_PersonList", list);
        }

        public ActionResult CreatePhone(int personId)
        {
            ViewBag.PhoneTypeId = new SelectList(db.PhoneTypes, "Id", "Name");
            var phone = new PersonPhoneViewModel { PersonId = personId };

            return PartialView("_CreatePersonPhone", phone);
        }

        [HttpPost]
        public ActionResult CreatePhone([Bind(Include = "PersonId,PhoneTypeId,Number")] PersonPhoneViewModel phoneViewModel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //TODO: Implement AutoMapper Here
            var phone = new PersonPhone
            {
                PhoneTypeId = phoneViewModel.PhoneTypeId,
                PersonId = phoneViewModel.PersonId,
                Phone = new Phone
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
            if (phone == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.PersonPhones.Remove(phone);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.PersonPhones.Where(c => c.PersonId == phone.PersonId);
            return PartialView("_PhoneListView", list);

        }

        public ActionResult GetPersonAddress(int id)
        {
            var list = db.PersonAddresses.Where(a => a.PersonId == id).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);
        }

        public ActionResult CreateAddress(int personId)
        {
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "Name");
            ViewBag.StateId = new SelectList(db.States, "Id", "Abbr");
            var addr = new PersonAddressViewModel()
            {
               PersonId = personId
            }; 
            return PartialView("_CreateAddress", addr);
        }

        [HttpPost]
        public ActionResult CreateAddress([Bind(Include = "PersonId,AddressTypeId,Street,Street2,City,StateId,PostalCode")] PersonAddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                var address = new PersonAddress
                {
                    PersonId = addressViewModel.PersonId,
                    AddressTypeId = addressViewModel.AddressTypeId,
                    Address = new Address
                    {
                        Street = addressViewModel.Street,
                        Street2 = addressViewModel.Street2,
                        City = addressViewModel.City,
                        StateId = addressViewModel.StateId,
                        PostalCode = addressViewModel.PostalCode,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name
                    }
                };
                db.PersonAddresses.Add(address);
                db.SaveChanges();
                return Json(new {success = true});
            }
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "Name");
            ViewBag.StateId = new SelectList(db.States, "Id", "Abbr");
            return PartialView("_CreateAddress");
            //var list = db.PersonAddresses.Where(a => a.PersonId == address.PersonId).Include(x => x.AddressType).Include(x => x.Address.State);
            //return PartialView("_AddressListView", list);

        }

        public ActionResult DeleteAddress(int id)
        {
            var address = db.PersonAddresses.Find(id);
            if (address == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.PersonAddresses.Remove(address);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.PersonAddresses.Where(a => a.PersonId == address.PersonId).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);
        }

        public ActionResult GetPersonList(string term)
        {
            if (term == null) term = Request.Params["filter[filters][0][value]"];
            //TODO: Code Smell. Refactor If..Else statement
            if (string.IsNullOrWhiteSpace(term))
            {
                var list = db.Persons.Where(x => x.IsDeleted == false);
                return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.Persons.Where(x => x.IsDeleted == false).Where(x => x.Lastname.Contains(term) || x.Firstname.Contains(term));
                return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreateNewStaffMember(int mediaId)
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            var staff = new StaffMemberViewModel { MediaId = mediaId };

            return PartialView("_CreateStaffMember", staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStaffMember([Bind(Include = "PersonId,RoleId,MediaId,Name,Title")] StaffMemberViewModel model)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (model.PersonId.IsInt())
            {
                var staff = new StaffMember
                {
                    PersonId = Convert.ToInt32(model.PersonId),
                    RoleId = model.RoleId,
                    MediaId = model.MediaId,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };
                db.StaffMembers.Add(staff);
                db.SaveChanges();
            }
            else
            {
                //Create new person record if PersonId is not an int
                var staff = new StaffMember
                {
                    Person = new Person
                    {
                        Firstname = model.PersonId.Split(new[] { ' ', '\t' }, StringSplitOptions.None)[0],
                        Lastname = model.PersonId.Split(new[] { ' ', '\t' }, StringSplitOptions.None)[1],
                        Title = model.Title,
                        ModifiedUser = User.Identity.Name,
                        CreatedUser = User.Identity.Name,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    },
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    RoleId = model.RoleId,
                    MediaId = model.MediaId
                };

                db.StaffMembers.Add(staff);
                db.SaveChanges();
            }

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list =
                db.StaffMembers.Where(x => x.IsDeleted == false).Where(x => x.MediaId == model.MediaId)
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
                db.StaffMembers.Where(x => x.IsDeleted == false).Where(x => x.MediaId == mediaStaffMember.MediaId)
                    .Include(x => x.Person)
                    .Include(x => x.Role);
            return PartialView("_StaffMembers", list);
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
