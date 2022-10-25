using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    ///     tài khoản để đăng nhập hệ thống
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Mã tài khoản , mã Employee , mã Customer
        /// </summary>
        public String AccountID { get ; set; }
        /// <summary>
        /// Tên gọi 
        /// </summary>
        public String FullName { get ; set; }
        /// <summary>
        /// Email
        /// </summary>
        public String Email { get; set; }
    }
}
