using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    /// <summary>
    /// kết quả truy vấn nhà cung cấp theo kiểu phân trang
    /// </summary>
    public class SupplierPaginationQueryResult : BasePaginationQueryResult
    {
        public List <Supplier> Data { get; set; }
    }
}