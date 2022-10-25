using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            String connectionString = "server=.;user id =sa;password=123;database=LiteCommerceDB";
            
            Employee data = new Employee()
            {
                FirstName = "Nguyễn" , 
                LastName = "Việt Hà" , 
                BirthDate= DateTime.Parse("12/10/1999"),
                Email = "hanguyen12b4@gmail.com", 
                Notes = "none", 
                Password = "none", 
                Photo = ""
            };
            var dal = new LiteCommerce.DataLayers.SQLServer.OrderDAL(connectionString);
            return Json(dal.List(1,5,"",default(DateTime), default(DateTime), 4), JsonRequestBehavior.AllowGet);
            
        }
    }
}