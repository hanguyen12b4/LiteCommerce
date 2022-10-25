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
    public class CustomerDAL : _BaseDAL, ICustomersDAL
    {
        public CustomerDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Customer data)
        {
            int CustomerID = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO  Customers
                                      (
	                                    CustomerName,
	                                    ContactName,
	                                    Address,
	                                    City,
	                                    PostalCode,
	                                    Country,
	                                    Email,
	                                    Password
                                      ) VALUES 
                                      (
	                                    @CustomerName,
	                                    @ContactName,
	                                    @Address,
	                                    @City,
	                                    @PostalCode,
	                                    @Country,
	                                    @Email,
	                                    @Password
                                      )
                                      SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                CustomerID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return CustomerID;
        }

        public int Count(string searchValue)
        {
            int countNumber = 0;
            string sValue = "%" + searchValue + "%";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Customers
                                    WHERE CustomerName  LIKE @CustomerName";
                cmd.Parameters.AddWithValue("@CustomerName", sValue);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                countNumber = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            return countNumber;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Employees 
                                    WHERE EmployeeID = @EmployeeID  AND NOT EXISTS(SELECT * FROM Orders WHERE EmployeeID = Employees.EmployeeID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }

        public Customer GetById(int id)
        {
            Customer data = new Customer();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data.CustomerID = Convert.ToInt32(dbReader["CustomerID"]);
                        data.CustomerName = Convert.ToString(dbReader["CustomerName"]);
                        data.ContactName = Convert.ToString(dbReader["ContactName"]);
                        data.Address = Convert.ToString(dbReader["Address"]);
                        data.City = Convert.ToString(dbReader["City"]);
                        data.Country = Convert.ToString(dbReader["Country"]);
                        data.Email = Convert.ToString(dbReader["Email"]);
                        data.Password = Convert.ToString(dbReader["Password"]);
                        data.PostalCode = Convert.ToString(dbReader["PostalCode"]);

                    }
                    connection.Close();
                }
            }
            return data;
        }

        public List<Customer> List(int page, int pageSize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            string sValue = "%" + searchValue + "%";
            int offset = (page - 1);
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM	Customers
                                    WHERE	CustomerName LIKE @sValue
                                    ORDER BY CustomerID desc
                                    OFFSET @offset ROWS
                                    FETCH NEXT @pagesize ROWS ONLY;";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@sValue", sValue);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pagesize", pageSize);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Customer()
                        {
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"])
                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }

        public bool Update(Customer data, int id)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"  UPDATE Customers
                                      SET	CustomerName = @CustomerName,
		                                    ContactName = @ContactName,
		                                    Address = @Address,
		                                    City = @City,
		                                    PostalCode = @PostalCode,
		                                    Country = @Country,
		                                    Email = @Email,
		                                    Password = @Password
                                      WHERE CustomerID = @CustomerID 
                                      SELECT @@IDENTITY
                                    ";
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                cmd.Parameters.AddWithValue("@CustomerID", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }
    }
}
