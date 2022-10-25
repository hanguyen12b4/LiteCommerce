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
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Product data)
        {
            int productID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Products
                                    (
	                                    ProductName,
	                                    SupplierID,
	                                    CategoryID,
	                                    Unit,
	                                    Price,
	                                    Photo
                                    )VALUES 
                                    (
	                                    @ProductName,
	                                    @SupplierID,
	                                    @CategoryID,
	                                    @Unit,
	                                    @Price,
	                                    @Photo
                                    )
                                    SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                productID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return productID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddAttribute(ProductAttribute data)
        {
            long attributeID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes
                                    (
	                                    AttributeName,
	                                    AttributeValue,
	                                    DisplayOrder,
	                                    ProductID
                                    )
                                    VALUES 
                                    (
	                                    @AttributeName,
                                        @AtrributeValue,
                                        @DisplayOrder,
                                        @ProducID
                                    )
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@ProducID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AtrributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                attributeID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return attributeID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddGallery(ProductGallery data)
        {
            int galleryID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductGallery
                                    (
                                        Photo,
                                        Description,
                                        ProductID,
                                        DisplayOrder,
                                        IsHidden
                                    )
                                    VALUES
                                    (
                                        @Photo,
                                        @Description,
                                        @ProductID,
                                        @DisplayOrder,
                                        @IsHidden
                                    )
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.isHidden);
                galleryID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return galleryID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="SupplierID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(int CategoryID, int SupplierID, string searchValue)
        {
            int count = 0;
            if(searchValue != "")
            {
                searchValue = "%" +searchValue + "%";
            }
            using (SqlConnection connection = GetConnection ())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Products
                                    WHERE	(@categoryID = 0 OR CategoryID = @categoryID) 
		                                    AND (@supplierID = 0 OR SupplierID = @supplierID)
		                                    AND (@searchValue = '' OR ProductName LIKE @searchValue)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@categoryID", CategoryID); 
                cmd.Parameters.AddWithValue("@supplierID", SupplierID);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool Delete(int productID)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Products
                                    WHERE ProductID = @ProductID	AND NOT EXISTS (SELECT * FROM OrderDetails WHERE ProductID = @ProductID)
                                    
                                    DELETE FROM ProductGallery 
                                    WHERE ProductID = @ProductID	AND NOT EXISTS (SELECT * FROM OrderDetails WHERE ProductID = @ProductID)
                                    
                                    DELETE FROM ProductAttributes 
                                    WHERE ProductID = @ProductID	AND NOT EXISTS (SELECT * FROM OrderDetails WHERE ProductID = @ProductID)
                                    ";
                cmd.Parameters.AddWithValue("@ProductID", productID);
                isDeleted = cmd.ExecuteNonQuery() > 0;
            }
            return isDeleted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public bool DeleteAttribute(long attributeID)
        {
            bool result = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes WHERE AttributeID = @AttributeID";

                cmd.Parameters.AddWithValue("@AttributeID", attributeID);

                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns></returns>
        public bool DeleteGallery(long galleryID)
        {
            bool result = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductGallery WHERE GalleryID = @galleryID";

                cmd.Parameters.AddWithValue("@galleryID", galleryID);

                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Product Get(int productID)
        {
            Product data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from Products where ProductID = @productId";
                cmd.Parameters.AddWithValue("@productId", productID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToInt32(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                        };
                    }
                }
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public ProductAttribute GetAttribute(long attributeID)
        {
            ProductAttribute data = new ProductAttribute();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID = @attributeID";
                cmd.Parameters.AddWithValue("@attributeID", attributeID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductAttribute()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        };
                    }
                }
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public ProductEx GetEx(int productID)
        {
            ProductEx data = new ProductEx();
            List<ProductAttribute> productAttribute = ListAttributes(productID);
            List<ProductGallery> productGallery = ListGalleries(productID);
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from Products where ProductID = @productId";
                cmd.Parameters.AddWithValue("@productId", productID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductEx()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Attributes = productAttribute,
                            Galleries = productGallery
                        };
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns></returns>
        public ProductGallery GetGallery(long galleryID)
        {
            ProductGallery data = new ProductGallery();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM ProductGallery where GalleryID = @galleryID";
                cmd.Parameters.AddWithValue("@galleryID", galleryID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductGallery()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            isHidden = Convert.ToBoolean(dbReader["isHidden"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        };
                    }
                }
            }

            return data;
        }

        public List<Supplier> getListSupplierByCategID(int CategoryID)
        {
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	DISTINCT Suppliers.SupplierID, Suppliers.SupplierName
                                    FROM	Products
		                            INNER JOIN Suppliers on Suppliers.SupplierID = Products.SupplierID
                                    WHERE	CategoryID = @CategoryID";
                
                if(CategoryID == 0)
                {
                    cmd.CommandText = @"SELECT	DISTINCT Suppliers.SupplierID, Suppliers.SupplierName
                                    FROM	Products
		                            INNER JOIN Suppliers on Suppliers.SupplierID = Products.SupplierID";
                }
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"])
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
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="CategoryID"></param>
        /// <param name="SupplierID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Product> List(int page, int pageSize, int CategoryID, int SupplierID, string searchValue)
        {
            List<Product> data = new List<Product>();
            using (SqlConnection connection = GetConnection())
            {
                if(searchValue != "")
                {
                    searchValue = "%" + searchValue + "%";
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT  *
                                    FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY ProductName) AS RowNumber
                                        FROM    Products 
                                        WHERE   (@categoryId = 0 OR CategoryId = @categoryId)
                                            AND  (@supplierId = 0 OR SupplierId = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryId", CategoryID);
                cmd.Parameters.AddWithValue("@supplierId", SupplierID);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);


                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            Unit = Convert.ToString(dbReader["Unit"])
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
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductAttribute> ListAttributes(int productId)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection connection = GetConnection())
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM	ProductAttributes WHERE ProductID = @productID ORDER BY DisplayOrder desc";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@productID", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductAttribute()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeID = Convert.ToUInt32(dbReader["AttributeID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),

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
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductGallery> ListGalleries(int productId)
        {
            List<ProductGallery> data = new List<ProductGallery>();
            using (SqlConnection connection = GetConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM	ProductGallery WHERE ProductID = @productID 
                                    ORDER BY DisplayOrder desc";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@productID", productId);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductGallery()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            GalleryID = Convert.ToUInt32(dbReader["GalleryID"]),
                            isHidden = Convert.ToBoolean(dbReader["isHidden"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
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
        public bool Update(Product data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update Products
                                    set ProductName = @ProductName,
                                    SupplierID	=@SupplierID,
                                    CategoryID =@CategoryID,
                                    Unit = @Unit,
                                    Price = @Price,
                                    Photo = @Photo
                                    Where ProductID = @ProductID
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);

                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAttribute(ProductAttribute data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE ProductAttributes
                                    SET 
	                                    AttributeName  = @AttributeName,
	                                    AttributeValue = @AttributeValue,
	                                    DisplayOrder   = @DisplayOrder
	                                    WHERE AttributeID = @AttributeID";
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateGallery(ProductGallery data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE	ProductGallery
                                    SET		Description= @Description,
		                                    DisplayOrder= @DisplayOrder,
		                                    IsHidden = @IsHidden,
		                                    Photo = @Photo
                                    WHERE	GalleryID = @GalleryID";
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.isHidden);
                cmd.Parameters.AddWithValue("@GalleryID", data.GalleryID);
                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
    }
}
