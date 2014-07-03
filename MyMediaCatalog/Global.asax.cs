using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using MyMediaCatalog.Domain;
using MyMediaCatalog.Models;

namespace MyMediaCatalog
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeAutomapper();

        }

        private void InitializeAutomapper()
        {
            Mapper.CreateMap<Media, MediaViewModel>();
            Mapper.CreateMap<Person, PersonViewModel>();
            Mapper.CreateMap<Company, CompanyViewModel>();
        }
    }
}