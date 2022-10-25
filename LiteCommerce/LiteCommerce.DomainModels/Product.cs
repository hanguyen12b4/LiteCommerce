using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        public String ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public String Unit { get; set; }
        public decimal Price { get; set; }
        public String Photo { get; set; }
    }

    public class ProductEx : Product
    {
        public List<ProductAttribute> Attributes { get; set; }
        public List<ProductGallery> Galleries { get; set; }

    }
}
