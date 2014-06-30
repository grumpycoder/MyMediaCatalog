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
    public class CompanyController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,WebsiteUrl")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,WebsiteUrl")] Company company)
        {
            if (ModelState.IsValid)
            {

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreatePhone(int companyId)
        {
            ViewBag.PhoneTypeId = new SelectList(db.PhoneTypes, "Id", "Name");
            var phone = new CompanyPhoneViewModel() { CompanyId = companyId };

            return PartialView("_CreateCompanyPhone", phone);
        }

        [HttpPost]
        public ActionResult CreatePhone([Bind(Include = "CompanyId,PhoneTypeId,Number")] CompanyPhoneViewModel phone)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //TODO: Automapper Here
            var companyphone = new CompanyPhone()
            {
                PhoneTypeId = phone.PhoneTypeId,
                CompanyId = phone.CompanyId,
                Phone = new Phone()
                {
                    Number = phone.Number
                }
            };

            db.CompanyPhones.Add(companyphone);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.CompanyPhones.Where(c => c.CompanyId == phone.CompanyId).Include(p => p.PhoneType);
            return PartialView("_PhoneListView", list);
        }

        public ActionResult DeletePhone(int id)
        {
            var phone = db.CompanyPhones.Find(id);
            db.CompanyPhones.Remove(phone);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.CompanyPhones.Where(c => c.CompanyId == phone.CompanyId);
            return PartialView("_PhoneListView", list);

        }

        public ActionResult CreateAddress(int companyId)
        {
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "Name");
            ViewBag.StateId = new SelectList(db.States, "Id", "Abbr");
            var addr = new CompanyAddressViewModel() { CompanyId = companyId };

            return PartialView("_CreateCompanyAddress");
        }

        [HttpPost]
        public ActionResult CreateAddress([Bind(Include = "CompanyId,AddressTypeId,Street,Street2,City,StateId,PostalCode")] CompanyAddressViewModel address)
        {
            var companyAddress = new CompanyAddress()
            {
                CompanyId = address.CompanyId,
                AddressTypeId = address.AddressTypeId,
                Address = new Address()
                {
                    Street = address.Street,
                    Street2 = address.Street2,
                    City = address.City,
                    StateId = address.StateId,
                    PostalCode = address.PostalCode
                }
            };
            db.CompanyAddresses.Add(companyAddress);
            db.SaveChanges();
            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.CompanyAddresses.Where(a => a.CompanyId == address.CompanyId).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);

        }

        public ActionResult DeleteAddress(int id)
        {
            var address = db.CompanyAddresses.Find(id);
            db.CompanyAddresses.Remove(address);
            db.SaveChanges();
            
            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.CompanyAddresses.Where(a => a.CompanyId == address.CompanyId).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);
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
