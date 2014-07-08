using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using MyMediaCatalog.Data;
using MyMediaCatalog.net.webservicex.www;

namespace MyMediaCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppContext db = new AppContext();

        public ActionResult Index()
        {

            //var svc = new country();
            //var list = svc.GetCountries(); 

            //foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            //{
            //    RegionInfo info = new RegionInfo(cultureInfo.Name);

            //}
            //var list = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Where(x => x.Name.Contains("AB")); 


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetStatesList(int? countryId)
        {
            if (countryId != null)
            {
                var list = db.States.Where(x => x.CountryId == countryId).OrderBy(x => x.Abbr);

                return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
            }
            var uslist = db.States.Where(x => x.Country.Abbr == "US").OrderBy(x => x.Abbr);
            return Json(uslist.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCity(string term)
        {
            var url = string.Format("http://gd.geobytes.com/AutoCompleteCity?filter=US&q={0}", term);

            //var sb = new StringBuilder();
            //sb.Append("http://gd.geobytes.com/AutoCompleteCity?filter=US?&q=" + term);
            //sb.Append("&template=json.txt");
            //sb.Append("&IpAddress=");
            //sb.Append(ip);

            var webClient = new WebClient();
            
            var data = webClient.OpenRead(url);

            if (data == null) { return Json(""); }

            //var reader = new StreamReader(data);
            var reader = new StreamReader(webClient.OpenRead(url));
            var msg = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}