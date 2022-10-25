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
    public class ProductGallery
    {
        public long GalleryID { get; set; }
        public int ProductID { get; set; }
        public String Photo { get; set; }
        public String Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool isHidden { get; set; }
    }
}
