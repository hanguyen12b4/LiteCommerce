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
    public class ProductsController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="CategoryID"></param>
        /// <param name="SupplierID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult List(int page = 1, int CategoryID = 0, int SupplierID = 0, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 5;
            var model = ProductService.List(page, pageSize, CategoryID, SupplierID, searchValue, out rowCount);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProduct()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = ProductService.GetEx(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public JsonResult SaveProduct(Product product)
        {
            var result = true;
            if (string.IsNullOrWhiteSpace(product.ProductName))
                result = false;
            if (string.IsNullOrWhiteSpace(product.Unit))
                result = false;
            if (string.IsNullOrWhiteSpace(product.Price.ToString()))
                result = false;
            if (string.IsNullOrWhiteSpace(product.Photo))
                product.Photo = "";


            if (result)
            {
                if(product.ProductID == 0)
                {
                    return Json(ProductService.Add(product), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(ProductService.Update(product), JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var model = ProductService.Get(id);
            if (Request.HttpMethod == "GET")
            {
                return View(model);
            }
            else
            {
                return Json(ProductService.Delete(id), JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttributeID"></param>
        /// <returns></returns>
        public JsonResult GetAttribute(long AttributeID)
        {
            return Json(ProductService.GetAttribute(AttributeID), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GalleryID"></param>
        /// <returns></returns>
        public JsonResult GetGallery(long GalleryID)
        {
            return Json(ProductService.GetGallery(GalleryID), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productAttribute"></param>
        /// <returns></returns>
        public JsonResult SaveAttribute(ProductAttribute productAttribute)
        {
            long ProductID = 0;
            if (productAttribute.AttributeID == 0)
            {
                ProductID = ProductService.AddAttribute(productAttribute);
                return Json(ProductID, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ProductID = productAttribute.ProductID;
                return Json(ProductService.UpdateAttribute(productAttribute), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productGallery"></param>
        /// <returns></returns>
        public JsonResult SaveGallery(ProductGallery productGallery)
        {
            long ProductID = 0;
            if (string.IsNullOrWhiteSpace(productGallery.Photo))
                productGallery.Photo = "";
            if (string.IsNullOrWhiteSpace(productGallery.Description))
                productGallery.Photo = "";
            if (productGallery.GalleryID == 0)
            {
                ProductID = ProductService.AddGallery(productGallery);
                return Json(ProductID, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ProductID = productGallery.ProductID;
                return Json(ProductService.UpdateGallery(productGallery), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeIds"></param>
        /// <returns></returns>
        public ActionResult DeleteAttributes(int id, long[] attributeIds)
        {
            if (attributeIds == null || attributeIds.Length == 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            ProductService.DeleteAttributes(attributeIds);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="galleryIds"></param>
        /// <returns></returns>
        public ActionResult DeleteGalleries(int id, long[] galleryIds)
        {
            if (galleryIds == null || galleryIds.Length == 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            ProductService.DeleteGalleries(galleryIds);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public JsonResult ListSupplierByCategID(int CategoryID)
        {
            return Json(ProductService.getListSupplierByCategID(CategoryID), JsonRequestBehavior.AllowGet);
        }
    }
}