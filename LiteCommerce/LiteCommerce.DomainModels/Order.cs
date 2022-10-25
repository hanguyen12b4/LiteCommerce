using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderTime { get; set; }
        public int EmployeeID { get; set; }
        public Nullable<DateTime> AcceptTime { get; set; }
        public int ShipperID { get; set; }
        public Nullable<DateTime> ShippedTime { get; set; }
        public Nullable<DateTime> FinishedTime { get; set; }
        public int Status { get; set; }

    }

    public class OrderClear : Order
    {
        public string CustomerName { get; set; }
        public string ShipperName { get; set; }
        public string EmployeeName { get; set; }
        public string OrderDescription { get; set; }
    }

    public class OrderDetail : Order
    {
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public string ProductName { get; set; }

    }

}
