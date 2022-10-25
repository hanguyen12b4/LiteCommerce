using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICustomersDAL 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Customer> List(int page, int pageSize, String searchValue);
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
        Customer GetById(int id);
        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns> generate key </returns>
        int Add(Customer data);
        /// <summary>
        /// Edit Customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true/false</returns>
        bool Update(Customer data, int id);
        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns> true false </returns>
        bool Delete(int id);
    }
}
