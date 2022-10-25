using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IShippers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Shipper> List(int page, int pageSize, String searchValue);

        int Count(String searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        bool Update(Shipper data, int ShipperID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        bool Delete(int ShipperID);

        Shipper GetById(int ShipperID);
    }
}
