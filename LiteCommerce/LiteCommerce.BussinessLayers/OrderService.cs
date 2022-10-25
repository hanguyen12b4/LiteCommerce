using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BussinessLayers
{
    /// <summary>
    /// OrderService
    /// </summary>
    public class OrderService
    {
        private static IOrdersDAL OrderDB;
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    OrderDB = new DataLayers.SQLServer.OrderDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }

        public  static List<OrderClear>  List(int page, int pageSize, string searchValue, DateTime OrderTimeFrom, DateTime OrderTimeTo, int status, out int rowCount)
        {
            rowCount = OrderDB.Count(searchValue, OrderTimeFrom, OrderTimeTo, status);
            return OrderDB.List(page, pageSize, searchValue, OrderTimeFrom, OrderTimeTo, status);
        }
        
        public static List<OrderDetail> getOrderDetailById (int OrderId)
        {
            return OrderDB.getDetailById(OrderId);
        }
    }
}
