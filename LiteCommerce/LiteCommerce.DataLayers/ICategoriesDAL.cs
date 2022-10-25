using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICategoriesDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Category> List(int page , int pageSize, String searchValue);

        int Count(String searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        bool Update(Category data , int CategoryID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        bool Delete( int CategoryID);

        Category GetById(int CategoryID);

        List<Category> getAllCategories();
    }
}
