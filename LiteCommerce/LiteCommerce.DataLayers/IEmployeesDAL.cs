using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeesDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, String searchValue);

        int Count(String searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        bool Update(Employee data, int EmployeeID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        bool Delete(int EmployeeID);

        Employee GetById(int EmployeeID);
    }
}
