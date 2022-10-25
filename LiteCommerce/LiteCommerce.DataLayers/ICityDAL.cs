using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai báo xử lí liên quan đến thành phố
    /// </summary>
    public interface ICityDAL
    {
        /// <summary>
        /// Danh sách tất cả các thành phố
        /// </summary>
        /// <returns>List city</returns>
        List<City> List();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>List city by countryname</returns>
        List<City> List(String countryName);
    }
}
