using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    ///  Khai báo chức năng xử lí dữ liệu liên quan đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// get list country
        /// </summary>
        /// <returns>List country</returns>
        List<Country> List();
    }
}
