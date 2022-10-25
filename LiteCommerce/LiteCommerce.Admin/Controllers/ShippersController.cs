using LiteCommerce.BussinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class ShippersController : Controller
    {
        // GET: Shippers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, string searchValue = "")
        {
            ViewBag.Title = "Quản lí nhà vận chuyển";
            int pageSize = 100;
            int rowCount = 0;
            List<Shipper> shippers = DataService.ListShippers(page, pageSize, searchValue, out rowCount);
            Models.ShipperPaginationQueryResult model = new Models.ShipperPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                searchValue = searchValue,
                Data = shippers
            };
            return View(model);
            
        }

        public JsonResult Save(Shipper shipperData)
        {
            if (string.IsNullOrWhiteSpace(shipperData.ShipperName))
                ModelState.AddModelError("ShipperName", "Tên sản phẩm không được để trống");

            if (string.IsNullOrWhiteSpace(shipperData.Phone))
                shipperData.Phone = "";

            if (shipperData.ShipperID == 0)
            {
                DataService.AddShipper(shipperData);
            }
            else
            {
                DataService.UpdateShipper(shipperData, shipperData.ShipperID);
            }
            return null;
        }

        [HttpGet]
        public JsonResult Edit(int id)
        {
            Shipper shipperData = new Shipper();
            shipperData = DataService.getShipper(id);
            return Json(shipperData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int ShipperID)
        {
            return Json(DataService.DeleteShipper(ShipperID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string searchValue)
        {
            return RedirectToAction("Index", "Shippers");
        }
    }
}