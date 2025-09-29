using log4net;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace Infrastructure.Commons
{
    public class DatabaseSql
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //private readonly IConfiguration _configuration;

        //// Inject IConfiguration through constructor
        //public DatabaseSql(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public static async Task<SqlConnection> GetConnect()
        {
            // Retrieve the connection string from appsettings.json
            string connectionString = "";
            //string connectionString = _configuration.GetConnectionString("DefaultConnection");

            SqlConnection con = new SqlConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                //con.Open();
                await con.OpenAsync();
            }
            return con;
        }

        public static SqlConnection GetConnect2()
        {
            string connectionString = "";

            SqlConnection con = new SqlConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public static async Task<IList<T>> ExecuteProcToList<T>(string procName, List<SqlParameter> lstParam)
        {
            var dt = new DataTable();
            //serializer.MaxJsonLength = Int32.MaxValue;
            IList<T> objectsList = new List<T>();
            try
            {
                //Execute procedure and get table result
                dt = await ExecuteProcTable(procName, lstParam);

                if (dt.Rows.Count == 0) return objectsList;
                objectsList = DataTableToList<T>(dt);

                return objectsList;
            }
            catch (Exception ex)
            {
                log.Error("ERROR: " + ex.Message);
                return null;
            }
        }
        public static IList<T> ExecuteProcToList2<T>(string procName, List<SqlParameter> lstParam)
        {
            var dt = new DataTable();
            //serializer.MaxJsonLength = Int32.MaxValue;
            IList<T> objectsList = new List<T>();
            try
            {
                //Execute procedure and get table result
                dt = ExecuteProcTable2(procName, lstParam);

                if (dt.Rows.Count == 0) return objectsList;
                objectsList = DataTableToList<T>(dt);

                return objectsList;
            }
            catch (Exception ex)
            {
                log.Error("ERROR: " + ex.Message);
                return null;
            }
        }
        public static async Task<DataTable> ExecuteProcTable(string procName, List<SqlParameter> lstParam)
        {
            var con = await GetConnect();
            var dt = new DataTable();
            try
            {
                using (var cmd = new SqlCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 360;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            cmd.Parameters.Add(param);
                        }

                    //var dtAdapter = new SqlDataAdapter(cmd);
                    //dtAdapter.Fill(dt);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                log.Error("ERROR: " + ex.Message);
                return new DataTable();
            }
            finally
            {
                //con.Close();
                await con.CloseAsync(); // Đóng kết nối bất đồng bộ
                //con.Dispose();

            }
        }

        public static DataTable ExecuteProcTable2(string procName, List<SqlParameter> lstParam)
        {
            var con = GetConnect2();
            var dt = new DataTable();
            try
            {
                using (var cmd = new SqlCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 360;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            cmd.Parameters.Add(param);
                        }

                    var dtAdapter = new SqlDataAdapter(cmd);
                    dtAdapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                log.Error("ERROR: " + ex.Message);
                return new DataTable();
            }
            finally
            {
                con.Close();
                con.Dispose();

            }
        }

        public static IList<T> DataTableToList<T>(DataTable table)
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = Activator.CreateInstance<T>();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var value = row[prop.Name];
                            Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, safeValue, null);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
