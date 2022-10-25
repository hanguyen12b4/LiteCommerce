using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// danh sách mặt hàng , tìm kiếm , lọc dữ liệu , phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="CategoryID">Mã loại hàng cần lấy (0 nếu lấy tất cả)</param>
        /// <param name="SupplierID">Mã nhà cung cấp ( 0 nếu lấy tất cả)</param>
        /// <param name="searchValue"> tên mặt hàng cần tìm ("" nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, int CategoryID, int SupplierID, string searchValue);
        /// <summary>
        /// Đếm số lượng mặt hàng
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="SupplierID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int CategoryID, int SupplierID, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Product Get(int productID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        ProductEx GetEx(int productID);
        /// <summary>
        /// /
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        bool Delete(int productID);
        /// <summary>
        /// lấy danh sách các thuộc tính của mặt hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long attributeID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        bool DeleteAttribute(long attributeID);


        List<ProductGallery> ListGalleries(int productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddGallery(ProductGallery data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryID);
        List<Supplier> getListSupplierByCategID(int CategoryID);
    }
}
