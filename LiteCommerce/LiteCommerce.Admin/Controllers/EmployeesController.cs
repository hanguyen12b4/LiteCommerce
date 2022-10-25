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
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Employee> employees = DataService.ListEmployees(page, pageSize, searchValue, out rowCount);
            Models.EmployeePaginationQueryResult model = new Models.EmployeePaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                searchValue = searchValue,
                Data = employees
            };
            return View(model);
        }

        public ActionResult Save(Employee empData)
        {

            if (string.IsNullOrWhiteSpace(empData.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");


            if (string.IsNullOrWhiteSpace(empData.LastName))
                ModelState.AddModelError("LastName", "Tên liên hệ không được để trống");

            if (string.IsNullOrWhiteSpace(empData.BirthDate.ToString()))
                ModelState.AddModelError("BirthDate", "Ngày sinh không được để trống");

            if (string.IsNullOrWhiteSpace(empData.Photo))
                empData.Photo = "";

            if (string.IsNullOrWhiteSpace(empData.Notes))
                empData.Notes = "";

            if (string.IsNullOrWhiteSpace(empData.Email))
                empData.Email = "";

            if (string.IsNullOrWhiteSpace(empData.Password))
                empData.Password = "";

            if (empData.EmployeeID == 0)
            {
                if(empData.Password!= null)
                {
                    empData.Password=CryptHelper.Md5(empData.Password);

                }
                DataService.AddEmp(empData);
            }
            else
            {
                if (empData.Password != null)
                {
                    empData.Password = CryptHelper.Md5(empData.Password);

                }
                DataService.UpdateEmp(empData, empData.EmployeeID);
            }
            return null;
        }

        public JsonResult Edit(int id)
        {
            Employee employeeData = new Employee();
            employeeData = DataService.getEmployee(id);
            return Json(employeeData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int EmployeeID)
        {
            return Json(DataService.DeleteEmp(EmployeeID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string searchValue)
        {
            return RedirectToAction("Index", "Employees");
        }
    }
}