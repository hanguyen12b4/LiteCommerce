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
    public class CityDAL : _BaseDAL, ICityDAL
    {
        public CityDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<City> List()
        {
            {
                List<City> data = new List<City>();
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * FROM Cities";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new City()
                            {
                                CityName = Convert.ToString(dbReader["CityName"]),
                                CountryName = Convert.ToString(dbReader["CountryName"])
                            });

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
        /// <param name="countryName"></param>
        /// <returns></returns>
        public List<City> List(string countryName)
        {
            {
                List<City> data = new List<City>();
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * FROM Cities WHERE CountryName = @countryName";
                    cmd.Parameters.AddWithValue("@countryName", countryName);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new City()
                            {
                                CityName = Convert.ToString(dbReader["CityName"]),
                                CountryName = Convert.ToString(dbReader["CountryName"])
                            });

                        }
                        connection.Close();
                    }
                }
                return data;
            }
        }
    }
}
