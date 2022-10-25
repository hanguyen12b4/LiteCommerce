using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BussinessLayers
{
    public static class AccountService
    {
        private static IAccount AccountDB; 

        public static void Init(DatabaseTypes dbType, String connectionString , AccountTypes accountType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    if(accountType == AccountTypes.Employee)
                    {
                        AccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
                    }else
                    {
                        AccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);

                    }
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }
        /// <summary>
        /// Kiểm tra tài khoản và mật khẩu hợp lệ
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Account Authorize (String userName ,String  password)
        {
            return AccountDB.Authorize(userName, password); 
        }

        public static bool ChangePassword (String email,String newPassword)
        {
            return AccountDB.ChangePassword(email, newPassword);
        }
    }

    public enum AccountTypes
    {
        /// <summary>
        /// Nhân viên dùng ứng dụng LiteCommerce.Admin
        /// </summary>
        Employee,
        /// <summary>
        /// Khách hàng  dùng ứng dụng LiteCommerce.Admin
        /// </summary>
        Customer
    }
}
