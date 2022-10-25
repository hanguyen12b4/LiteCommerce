using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// 
        /// </summary>
        protected String _connectionString;

        public _BaseDAL(String connectionString)
        {
            this._connectionString = connectionString;
        }

        protected SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this._connectionString;
            connection.Open();
            return connection;
        }
    }
}
