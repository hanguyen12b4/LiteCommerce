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
    public class ShipperDAL : _BaseDAL, IShippers
    {
        public ShipperDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Shipper data)
        {
            int ShipperID = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Shippers
                                    (
	                                    ShipperName,
	                                    Phone
                                    ) VALUES 
                                    (
	                                    @ShipperName,
	                                    @Phone
                                    )
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                ShipperID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return ShipperID;
        }

        public int Count(string searchValue)
        {
            int countNumber = 0;
            string sValue = "%" + searchValue + "%";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Shippers
                                    WHERE ShipperName  LIKE @ShipperName";
                cmd.Parameters.AddWithValue("@ShipperName", sValue);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                countNumber = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            return countNumber;
        }

        public bool Delete(int ShipperID)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Shippers 
                                    WHERE ShipperID = @ShipperID  AND NOT EXISTS(SELECT * FROM Orders WHERE ShipperID = Shippers.ShipperID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }

        public Shipper GetById(int ShipperID)
        {
            Shipper data = new Shipper();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Shippers WHERE ShipperID = @ShipperID";
                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data.ShipperID = Convert.ToInt32(dbReader["ShipperID"]);
                        data.ShipperName = Convert.ToString(dbReader["ShipperName"]);
                        data.Phone = Convert.ToString(dbReader["Phone"]);

                    }
                    connection.Close();
                }
            }
            return data;
        }

        public List<Shipper> List(int page, int pageSize, string searchValue)
        {
            List<Shipper> data = new List<Shipper>();
            string sValue = "%" + searchValue + "%";
            int offset = (page - 1);
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM	Shippers
                                    WHERE	ShipperName LIKE @sValue
                                    ORDER BY ShipperID desc
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
                        data.Add(new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }

        public bool Update(Shipper data, int ShipperID)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE	Shippers
                                    SET		ShipperName = @ShipperName,
		                                    Phone  =  @Phone
                                    WHERE	ShipperID = @ShipperID 
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }
    }
}
