using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BussinessLayers
{
    /// <summary>
    /// Các chức năng tác nghiệp liên quan đến quản lí dữ liệu
    /// </summary>
    public class DataService
    {
        private static ICountryDAL countryDAL;
        private static ICityDAL cityDAL;
        private static ISuppliersDAL supplierDAL;
        private static ICustomersDAL customerDAL;
        private static IEmployeesDAL employeeDAL;
        private static ICategoriesDAL categoryDAL;
        private static IShippers shipperDAL;


        public static void Init(DatabaseTypes dbType, String connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    countryDAL = new DataLayers.SQLServer.CountryDAL(connectionString);
                    cityDAL = new DataLayers.SQLServer.CityDAL(connectionString);
                    supplierDAL = new DataLayers.SQLServer.SupplierDAL(connectionString);
                    customerDAL = new DataLayers.SQLServer.CustomerDAL(connectionString);
                    employeeDAL = new DataLayers.SQLServer.EmployeeDAL(connectionString);
                    categoryDAL = new DataLayers.SQLServer.CategoryDAL(connectionString);
                    shipperDAL = new DataLayers.SQLServer.ShipperDAL(connectionString);

                    break;
                case DatabaseTypes.FakeDB:

                    break;
                default:
                    throw new Exception("Data not supported");
            }
        }
        //Custom Service

        /// <summary>
        ///  get list all country
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListCountries()
        {
            return countryDAL.List();
        }
        /// <summary>
        /// get list all city
        /// </summary>
        /// <returns></returns>
        public static List<City> ListCities()
        {
            return cityDAL.List();
        }
        /// <summary>
        /// get city BY country
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>List City By countryName</returns>
        public static List<City> ListCities(string countryName)
        {
            return cityDAL.List(countryName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        /// 
        
        //Supplier Service

        public static List<Supplier> ListSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 25;
            rowCount = supplierDAL.Count(searchValue);
            return supplierDAL.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// get supplier by ID
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public static Supplier getSupplier(int SupplierID)
        {
            return supplierDAL.GetById(SupplierID);
        }
        /// <summary>
        /// update supplier 
        /// </summary>
        /// <param name="data"> suppliers data</param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public static bool Update(Supplier data, int SupplierID)
        {
            return supplierDAL.Update(data, SupplierID);
        }

        public static int AddSup (Supplier data )
        {
            return supplierDAL.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public static bool Delete(int SupplierID)
        {
            return supplierDAL.Delete(SupplierID);
        }
        
        //Customer Service 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 25;
            rowCount = supplierDAL.Count(searchValue);
            return customerDAL.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerData"></param>
        /// <returns></returns>
        public static int addCustomer(Customer customerData)
        {
            return customerDAL.Add(customerData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Customer getCustomer(int CustomerID)
        {
            return customerDAL.GetById(CustomerID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static bool UpdateCus(Customer data, int CustomerID)
        {
            return customerDAL.Update(data, CustomerID);
        }

        public static bool DeleteCus(int CustomerID)
        {
            return customerDAL.Delete(CustomerID);
        }

        //Employee Service

        public static List<Employee> ListEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 25;
            rowCount = employeeDAL.Count(searchValue);
            return employeeDAL.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerData"></param>
        /// <returns></returns>
        public static int AddEmp(Employee empData)
        {
            return employeeDAL.Add(empData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Employee getEmployee(int EmployeeID)
        {
            return employeeDAL.GetById(EmployeeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static bool UpdateEmp(Employee data, int EmployeeID)
        {
            return employeeDAL.Update(data, EmployeeID);
        }

        public static bool DeleteEmp(int EmployeeID)
        {
            return customerDAL.Delete(EmployeeID);
        }

        // CategoryService
        public static List<Category> ListCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 25;
            rowCount = categoryDAL.Count(searchValue);
            return categoryDAL.List(page, pageSize, searchValue);
        }

        public static int AddCategory(Category categoryData)
        {
            return categoryDAL.Add(categoryData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public static Category getCategory(int CategoryId)
        {
            return categoryDAL.GetById(CategoryId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public static bool UpdateCag(Category data, int CategoryId)
        {
            return categoryDAL.Update(data, CategoryId);
        }

        public static bool DeleteCag(int CategoryId)
        {
            return categoryDAL.Delete(CategoryId);
        }

        public static List<Category> getAllCategories()
        {
            return categoryDAL.getAllCategories();
        }
        //ShipperService

        public static List<Shipper> ListShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 25;
            rowCount = shipperDAL.Count(searchValue);
            return shipperDAL.List(page, pageSize, searchValue);
        }

        public static int AddShipper(Shipper data)
        {
            return shipperDAL.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static Shipper getShipper(int ShipperID)
        {
            return shipperDAL.GetById(ShipperID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data, int ShipperID)
        {
            return shipperDAL.Update(data, ShipperID);
        }

        public static bool DeleteShipper(int ShipperID)
        {
            return shipperDAL.Delete(ShipperID);
        }
    } 
}
