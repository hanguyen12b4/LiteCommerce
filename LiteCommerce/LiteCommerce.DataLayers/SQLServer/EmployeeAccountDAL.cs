using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class EmployeeAccountDAL : _BaseDAL, IAccount
    {
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Authorize(string userName, string password)
        {
            Account data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT EmployeeID , FirstName, LastName , Email FROM Employees WHERE Email = @email AND Password = @password ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@email", userName);
                cmd.Parameters.AddWithValue("@password", password);
                using(SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if(dbReader.Read())
                    {
                        data = new Account()
                        {
                            AccountID = dbReader["EmployeeID"].ToString(),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Email = dbReader["Email"].ToString()
                        };
                    }
                }
                connection.Close();

            }
            return data; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string email, string newPassword)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Employees 
                                    SET		Password = @newPassword
                                    WHERE	Email = @email
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@newPassword", newPassword);
                cmd.Parameters.AddWithValue("@email", email);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }
    }
}
