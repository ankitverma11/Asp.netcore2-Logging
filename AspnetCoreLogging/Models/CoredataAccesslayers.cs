using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public class CoredataAccesslayers
    {
        string connectionstring = "server=216.12.92.186; UID=saGenacisLocal; Password=heights@123;Database=GenacisLive;pooling=true;Max Pool Size=100";

        public DataSet GetLeakageSystemView(string strGraphViewType, DateTime dtStartDate, DateTime dtEndDate, int intUserId, int intDivisionIdToFilter, int intRegionIdToFilter, int intCustomerIdToFilter)
        {
            try
            {
                DataSet dsLeakageSystemView = null;
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlParameter[] oSqlParmaters = new SqlParameter[7];
                    oSqlParmaters[0] = CreateParameter(DbType.String, ParameterDirection.Input, "@graphViewType", strGraphViewType);
                    oSqlParmaters[1] = CreateParameter(DbType.DateTime, ParameterDirection.Input, "@startDate", dtStartDate);
                    oSqlParmaters[2] = CreateParameter(DbType.DateTime, ParameterDirection.Input, "@endDate", dtEndDate);
                    oSqlParmaters[3] = CreateParameter(DbType.Int32, ParameterDirection.Input, "@userId", intUserId);
                    oSqlParmaters[4] = CreateParameter(DbType.Int32, ParameterDirection.Input, "@divisionIdToFilter", intDivisionIdToFilter);
                    oSqlParmaters[5] = CreateParameter(DbType.Int32, ParameterDirection.Input, "@regionIdToFilter", intRegionIdToFilter);
                    oSqlParmaters[6] = CreateParameter(DbType.Int32, ParameterDirection.Input, "@customerIdToFilter", intCustomerIdToFilter);
                    ExecuteDataset(con, CommandType.StoredProcedure, "GetLeakageSystemView", "LeakageView", ref dsLeakageSystemView, oSqlParmaters);
                }
                //Return the dataset
                return dsLeakageSystemView;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
               
            }

        }

        public static SqlParameter CreateParameter(DbType dbType,
                                               ParameterDirection parameterDirection,
                                               string ParameterName,
                                               object Value)
        {
            SqlParameter sqlParam = new SqlParameter();
            sqlParam.DbType = dbType;
            sqlParam.Direction = parameterDirection;
            sqlParam.ParameterName = ParameterName;
            sqlParam.Value = Value;
            return sqlParam;
        }

        public static void ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, string SourceTableName, ref DataSet oDS, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                oDS = null;
                oDS = new DataSet();

                da.TableMappings.Add("Table", SourceTableName);


                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(oDS);


                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

            }
        }
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();

            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;
            command.CommandTimeout = 50000;
            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
    }
}