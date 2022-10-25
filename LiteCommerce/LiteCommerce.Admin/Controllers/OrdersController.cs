using LiteCommerce.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index( )
        {
            return View();
        }
    public ActionResult List(string page, string OrderTimeFrom, string OrderTimeTo,
            string searchValue, int status)
        {
            DateTime OF = new DateTime();
            DateTime OT = new DateTime();
            if (!string.IsNullOrEmpty(OrderTimeFrom))
            {
                OF = DateTime.Parse(OrderTimeFrom);
            }else
            {
                OF = DateTime.Parse("1980-01-01");
            }
            if (!string.IsNullOrEmpty(OrderTimeTo))
            {
                OT = DateTime.Parse(OrderTimeTo);
            }else
            {
                OT = DateTime.Parse("1980-01-01");
            }
            
            int rowCount = 0;
            int pageSize = 10;
            var model = OrderService.List(Convert.ToInt32(page), pageSize, searchValue, OF, OT, status, out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;

            int PageCount = rowCount / pageSize;
            ViewBag.PageCount = PageCount;
            if (rowCount % pageSize > 0)
            {
                PageCount += 1;
                ViewBag.PageCount = PageCount;
            }
            return View(model);
        }
        public ActionResult OrderDetail(int id)
        {
            var model = OrderService.getOrderDetailById(id);
            return View(model);
        }



    }
     
}