using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace DAAB
{
    public class DAABOracleFactory : DAABAbstracFactory  
    {
        private OracleConnection m_conecSQL;
        private OracleTransaction m_TranSQL;

        private void AttachParameters(OracleCommand command, OracleParameter[] commandParameters)
        {
            if ((command == null))
            {
                throw new ArgumentNullException("command");
            }
            if ((commandParameters != null))
            {
                ;
                foreach (OracleParameter p in commandParameters)
                {
                    if ((p != null))
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private void AssignParameterValues(OracleParameter[] commandParameters, DataRow dataRow)
        {
            if (commandParameters == null || dataRow == null)
            {
                return;
            }

            int i = 0;
            foreach (OracleParameter commandParameter in commandParameters)
            {
                if ((commandParameter.ParameterName == null || commandParameter.ParameterName.Length <= 1))
                {
                    throw new Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: ' {1}' .", i, commandParameter.ParameterName));
                }
                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName) != -1)
                {
                    commandParameter.Value = dataRow[commandParameter.ParameterName];
                }
                i++;
            }
        }

        private void AssignParameterValues(OracleParameter[] commandParameters, object[] parameterValues)
        {
            int j;
            if ((commandParameters == null) && (parameterValues == null))
            {
                return;
            }
            if (commandParameters != null && commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            j = commandParameters.Length - 1;
            for (int i = 0; i <= j; i++)
            {
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = ((IDbDataParameter)(parameterValues[i]));
                    if ((paramInstance.Value == null))
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if ((parameterValues[i] == null))
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        private void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, ref bool mustCloseConnection)
        {
            if ((command == null))
            {
                throw new ArgumentNullException("command");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                mustCloseConnection = true;
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null)
                {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private OracleParameter[] DevuelveParametros(CommandType commandType, ref ArrayList parametros)
        {
            if (parametros != null && !(parametros == null && parametros.Count > 0))
            {
                OracleParameter[] aParam = new OracleParameter[parametros.Count];

                for (int i = 0; i <= parametros.Count - 1; i++)
                {
                    if (parametros[i] is OracleParameter)
                    {
                        aParam[i] = (OracleParameter)parametros[i];
                    }
                    else
                    {
                        aParam[i] = CreaParametro(((IDbDataParameter)(parametros[i])));
                    }
                    OracleParameter ParAux = CreaParametro(((IDbDataParameter)(parametros[i]))); ;
                    if (ParAux.Direction != ParameterDirection.Input)
                    {
                        parametros[i] = aParam[i];
                    }
                }
                return aParam;
            }
            else
            {
                return null;
            }
        }

        private OracleParameter CreaParametro(IDbDataParameter parametro)
        {
            OracleParameter oParam = new OracleParameter();
            oParam.Direction = parametro.Direction;
            oParam.ParameterName = parametro.ParameterName;
            if (parametro.Value == null)
            {
                oParam.Value = DBNull.Value;
            }
            else
            {
                oParam.Value = parametro.Value;
            }
            oParam.SourceColumn = parametro.SourceColumn;
            oParam.SourceVersion = parametro.SourceVersion;
            if (parametro.DbType == DbType.Currency)
            {
                oParam.OracleType = OracleType.Number;
            }
            else if (parametro.DbType == DbType.Double || parametro.DbType == DbType.Single)
            {
                oParam.OracleType = OracleType.Double;
            }
            else if (parametro.DbType == DbType.Decimal || parametro.DbType == DbType.VarNumeric)
            {
                oParam.OracleType = OracleType.Number;
                if (parametro.Size > 0)
                {
                    oParam.Size = parametro.Size;
                }
                oParam.Scale = parametro.Scale;
                oParam.Precision = parametro.Precision;
            }
            else if (parametro.DbType == DbType.Byte)
            {
                oParam.OracleType = OracleType.Byte;
            }
            else if (parametro.DbType == DbType.AnsiString)
            {
                oParam.OracleType = OracleType.VarChar;
                if (parametro.Size > 0)
                {
                    oParam.Size = parametro.Size;
                }
                else
                {
                    oParam.Size = 50;
                }
            }
            else if (parametro.DbType == DbType.AnsiStringFixedLength)
            {
                oParam.OracleType = OracleType.Char;
                if (parametro.Size > 0)
                {
                    oParam.Size = parametro.Size;
                }
                else
                {
                    oParam.Size = 50;
                }
            }
            else if (parametro.DbType == DbType.Binary)
            {
                oParam.OracleType = OracleType.Blob;
            }
            else if (parametro.DbType == DbType.Boolean)
            {
                oParam.OracleType = OracleType.Byte;
            }
            else if (parametro.DbType == DbType.Date)
            {
                oParam.OracleType = OracleType.DateTime;
            }
            else if (parametro.DbType == DbType.DateTime)
            {
                oParam.OracleType = OracleType.DateTime;
            }
            else if (parametro.DbType == DbType.Guid)
            {
                oParam.OracleType = OracleType.RowId;
            }
            else if (parametro.DbType == DbType.SByte || parametro.DbType == DbType.Int16 || parametro.DbType == DbType.UInt16)
            {
                oParam.OracleType = OracleType.Int16;
            }
            else if (parametro.DbType == DbType.Int32 || parametro.DbType == DbType.UInt32)
            {
                oParam.OracleType = OracleType.Int32;
            }
            else if (parametro.DbType == DbType.Int64 || parametro.DbType == DbType.UInt64)
            {
                oParam.OracleType = OracleType.Int32;
            }
            else if (parametro.DbType == DbType.Object)
            {
                oParam.OracleType = OracleType.Cursor;
            }
            else if (parametro.DbType == DbType.String)
            {
                oParam.OracleType = OracleType.VarChar;
                if (parametro.Size > 0)
                {
                    oParam.Size = parametro.Size;
                }
                else
                {
                    oParam.Size = 50;
                }
            }
            else if (parametro.DbType == DbType.StringFixedLength)
            {
                oParam.OracleType = OracleType.Char;
                if (parametro.Size > 0)
                {
                    oParam.Size = parametro.Size;
                }
                else
                {
                    oParam.Size = 50;
                }
            }
            else if (parametro.DbType == DbType.Time)
            {
                oParam.OracleType = OracleType.DateTime;
            }
            return oParam;
        }

        private bool estableceConexion(bool pb_transaccional, string pc_cadConexion)
        {
            if (m_conecSQL == null || m_conecSQL.State != ConnectionState.Open)
            {
                m_conecSQL = new OracleConnection(pc_cadConexion);
                m_conecSQL.Open();
            }
            if (pb_transaccional && m_TranSQL == null)
            {
                m_TranSQL = m_conecSQL.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            return true;
        }

        public override DataSet ExecuteDataset(ref DAABRequest request)
        {
            string connectionString = request.ConnectionString;
            if ((connectionString == null || connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((request.Command == null || request.Command.Length == 0))
            {
                throw new ArgumentNullException("No ha ingresado el commando a ejecutar.");
            }
            try
            {
                ArrayList lista = request.Parameters;
                estableceConexion(request.Transactional, connectionString);
                OracleParameter[] aparam = DevuelveParametros(request.CommandType, ref lista);
                if (request.Transactional)
                {
                    return ExecuteDatasets(m_TranSQL, request.CommandType, request.Command, aparam, request.TableNames);
                }
                else
                {
                    return ExecuteDatasets(m_conecSQL, request.CommandType, request.Command, aparam, request.TableNames);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (!(request.Transactional) & !(m_conecSQL == null))
                {
                    m_conecSQL.Dispose();
                }
            }
        }

        private DataSet ExecuteDatasets(OracleConnection connection, CommandType commandType, string commandText, OracleParameter[] commandParameters, string[] tableNames)
        {
            if ((connection == null))
            {
                throw new ArgumentNullException("connection");
            }
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            OracleDataAdapter dataAdatpter = new OracleDataAdapter();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, ref mustCloseConnection);
            try
            {
                dataAdatpter = new OracleDataAdapter(cmd);
                if (tableNames == null)
                {
                    dataAdatpter.Fill(ds);
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        ds.Tables[0].TableName = "Tabla" + i.ToString();
                        ds.AcceptChanges();
                    }
                }
                else if (!(tableNames == null && tableNames.Length > 0))
                {
                    dataAdatpter.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        //string tableName = "Table"; 					
                        for (int index = 0; index < tableNames.Length; index++)
                        {
                            if ((tableNames[index] == null || tableNames[index].Length == 0))
                            {
                                throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                            }
                            //dataAdatpter.TableMappings.Add(tableName, tableNames[index]); 
                            //tableName = tableName + (index + 1).ToString(); 
                            ds.Tables[index].TableName = tableNames[index];
                        }
                    }
                }
                cmd.Parameters.Clear();
            }
            finally
            {
                if (dataAdatpter != null)
                {
                    dataAdatpter.Dispose();
                }
            }
            if ((mustCloseConnection))
            {
                connection.Close();
            }
            return ds;
        }

        private DataSet ExecuteDatasets(OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, string[] tableNames)
        {
            if ((transaction == null))
            {
                throw new ArgumentNullException("transaction");
            }
            if (!((transaction == null)) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            OracleDataAdapter dataAdatpter = new OracleDataAdapter();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
            try
            {
                dataAdatpter = new OracleDataAdapter(cmd);
                if (!(tableNames == null && tableNames.Length > 0))
                {
                    string tableName = "Table";

                    for (int index = 0; index <= tableNames.Length - 1; index++)
                    {
                        if ((tableNames[index] == null || tableNames[index].Length == 0))
                        {
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        }
                        dataAdatpter.TableMappings.Add(tableName, tableNames[index]);
                        tableName = tableName + (index + 1).ToString();
                    }
                }
                dataAdatpter.Fill(ds);
                cmd.Parameters.Clear();
            }
            finally
            {
                if ((!(dataAdatpter == null)))
                {
                    dataAdatpter.Dispose();
                }
            }
            return ds;
        }

        public override int ExecuteNonQuery(ref DAABRequest request)
        {
            string connectionString = request.ConnectionString;
            if ((connectionString == null || connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((request.Command == null || request.Command.Length == 0))
            {
                throw new ArgumentNullException("No ha ingresado el commando a ejecutar.");
            }
            try
            {
                ArrayList lista = request.Parameters;
                OracleParameter[] aparam = DevuelveParametros(request.CommandType, ref lista);
                estableceConexion(request.Transactional, connectionString);
                int iretval;
                if (request.Transactional)
                {
                    iretval = ExecuteNonQuerys(m_TranSQL, request.CommandType, request.Command, aparam);
                }
                else
                {
                    iretval = ExecuteNonQuerys(m_conecSQL, request.CommandType, request.Command, aparam);
                }
                return iretval;
            }
            finally
            {
                if (!(request.Transactional) & !(m_conecSQL == null))
                {
                    m_conecSQL.Dispose();
                }
            }
        }

        private int ExecuteNonQuerys(OracleConnection connection, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            if ((connection == null))
            {
                throw new ArgumentNullException("connection");
            }
            OracleCommand cmd = new OracleCommand();
            int retval;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, ((OracleTransaction)(null)), commandType, commandText, commandParameters, ref mustCloseConnection);
            retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if ((mustCloseConnection))
            {
                connection.Close();
            }
            return retval;
        }

        private int ExecuteNonQuerys(OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            if ((transaction == null))
            {
                throw new ArgumentNullException("transaction");
            }
            if (!((transaction == null)) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            OracleCommand cmd = new OracleCommand();
            int retval;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
            retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return retval;
        }

        public override object ExecuteScalar(ref DAABRequest Request)
        {
            string connectionString = Request.ConnectionString;
            if ((connectionString == null || connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((Request.Command == null || Request.Command.Length == 0))
            {
                throw new ArgumentNullException("No ha ingresado el commando a ejecutar.");
            }
            try
            {
                ArrayList lista = Request.Parameters;
                estableceConexion(Request.Transactional, connectionString);
                OracleParameter[] aparam = DevuelveParametros(Request.CommandType, ref lista);
                if (Request.Transactional)
                {
                    return ExecuteScalares(m_TranSQL, Request.CommandType, Request.Command, aparam);
                }
                else
                {
                    return ExecuteScalares(m_conecSQL, Request.CommandType, Request.Command, aparam);
                }
            }
            finally
            {
                if (!(Request.Transactional) & !(m_conecSQL == null))
                {
                    m_conecSQL.Dispose();
                }
            }
        }

        private object ExecuteScalares(OracleConnection connection, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            if ((connection == null))
            {
                throw new ArgumentNullException("connection");
            }
            OracleCommand cmd = new OracleCommand();
            object retval;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, ((OracleTransaction)(null)), commandType, commandText, commandParameters, ref mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if ((mustCloseConnection))
            {
                connection.Close();
            }
            return retval;
        }

        private object ExecuteScalares(OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            if ((transaction == null))
            {
                throw new ArgumentNullException("transaction");
            }
            if (!((transaction == null)) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            OracleCommand cmd = new OracleCommand();
            object retval;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        public override DAABDataReader ExecuteReader(ref DAABRequest Request)
        {
            DAABOracleDataReader oDataReaderSQL;
            string connectionString = Request.ConnectionString;
            if ((connectionString == null || connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            try
            {
                ArrayList lista = Request.Parameters;
                OracleParameter[] aparam = DevuelveParametros(Request.CommandType, ref lista);
                estableceConexion(false, connectionString);
                OracleDataReader drSQL;
                drSQL = ExecuteReaders(m_conecSQL, ((OracleTransaction)(null)), Request.CommandType, Request.Command, aparam);
                oDataReaderSQL = new DAABOracleDataReader();
                oDataReaderSQL.ReturnDataReader = drSQL;
                return oDataReaderSQL;
            }
            catch (OracleException ex1)
            {
                if (!(m_conecSQL == null))
                {
                    m_conecSQL.Dispose();
                }
                Request.Exception = ex1;
                throw ex1;
                //return null; 
            }
            catch (Exception ex1)
            {
                Request.Exception = ex1;
                if (!(m_conecSQL == null))
                {
                    m_conecSQL.Dispose();
                }
                throw;
                //return null; 
            }
        }

        private OracleDataReader ExecuteReaders(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            if ((connection == null))
            {
                throw new ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            OracleCommand cmd = new OracleCommand();
            try
            {
                OracleDataReader dataReader;
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                bool canClear = true;
                foreach (OracleParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                    }
                }
                if ((canClear))
                {
                    cmd.Parameters.Clear();
                }
                return dataReader;
            }
            catch (Exception ex1)
            {
                if ((mustCloseConnection))
                {
                    connection.Close();
                }
                throw ex1;
            }
        }

        public override void FillDataset(ref DAABRequest request)
        {
            string connectionString = request.ConnectionString;
            if ((connectionString == null || connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((request.Command == null || request.Command.Length == 0))
            {
                throw new ArgumentNullException("No ha ingresado el commando a ejecutar.");
            }
            if ((request.RequestDataSet == null))
            {
                throw new ArgumentNullException("RequestDataSet");
            }
            try
            {
                ArrayList lista = request.Parameters;
                estableceConexion(request.Transactional, connectionString);
                OracleParameter[] aparam = DevuelveParametros(request.CommandType, ref lista);
                if (request.Transactional)
                {
                    FillDatasets(m_conecSQL, m_TranSQL, request.CommandType, request.Command, request.RequestDataSet, request.TableNames, aparam);
                }
                else
                {
                    FillDatasets(m_conecSQL, ((OracleTransaction)(null)), request.CommandType, request.Command, request.RequestDataSet, request.TableNames, aparam);
                }
            }
            catch (Exception ex1)
            {
                request.Exception = ex1;
                throw ex1;
            }
            finally
            {
                if (!(request.Transactional) & !(m_conecSQL == null))
                {
                    m_conecSQL.Dispose();
                }
            }
        }

        private void FillDatasets(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, OracleParameter[] commandParameters)
        {
            if ((connection == null))
            {
                throw new ArgumentNullException("connection");
            }
            if ((dataSet == null))
            {
                throw new ArgumentNullException("dataSet");
            }
            OracleCommand command = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
            OracleDataAdapter dataAdapter = new OracleDataAdapter(command);
            try
            {
                if (!(tableNames == null && tableNames.Length > 0))
                {
                    string tableName = "Table";
                    int index = 0;
                    for (index = 0; index <= tableNames.Length - 1; index++)
                    {
                        if ((tableNames[index] == null || tableNames[index].Length == 0))
                        {
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        }
                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        tableName = tableName + (index + 1).ToString();
                    }
                }
                dataAdapter.Fill(dataSet);
                command.Parameters.Clear();
            }
            finally
            {
                if ((!(dataAdapter == null)))
                {
                    dataAdapter.Dispose();
                }
            }
            if ((mustCloseConnection))
            {
                connection.Close();
            }
        }

        public override void UpdateDataSet(ref DAABRequest RequestInsert, ref DAABRequest RequestUpdate, ref DAABRequest RequestDelete)
        {
            string connectionString = RequestInsert.ConnectionString;
            OracleCommand cmdCommandInsert;
            OracleCommand cmdCommandUpdate;
            OracleCommand cmdCommandDelete;
            if ((connectionString == null || connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((RequestInsert.Command == null || RequestInsert.Command.Length == 0))
            {
                throw new ArgumentNullException("No ha ingresado el commando a ejecutar.RequestInsert");
            }
            if ((RequestUpdate.Command == null || RequestUpdate.Command.Length == 0))
            {
                throw new ArgumentNullException("No ha ingresado el commando a ejecutar.RequestUpdate");
            }
            if ((RequestDelete.Command == null || RequestDelete.Command.Length == 0))
            {
                throw new ArgumentNullException("No ha ingresado el commando a ejecutar.RequestDelete");
            }
            if ((RequestInsert.RequestDataSet == null))
            {
                throw new ArgumentNullException("RequestDataSet:RequestInsert");
            }
            if (RequestInsert.TableNames == null)
            {
                throw new ArgumentNullException("Falta especificar el nombre de la tabla a actualizar");
            }
            try
            {
                bool cerrarCn = false;
                ArrayList lista = RequestInsert.Parameters;
                estableceConexion(RequestInsert.Transactional, connectionString);
                cmdCommandInsert = new OracleCommand();
                OracleParameter[] aparamInsert = DevuelveParametros(RequestInsert.CommandType, ref lista);
                PrepareCommand(cmdCommandInsert, m_conecSQL, m_TranSQL, RequestInsert.CommandType, RequestInsert.Command, aparamInsert, ref cerrarCn);
                cmdCommandUpdate = new OracleCommand();
                OracleParameter[] aparamUpdate = DevuelveParametros(RequestUpdate.CommandType, ref lista);
                PrepareCommand(cmdCommandUpdate, m_conecSQL, m_TranSQL, RequestUpdate.CommandType, RequestUpdate.Command, aparamUpdate, ref cerrarCn);
                cmdCommandDelete = new OracleCommand();
                OracleParameter[] aparamDelete = DevuelveParametros(RequestDelete.CommandType, ref lista);
                PrepareCommand(cmdCommandDelete, m_conecSQL, m_TranSQL, RequestDelete.CommandType, RequestDelete.Command, aparamDelete, ref cerrarCn);
                UpdateDatasets(cmdCommandInsert, cmdCommandDelete, cmdCommandUpdate, RequestInsert.RequestDataSet, RequestInsert.TableNames[0]);
            }
            catch (Exception ex1)
            {
                RequestInsert.Exception = ex1;
                RequestDelete.Exception = ex1;
                RequestUpdate.Exception = ex1;
                throw ex1;
            }
            finally
            {
                if (!(RequestInsert.Transactional) & !(m_conecSQL == null))
                {
                    m_conecSQL.Dispose();
                }
            }
        }

        public void UpdateDatasets(OracleCommand insertCommand, OracleCommand deleteCommand, OracleCommand updateCommand, DataSet dataSet, string tableName)
        {
            if ((insertCommand == null))
            {
                throw new ArgumentNullException("insertCommand");
            }
            if ((deleteCommand == null))
            {
                throw new ArgumentNullException("deleteCommand");
            }
            if ((updateCommand == null))
            {
                throw new ArgumentNullException("updateCommand");
            }
            if ((dataSet == null))
            {
                throw new ArgumentNullException("dataSet");
            }
            if ((tableName == null || tableName.Length == 0))
            {
                throw new ArgumentNullException("tableName");
            }
            OracleDataAdapter dataAdapter = new OracleDataAdapter();
            try
            {
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataSet, tableName);
                dataSet.AcceptChanges();
            }
            finally
            {
                if ((!(dataAdapter == null)))
                {
                    dataAdapter.Dispose();
                }
            }
        }

        protected void Finalize()
        {
            if (!(m_conecSQL == null))
            {
                if (!((m_conecSQL.State == ConnectionState.Closed)) | !((m_conecSQL.State == ConnectionState.Broken)))
                {
                    m_conecSQL.Close();
                }
            }
            //base.Finalize(); 
        }

        public override void CommitTransaction()
        {
            if (!(m_conecSQL == null && m_conecSQL.State == ConnectionState.Open && !(m_TranSQL == null)))
            {
                m_TranSQL.Commit();
                m_TranSQL = null;
                Dispose();
            }
        }

        public override void RollBackTransaction()
        {
            if (!(m_conecSQL == null && m_conecSQL.State == ConnectionState.Open && !(m_TranSQL == null)))
            {
                m_TranSQL.Rollback();
                m_TranSQL = null;
                Dispose();
            }
        }

        public override void Dispose()
        {
            if (!(m_conecSQL == null && (!((m_conecSQL.State == ConnectionState.Closed)) | !((m_conecSQL.State == ConnectionState.Broken)))))
            {
                if (m_conecSQL.State == ConnectionState.Open && !(m_TranSQL == null))
                {
                    m_TranSQL.Commit();
                    m_TranSQL = null;
                }
                m_conecSQL.Dispose();
            }
        }
    }
}
