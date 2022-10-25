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
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, string searchValue = "")
        {
            ViewBag.Title = "Quản lí thông tin khách hàng";
            int pageSize = 10;
            int rowCount = 0;
            List<Customer> customers = DataService.ListCustomers(page, pageSize, searchValue, out rowCount);
            Models.CustomerPaginationQueryResult model = new Models.CustomerPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                searchValue = searchValue,
                Data = customers
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Customer customerData)
        {

            if (string.IsNullOrWhiteSpace(customerData.ContactName))
                ModelState.AddModelError("SupplierName", "Tên không được để trống");


            if (string.IsNullOrWhiteSpace(customerData.ContactName))
                ModelState.AddModelError("ContactName", "Tên liên hệ không được để trống");

            if (string.IsNullOrWhiteSpace(customerData.Address))
                customerData.Address = "";

            if (string.IsNullOrWhiteSpace(customerData.Country))
                customerData.Country = "";

            if (string.IsNullOrWhiteSpace(customerData.City))
                customerData.City = "";

            if (string.IsNullOrWhiteSpace(customerData.PostalCode))
                customerData.PostalCode = "";

            if (string.IsNullOrWhiteSpace(customerData.Email))
                customerData.Email = "";

            if (string.IsNullOrWhiteSpace(customerData.Password))
                customerData.Password = "";

            if (customerData.CustomerID == 0)
            {
                DataService.addCustomer(customerData);
            }
            else
            {
                DataService.UpdateCus(customerData, customerData.CustomerID);
            }
            return null;
        }

        public JsonResult Edit(int id)
        {
            Customer customerData = new Customer();
            customerData = DataService.getCustomer(id);
            return Json(customerData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int CustomerID)
        {
            return Json(DataService.DeleteCus(CustomerID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string searchValue)
        {
            return RedirectToAction("Index", "Customers");
        }
    }
}