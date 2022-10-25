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
    public class CategoryDAL : _BaseDAL, ICategoriesDAL
    {
        public CategoryDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Category data)
        {
            int CategoryID = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Categories
                                    (
	                                    CategoryName,
	                                    Description
                                    ) VALUES 
                                    (
	                                    @CategoryName,
	                                    @Description
                                    )
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                CategoryID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return CategoryID;
        }

        public int Count(string searchValue)
        {
            int countNumber = 0;
            string sValue = "%" + searchValue + "%";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Categories
                                    WHERE CategoryName  LIKE @CategoryName";
                cmd.Parameters.AddWithValue("@CategoryName", sValue);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                countNumber = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            return countNumber;
        }

        public bool Delete(int CategoryID)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Categories 
                                    WHERE CategoryID = @CategoryID  AND NOT EXISTS(SELECT * FROM Products WHERE CategoryID = Categories.CategoryID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }

        public Category GetById(int CategoryID)
        {
            Category data = new Category();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Categories WHERE CategoryID = @CategoryID";
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data.CategoryID = Convert.ToInt32(dbReader["CategoryID"]);
                        data.CategoryName = Convert.ToString(dbReader["CategoryName"]);
                        data.Description = Convert.ToString(dbReader["Description"]);

                    }
                    connection.Close();
                }
            }
            return data;
        }

        public List<Category> List(int page, int pageSize, string searchValue)
        {
            List<Category> data = new List<Category>();
            string sValue = "%" + searchValue + "%";
            int offset = (page - 1);
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM	Categories
                                    WHERE	CategoryName LIKE @sValue
                                    ORDER BY CategoryID desc
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
                        data.Add(new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }

        public List<Category> getAllCategories()
        {
            List<Category> data = new List<Category>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM	Categories";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                        });
                    }
                    connection.Close();
                }
            }
            return data;
        }

        public bool Update(Category data, int CategoryID)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE	Categories
                                    SET		CategoryName = @CategoryName,
		                                    Description  =  @Description
                                    WHERE	CategoryID = @CategoryID 
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
            }
            return result;
        }
    }
}
