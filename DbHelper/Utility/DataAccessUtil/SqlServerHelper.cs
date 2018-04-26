using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Utility.DataAccessUtil
{
    /// <inheritdoc />
    /// <summary>
    /// SQL Server数据访问工具类
    /// Defautl ConnectionString Name is SqlServerHelper
    /// </summary>
    public class SqlServerHelper : IDisposable
    {
        #region Fields
        public string ConnString { get; set; }
        private SqlConnection Connection { get; }
        private SqlCommand Command { get; }
        #endregion

        #region Constructor
        public SqlServerHelper()
        {
            ConnString = WebConfigurationManager.ConnectionStrings["SqlServerHelper"]
                .ToString();
            Connection = new SqlConnection(ConnString);
            Command = Connection.CreateCommand();
        }

        public SqlServerHelper(string connStringName)
        {
            ConnString = WebConfigurationManager.ConnectionStrings[connStringName]
                .ToString();
            Connection = new SqlConnection(ConnString);
            Command = Connection.CreateCommand();
        }
        #endregion

        #region Connection
        public virtual void OpenConnection()
        {
            Connection.ConnectionString = ConnString;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        public virtual void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
        }

        #endregion

        #region create an Command
        private SqlCommand PrepareCommand(CommandType cmdType, string cmdText, SqlParameter cmdParm)
        {
            Command.CommandText = cmdText;
            Command.CommandType = cmdType;
            Command.Parameters.Clear();
            if (cmdParm != null)
            {
                AddParameter(cmdParm);
            }
            return Command;
        }

        private SqlCommand PrepareCommand(CommandType cmdType, string cmdText, IList<SqlParameter> cmdParms)
        {
            Command.CommandText = cmdText;
            Command.CommandType = cmdType;
            Command.Parameters.Clear();
            if (cmdParms != null)
            {
                AddParameters(cmdParms);
            }
            return Command;
        }

        private SqlCommand PrepareCommand(CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            Command.CommandText = cmdText;
            Command.CommandType = cmdType;
            Command.Parameters.Clear();
            if (cmdParms != null)
            {
                AddParameters(cmdParms);
            }
            return Command;
        }

        private SqlCommand PrepareCommand(CommandType cmdType, string cmdText)
        {
            Command.CommandText = cmdText;
            Command.CommandType = cmdType;
            return Command;
        }
        private SqlCommand PrepareCommand(string cmdText)
        {
            Command.CommandText = cmdText;
            return Command;
        }
        #endregion

        #region add SqlParameter list
        public SqlParameter AddParameter(SqlParameter param)
        {
            Command.Parameters.Add(param);
            return param;
        }

        public SqlParameter AddParameter(string parameterName, SqlDbType type, object value)
        {
            return AddParameter(parameterName, type, value, ParameterDirection.Input);
        }

        public SqlParameter AddParameter(string parameterName, SqlDbType type, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = type,
                Value = value,
                Direction = direction
            };
            Command.Parameters.Add(param);
            return param;
        }

        public SqlParameter AddParameter(string parameterName, SqlDbType type, int size, object value)
        {
            return AddParameter(parameterName, type, size, value, ParameterDirection.Input);
        }

        public SqlParameter AddParameter(string parameterName, SqlDbType type, int size, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = type,
                Size = size,
                Value = value,
                Direction = direction
            };
            Command.Parameters.Add(param);
            return param;
        }

        public void AddRangeParameters(SqlParameter[] parameters)
        {
            Command.Parameters.AddRange(parameters);
        }

        public void AddParameters(IList<SqlParameter> parameters)
        {
            if (parameters.Count > 0)
            {
                Command.Parameters.AddRange(parameters.ToArray());
            }
        }

        public void ClearParameter()
        {
            if (Command != null && Command.Parameters.Count > 0)
            {
                Command.Parameters.Clear();
            }
        }
        #endregion

        #region ExecuteScalar
        public object ExecuteScalar(string cmdText)
        {
            try
            {
                PrepareCommand(cmdText);
                OpenConnection();
                var result = Command.ExecuteScalar();
                CloseConnection();
                return result;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            try
            {
                PrepareCommand(cmdType, cmdText);
                OpenConnection();
                var result = Command.ExecuteScalar();
                CloseConnection();
                return result;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, SqlParameter cmdParam)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParam);
                OpenConnection();
                var result = Command.ExecuteScalar();
                CloseConnection();
                return result;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, IList<SqlParameter> cmdParams)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParams);
                OpenConnection();
                var result = Command.ExecuteScalar();
                CloseConnection();
                return result;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region ExcuteReader
        public SqlDataReader ExecuteReader(string cmdText)
        {
            try
            {
                PrepareCommand(cmdText);
                OpenConnection();
                SqlDataReader oradr = Command.ExecuteReader();
                CloseConnection();
                return oradr;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {
            try
            {
                PrepareCommand(cmdType, cmdText);
                OpenConnection();
                SqlDataReader oradr = Command.ExecuteReader();
                CloseConnection();
                return oradr;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, SqlParameter cmdParm)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParm);
                OpenConnection();
                SqlDataReader oradr = Command.ExecuteReader();
                CloseConnection();
                return oradr;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, IList<SqlParameter> cmdParms)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParms);
                OpenConnection();
                SqlDataReader oradr = Command.ExecuteReader();
                CloseConnection();
                return oradr;
            }
            catch (SqlException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(string cmdText)
        {
            try
            {
                PrepareCommand(cmdText);
                OpenConnection();
                int num = Command.ExecuteNonQuery();
                CloseConnection();
                return num;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }

        }
        public int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            try
            {
                PrepareCommand(cmdType, cmdText);
                OpenConnection();
                int num = Command.ExecuteNonQuery();
                CloseConnection();
                return num;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, SqlParameter cmdParam)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParam);
                OpenConnection();
                int num = Command.ExecuteNonQuery();
                CloseConnection();
                return num;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, IList<SqlParameter> cmdParms)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParms);
                OpenConnection();
                int num = Command.ExecuteNonQuery();
                CloseConnection();
                return num;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParms);
                OpenConnection();
                int num = Command.ExecuteNonQuery();
                CloseConnection();
                return num;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region DataSet
        public DataSet GetDataSet(string cmdText)
        {
            try
            {
                DataSet ds = new DataSet();
                PrepareCommand(cmdText);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                adpter.Fill(ds);
                CloseConnection();
                return ds;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetDataSet(CommandType cmdType, string cmdText, SqlParameter cmdParam)
        {
            try
            {
                DataSet ds = new DataSet();
                PrepareCommand(cmdType, cmdText, cmdParam);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                adpter.Fill(ds);
                CloseConnection();
                return ds;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetDataSet(CommandType cmdType, string cmdText)
        {
            try
            {
                DataSet ds = new DataSet();
                PrepareCommand(cmdType, cmdText);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                adpter.Fill(ds);
                CloseConnection();
                return ds;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetDataSet(CommandType cmdType, string cmdText, IList<SqlParameter> cmdParms)
        {
            try
            {
                DataSet ds = new DataSet();
                PrepareCommand(cmdType, cmdText, cmdParms);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                adpter.Fill(ds);
                CloseConnection();
                return ds;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region return an SqlDataAdapter
        public SqlDataAdapter GetDataAdapter(string cmdText)
        {
            try
            {
                PrepareCommand(cmdText);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                //CloseConnection();
                return adpter;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataAdapter GetDataAdapter(CommandType cmdType, string cmdText)
        {
            try
            {
                PrepareCommand(cmdType, cmdText);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                //CloseConnection();
                return adpter;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataAdapter GetDataAdapter(CommandType cmdType, string cmdText, SqlParameter cmdParam)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParam);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                //CloseConnection();
                return adpter;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataAdapter GetDataAdapter(CommandType cmdType, string cmdText, IList<SqlParameter> cmdParms)
        {
            try
            {
                PrepareCommand(cmdType, cmdText, cmdParms);
                OpenConnection();
                SqlDataAdapter adpter = new SqlDataAdapter { SelectCommand = Command };
                //CloseConnection();
                return adpter;
            }
            catch (SqlException dbEx)
            {

                throw new Exception(dbEx.Message);
            }
            finally
            {
                CloseConnection();
            }

        }
        #endregion
    }
}
