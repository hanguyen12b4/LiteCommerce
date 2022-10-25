using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;
using System.Data;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class OrderDAL : _BaseDAL, IOrdersDAL
    {
        public OrderDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="OrderTimeFrom"></param>
        /// <param name="OrderTimeTo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int Count(string searchValue, DateTime OrderTimeFrom, DateTime OrderTimeTo, int status)
        {
            int count = 0;
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Orders
		                                    INNER JOIN Customers on Orders.CustomerID = Customers.CustomerID
                                    WHERE	(Customers.CustomerName LIKE @searchValue OR @searchValue = '')
		                                    AND (OrderTime >= @OrderTimeFrom OR @OrderTimeFrom = '1980-01-01')
		                                    AND (OrderTime <= @OrderTimeTo OR @OrderTimeTo = '1980-01-01') 
                                            AND (Orders.Status = @Status OR @Status= 10)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@OrderTimeFrom", OrderTimeFrom);
                cmd.Parameters.AddWithValue("@OrderTimeTo", OrderTimeTo);
                cmd.Parameters.AddWithValue("@Status", status);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public List<OrderDetail> getDetailById(int OrderId)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	Orders.* , OrderDetails.*,Products.ProductName
                                    FROM	Orders
		                                    INNER JOIN OrderDetails on OrderDetails.OrderId = Orders.OrderId
		                                    INNER JOIN Products on Products.ProductId = OrderDetails.ProductId
                                    WHERE	Orders.OrderId = @OrderId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderDetail()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            AcceptTime = string.IsNullOrEmpty(dbReader["AcceptTime"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dbReader["AcceptTime"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            FinishedTime = string.IsNullOrEmpty(dbReader["FinishedTime"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dbReader["FinishedTime"]),
                            ShippedTime = string.IsNullOrEmpty(dbReader["ShippedTime"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dbReader["ShippedTime"]),
                            Status = Convert.ToInt32(dbReader["Status"]),
                            OrderDetailID = Convert.ToInt32(dbReader["OrderDetailID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            Quantity = Convert.ToInt32(dbReader["Quantity"]),
                            SalePrice = Convert.ToDecimal(dbReader["SalePrice"])                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="OrderTimeFrom"></param>
        /// <param name="OrderTimeTo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<OrderClear> List(int page, int pageSize, string searchValue, DateTime OrderTimeFrom, DateTime OrderTimeTo, int status)
        {
           List<OrderClear> data = new List<OrderClear>();
            if(searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            int offset = (page - 1) * pageSize;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	Orders.*,Customers.CustomerName,Employees.FirstName,Employees.LastName,Shippers.ShipperName,OrderStatus.Description
                                    FROM	Orders
		                                    INNER JOIN Customers on Orders.CustomerID = Customers.CustomerID
											INNER JOIN Shippers	 on Orders.ShipperID=Shippers.ShipperID
											INNER JOIN Employees on Employees.EmployeeID = Orders.EmployeeID
                                            INNER JOIN OrderStatus on OrderStatus.Status = Orders.Status 	
                                    WHERE	(Customers.CustomerName LIKE @searchValue OR @searchValue = '')
		                                    AND (OrderTime >= @OrderTimeFrom OR @OrderTimeFrom = '1980-01-01')
		                                    AND (OrderTime <= @OrderTimeTo OR @OrderTimeTo = '1980-01-01') 
                                            AND (Orders.Status = @Status OR @Status= 10)
                                    ORDER BY OrderID desc
                                    OFFSET @offset ROWS
                                    FETCH NEXT @pagesize ROWS ONLY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pagesize", pageSize);
                cmd.Parameters.AddWithValue("@OrderTimeFrom", OrderTimeFrom);
                cmd.Parameters.AddWithValue("@OrderTimeTo", OrderTimeTo);
                cmd.Parameters.AddWithValue("@Status", status);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderClear()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            AcceptTime =string.IsNullOrEmpty(dbReader["AcceptTime"].ToString()) ?  (DateTime?)null : Convert.ToDateTime(dbReader["AcceptTime"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            FinishedTime = string.IsNullOrEmpty(dbReader["FinishedTime"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dbReader["FinishedTime"]),
                            ShippedTime = string.IsNullOrEmpty(dbReader["ShippedTime"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dbReader["ShippedTime"]),
                            Status = Convert.ToInt32(dbReader["Status"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            EmployeeName = Convert.ToString(dbReader["FirstName"]) + " " + Convert.ToString(dbReader["LastName"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
                            OrderDescription = Convert.ToString(dbReader["Description"])
                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }
    }
}
