using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai báo lấy danh sách nhà cung cấp
    /// </summary>
    public interface ISuppliersDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Supplier> List(int page , int pageSize , String searchValue);
        /// <summary>
        /// Số
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(String searchValue);
        /// <summary>
        /// lấy thông tin theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Supplier GetById(int id);
        /// <summary>
        /// Add Suppliers
        /// </summary>
        /// <param name="data"></param>
        /// <returns> generate key </returns>
        int Add(Supplier data);
        /// <summary>
        /// Edit supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true/false</returns>
        bool Update(Supplier data , int id);
        /// <summary>
        /// Delete Suppliers
        /// </summary>
        /// <param name="id"></param>
        /// <returns> true false </returns>
        bool Delete(int id);
    }
}
