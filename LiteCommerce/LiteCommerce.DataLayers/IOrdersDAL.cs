using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public interface IOrdersDAL
    {
        List<OrderClear> List(int page, int pageSize, string searchValue, DateTime OrderTimeFrom, DateTime OrderTimeTo, int status);
        int Count(string searchValue, DateTime OrderTimeFrom, DateTime OrderTimeTo, int status);

        List<OrderDetail> getDetailById(int OrderId);
    }
}
