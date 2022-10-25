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
    /// <summary>
    /// 
    /// </summary>
    public class SupplierDAL : _BaseDAL, ISuppliersDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Supplier data)
        {
            int supplierId = 0;
            using (SqlConnection connection  = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Suppliers
                                    (
	                                    SupplierName
	                                    ,ContactName
	                                    ,Address
	                                    ,City
	                                    ,PostalCode
	                                    ,Country
	                                    ,Phone
                                    ) 
                                    VALUES 
                                    (
	                                    @SupplierName
	                                    ,@ContactName
	                                    ,@Address
	                                    ,@City
	                                    ,@PostalCode
	                                    ,@Country
	                                    ,@Phone
                                    )
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@SupplierName",data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                supplierId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            
            return supplierId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int countNumber = 0;
            string sValue = "%" + searchValue + "%";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Suppliers
                                    WHERE SupplierName  LIKE @SupplierName";
                cmd.Parameters.AddWithValue("@SupplierName", sValue);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                countNumber = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            return countNumber;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Suppliers 
                                    WHERE SupplierID = @supplierId  AND NOT EXISTS(SELECT * FROM Products WHERE SupplierID = Suppliers.SupplierID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@supplierId", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Supplier GetById(int id)
        {
            {
                Supplier data = new Supplier();
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * FROM Suppliers WHERE SupplierID = @SupplierID";
                    cmd.Parameters.AddWithValue("@SupplierID", id);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data.SupplierID = Convert.ToInt32(dbReader["SupplierID"]);
                            data.SupplierName = Convert.ToString(dbReader["SupplierName"]);
                            data.ContactName = Convert.ToString(dbReader["ContactName"]);
                            data.Address = Convert.ToString(dbReader["Address"]);
                            data.City = Convert.ToString(dbReader["City"]);
                            data.Country = Convert.ToString(dbReader["Country"]);
                            data.Phone = Convert.ToString(dbReader["Phone"]);
                            data.PostalCode = Convert.ToString(dbReader["PostalCode"]);
                        }
                        connection.Close();
                    }
                }
                return data;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Supplier> List(int page, int pageSize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
            string sValue = "%" + searchValue + "%";
            int offset = (page - 1);
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM	Suppliers
                                    WHERE	SupplierName LIKE @sValue
                                    ORDER BY SupplierID desc
                                    OFFSET @offset ROWS
                                    FETCH NEXT @pagesize ROWS ONLY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@sValue", sValue);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pagesize", pageSize);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"])
                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data, int id)
        {
            bool result = false;
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = @"UPDATE	Suppliers
                                        SET		SupplierName =@SupplierName,
		                                        ContactName = @ContactName,
		                                        Address = @Address,
		                                        City = @City,
		                                        PostalCode =@PostalCode,
		                                        Country = @Country,
		                                        Phone= @Phone
                                        WHERE SupplierID =  @SupplierID";
                    cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                    cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                    cmd.Parameters.AddWithValue("@Address", data.Address);
                    cmd.Parameters.AddWithValue("@City", data.City);
                    cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                    cmd.Parameters.AddWithValue("@Country", data.Country);
                    cmd.Parameters.AddWithValue("@Phone", data.Phone);
                    cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                int rowsAffected = cmd.ExecuteNonQuery();
                    result = rowsAffected > 0;
                }
                return result;
        }
    }
}
