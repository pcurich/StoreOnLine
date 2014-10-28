using System;
using System.Data;
using System.Data.SqlClient;

namespace StoreOnLine.Service.Business
{

    /// <summary>
    /// Al estar usando MVC y consultas la base de datos por medio de linq pero aveces se prefiere los metodos tradicionales para 
    /// optener metodos trdicionales
    /// Esta clase tiene algunas funciones que pueden ayudar. Se puede llamar cualquien SQL Store Procedure con parametros
    /// de entrada o salida
    /// </summary>
    public class Sql
    {
        public static string Conn()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["CoreDBContext"].ConnectionString;
        }

        public static string Conn(string connectionstraing)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionstraing].ConnectionString;
        }

        public static void ExecuteNonQueryNoReturn(string sql, CommandType commandType, params object[] pars)
        {
            var con = new SqlConnection(Conn());
            con.Open();
            var com = new SqlCommand(sql, con) {CommandType = commandType};
            
            for (var i = 0; i < pars.Length; i += 2)
            {
                var par = new SqlParameter(pars[i].ToString(), pars[i + 1].ToString());
                com.Parameters.Add(par);
            }
            
            try
            {
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                throw new ArgumentException(ex.Message);
            }
        }
        
        public static string ExecuteNonQueryAndReturn(string sql, CommandType commandType, params object[] pars)
        {
            var con = new SqlConnection(Conn());
            con.Open();
            var com = new SqlCommand(sql, con) {CommandType = commandType};
            
            for (var i = 0; i < pars.Length - 1; i += 2)
            {
                var par = new SqlParameter(pars[i].ToString(), pars[i + 1].ToString());
                com.Parameters.Add(par);
            }
            
            com.Parameters.Add(pars[pars.Length - 1].ToString(), SqlDbType.NVarChar, 100);
            com.Parameters[pars[pars.Length - 1].ToString()].Direction = ParameterDirection.Output;
            
            try
            {
                com.ExecuteNonQuery();
                con.Close();
                return com.Parameters[pars[pars.Length - 1].ToString()].Value.ToString();
            }
            catch (SqlException e)
            {
                con.Close();
                return e.Message;
            }
        }

        public static DataTable GetTable(string sql)
        {
            var con = new SqlConnection {ConnectionString = Conn()};
            con.Open();
            var sqlAd = new SqlDataAdapter(sql, con);
            con.Close();
            var dt = new DataTable();
            dt.Clear();
            sqlAd.Fill(dt);
            return dt;
        }

        public static DataTable GetTable(string sql, string connectinstring)
        {
            var con = new SqlConnection {ConnectionString = Conn(connectinstring)};
            con.Open();
            var sqlAd = new SqlDataAdapter(sql, con);
            con.Close();
            var dt = new DataTable();
            dt.Clear();
            sqlAd.Fill(dt);
            return dt;
        }

        public static void ExecuteNonQuery(string commandText, string connectionString)
        {
            var con = new SqlConnection();
            var sqlCmd = new SqlCommand();
            con.ConnectionString = Conn(connectionString);
            con.Open();
            sqlCmd.CommandText = commandText;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = con;
            sqlCmd.ExecuteNonQuery();
            con.Close();
        }

        public static void ExecuteNonQuery(string commandText, string connectionString, params object[] pars)
        {
            var con = new SqlConnection();
            var sqlCmd = new SqlCommand();
            con.ConnectionString = Conn(connectionString);
            con.Open();
            sqlCmd.CommandText = commandText;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = con;
           
            for (var i = 0; i < pars.Length; i++)
            {
                var par = new SqlParameter(pars[i].ToString(), pars[i + 1].ToString());
                sqlCmd.Parameters.Add(par);
            }
            
            try
            {
                sqlCmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
