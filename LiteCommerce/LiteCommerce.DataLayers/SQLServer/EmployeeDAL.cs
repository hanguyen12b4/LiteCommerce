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
    public class EmployeeDAL : _BaseDAL, IEmployeesDAL
    {
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Employee data)
        {
            int EmployeeID = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Employees 
                                  (
	                                LastName,
	                                FirstName,
	                                BirthDate,
	                                Photo,
	                                Notes,
	                                Email,
	                                Password
                                  )VALUES 
                                  (
	                                @LastName,
	                                @FirstName,
	                                @BirthDate,
	                                @Photo,
	                                @Notes,
	                                @Email,
	                                @Password
                                  )
                                  SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                EmployeeID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return EmployeeID;
        }

        public int Count(string searchValue)
        {
            int countNumber = 0;
            string sValue = "%" + searchValue + "%";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Employees
                                    WHERE FirstName  LIKE @FirstName or LastName LIKE @LastName";
                cmd.Parameters.AddWithValue("@FirstName", sValue);
                cmd.Parameters.AddWithValue("@LastName", sValue);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                countNumber = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            return countNumber;
        }

        public bool Delete(int EmployeeID)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Employees 
                                    WHERE EmployeeID = @EmployeeID  AND NOT EXISTS(SELECT * FROM Orders WHERE EmployeeID = Employees.EmployeeID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }

        public Employee GetById(int EmployeeID)
        {
            Employee data = new Employee();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data.EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]);
                        data.FirstName = Convert.ToString(dbReader["FirstName"]);
                        data.LastName = Convert.ToString(dbReader["LastName"]);
                        data.Notes = Convert.ToString(dbReader["Notes"]);
                        data.Password = Convert.ToString(dbReader["Password"]);
                        data.Photo = Convert.ToString(dbReader["Photo"]);
                        data.Email = Convert.ToString(dbReader["Email"]);
                        data.BirthDate = Convert.ToDateTime(dbReader["BirthDate"]);

                    }
                    connection.Close();
                }
            }
            return data;
        }

        public List<Employee> List(int page, int pageSize, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            string sValue = "%" + searchValue + "%";
            int offset = (page - 1);
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM	Employees
                                    WHERE	LastName LIKE @sValue or FirstName LIKE @sValue
                                    ORDER BY EmployeeID desc
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
                        data.Add(new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Password = Convert.ToString(dbReader["Password"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }

        public bool Update(Employee data, int EmployeeID)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE	Employees
                                      SET		
			                                    LastName =@LastName,
			                                    FirstName =@FirstName,
			                                    BirthDate =@BirthDate,
			                                    Photo =@Photo,
			                                    Notes =@Notes,
			                                    Email =@Email,
			                                    Password =@Password
	                                    WHERE EmployeeId = @EmployeeId
                                      SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }
    }
}
