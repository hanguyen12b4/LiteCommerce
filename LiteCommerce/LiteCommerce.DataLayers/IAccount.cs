using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IAccount
    {
        /// <summary>
        /// Kiểm  tra thông tin đăng nhập . Nếu đúng thì trả về 1 account , 
        /// ngược lại thì trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Account Authorize(String userName, String password);
        /// <summary>
        /// Thay đổi mk
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(String email, String newPassword);


    }
}
