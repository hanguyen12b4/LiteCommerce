using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LiteCommerce.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Khởi tạo các lớp tác nghiệp
            string cnStr = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            LiteCommerce.BussinessLayers.DataService.Init(BussinessLayers.DatabaseTypes.SQLServer,cnStr);
            LiteCommerce.BussinessLayers.AccountService.Init(BussinessLayers.DatabaseTypes.SQLServer, cnStr,BussinessLayers.AccountTypes.Employee);
            LiteCommerce.BussinessLayers.ProductService.Init(BussinessLayers.DatabaseTypes.SQLServer, cnStr);
            LiteCommerce.BussinessLayers.OrderService.Init(BussinessLayers.DatabaseTypes.SQLServer, cnStr);



        }
    }
}
