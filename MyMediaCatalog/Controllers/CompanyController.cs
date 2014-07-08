using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MyMediaCatalog.Data;
using MyMediaCatalog.Domain;
using MyMediaCatalog.Models;

namespace MyMediaCatalog.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly AppContext db = new AppContext();

        public ActionResult Index()
        {
            return View(db.Companies.Where(x => x.IsDeleted == false).ToList());
        }

        public ActionResult GetGridCompanyList([DataSourceRequest] DataSourceRequest request)
        {
            var list = db.Companies.Where(x => x.IsDeleted == false).Project().To<CompanyViewModel>();
            return Json(list.ToDataSourceResult(request));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = db.Companies.Find(id);
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
            if (!ModelState.IsValid) return View(company);

            company.DateCreated = DateTime.Now;
            company.DateModified = DateTime.Now;
            company.CreatedUser = User.Identity.Name;
            company.ModifiedUser = User.Identity.Name;

            db.Companies.Add(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var company = db.Companies.Find(id);
            if (company == null) { return HttpNotFound(); }

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,WebsiteUrl")] Company company)
        {
            if (!ModelState.IsValid) return View(company);

            company.DateModified = DateTime.Now;
            company.ModifiedUser = User.Identity.Name;
            db.Entry(company).State = EntityState.Modified;
            db.SaveChanges();

            if (Request.IsAjaxRequest()) { return new HttpStatusCodeResult(HttpStatusCode.OK); }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var company = db.Companies.Find(id);
            if (company == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            db.Companies.Remove(company);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return RedirectToAction("Index");

            var list = db.Companies.Where(x => x.IsDeleted == false).ToList();
            return PartialView("_CompanyListView", list);
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
            var companyphone = new CompanyPhone
            {
                PhoneTypeId = phone.PhoneTypeId,
                CompanyId = phone.CompanyId,
                Phone = new Phone
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
            if (phone == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

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
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Abbr");
            ViewBag.CountryList = new SelectList(db.Countries, "Id", "Abbr", 252);

            var addr = new CompanyAddressViewModel()
            {
                CompanyId = companyId
                //CountryId = db.Countries.FirstOrDefault(x => x.Abbr == "US").Id
            };
            return PartialView("_CreateAddress", addr);
        }

        [HttpPost]
        public ActionResult CreateAddress([Bind(Include = "CompanyId,AddressTypeId,Street,Street2,City,StateId,PostalCode,CountryId")] CompanyAddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                var address = new CompanyAddress
                {
                    CompanyId = addressViewModel.CompanyId,
                    AddressTypeId = addressViewModel.AddressTypeId,
                    Address = new Address
                    {
                        Street = addressViewModel.Street,
                        Street2 = addressViewModel.Street2,
                        City = addressViewModel.City,
                        StateId = addressViewModel.StateId,
                        PostalCode = addressViewModel.PostalCode,
                        CountryId = addressViewModel.CountryId, 
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name
                    }
                };
                db.CompanyAddresses.Add(address);
                db.SaveChanges();
                return Json(new { success = true });
            }
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "Name");
            ViewBag.StateId = new SelectList(db.States, "Id", "Abbr");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Abbr");

            return PartialView("_CreateAddress");

        }

        public ActionResult DeleteAddress(int id)
        {
            var address = db.CompanyAddresses.Find(id);
            if (address == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            db.CompanyAddresses.Remove(address);
            db.SaveChanges();

            if (!Request.IsAjaxRequest()) return new HttpStatusCodeResult(HttpStatusCode.OK);

            var list = db.CompanyAddresses.Where(a => a.CompanyId == address.CompanyId).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);
        }

        public ActionResult GetCompanyAddress(int id)
        {
            var list = db.CompanyAddresses.Where(a => a.CompanyId == id).Include(x => x.AddressType).Include(x => x.Address.State);
            return PartialView("_AddressListView", list);
        }

        public ActionResult GetCompanyList(string term)
        {
            if (term == null) term = Request.Params["filter[filters][0][value]"];
            //TODO: Code Smell. Refactor If..Else statement
            if (string.IsNullOrWhiteSpace(term))
            {
                var list = db.Companies.Where(x => x.IsDeleted == false).Project().To<CompanyViewModel>();
                return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.Companies.Where(x => x.IsDeleted == false).Where(x => x.Name.Contains(term)).Project().To<CompanyViewModel>();
                return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
            }
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
