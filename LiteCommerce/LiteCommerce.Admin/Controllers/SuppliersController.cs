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
    public class SuppliersController : Controller
    {
        /// <summary>
        /// Tìm kiếm hiển thị danh sách các chức năng
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Supplier> suppliers = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);
            Models.SupplierPaginationQueryResult model = new Models.SupplierPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                searchValue = searchValue,
                Data = suppliers
            };
            return View(model);
        }


        /// <summary>
        /// Bổ sung nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return RedirectToAction("Index", "Suppliers");
        }

        public JsonResult Edit(int id)
        {
            Supplier supData = new Supplier();
            supData = DataService.getSupplier(id);
            return Json(supData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete(String SupplierID)
        {
            return Json(DataService.Delete(Int32.Parse(SupplierID)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(Supplier supData)
        {

            if (string.IsNullOrWhiteSpace(supData.SupplierName))
                ModelState.AddModelError("SupplierName", "Tên không được để trống");


            if (string.IsNullOrWhiteSpace(supData.ContactName))
                ModelState.AddModelError("ContactName", "Tên liên hệ không được để trống");

            if (string.IsNullOrWhiteSpace(supData.Address))
                supData.Address = "";

            if (string.IsNullOrWhiteSpace(supData.Country))
                supData.Country = "";

            if (string.IsNullOrWhiteSpace(supData.City))
                supData.City = "";

            if (string.IsNullOrWhiteSpace(supData.PostalCode))
                supData.PostalCode = "";

            if (string.IsNullOrWhiteSpace(supData.Phone))
                supData.Phone = "";

            if (supData.SupplierID == 0 )
            {
                DataService.AddSup(supData);
            }else
            {
                DataService.Update(supData, supData.SupplierID);
            }
            return null;
        }
    }
}