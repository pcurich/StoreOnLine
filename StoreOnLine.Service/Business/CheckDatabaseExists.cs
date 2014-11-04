using System.Data.SqlClient;

namespace StoreOnLine.Service.Business
{
    /// <summary>
    /// Revisa si una base de datos existe
    /// todo me parece que esto tmb lo hace entity pero aca no uso entity
    /// </summary>
    public class CheckDatabaseExists
    {
        public static bool IsDefaultDatabaseExists()
        {
            var connectionString = Sql.Conn();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool CheckDatabase(string databaseName)
        {
            bool result;
            try
            {
                var tmpConn = new SqlConnection(Sql.Conn());
                var sqlCreateDbQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);
                using (tmpConn)
                {
                    using (var sqlCmd = new SqlCommand(sqlCreateDbQuery, tmpConn))
                    {
                        tmpConn.Open();
                        var databaseId = (int)sqlCmd.ExecuteScalar();
                        tmpConn.Close();
                        result = (databaseId > 0);
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
