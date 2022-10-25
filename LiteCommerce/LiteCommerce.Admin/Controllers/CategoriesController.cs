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
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, string searchValue = "")
        {
            ViewBag.Title = "Quản lí loại hàng";
            int pageSize = 10;
            int rowCount = 0;
            List<Category> categories = DataService.ListCategories(page, pageSize, searchValue, out rowCount);
            Models.CategoryPaginationQueryResult model = new Models.CategoryPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                searchValue = searchValue,
                Data = categories
            };
            return View(model);
        }

       [HttpGet]
        public JsonResult Edit(int id)
        {
            Category categorydata = new Category();
            categorydata = DataService.getCategory(id);
            return Json(categorydata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save (Category categoryData )
        {
            if (string.IsNullOrWhiteSpace(categoryData.CategoryName))
                ModelState.AddModelError("CategoryName", "Tên sản phẩm không được để trống");

            if (string.IsNullOrWhiteSpace(categoryData.Description))
                categoryData.Description = "";

            if (categoryData.CategoryID == 0)
            {
                DataService.AddCategory(categoryData);
            }
            else
            {
                DataService.UpdateCag(categoryData, categoryData.CategoryID);
            }
            return null;
        }
        [HttpPost]
        public JsonResult Delete(int CategoryID)
        {
            return Json(DataService.DeleteCag(CategoryID), JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult getAllCategories()
        {
            return Json(DataService.getAllCategories(), JsonRequestBehavior.AllowGet);
        }


    }
}