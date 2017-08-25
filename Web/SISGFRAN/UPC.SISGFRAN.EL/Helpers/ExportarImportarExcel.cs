using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Helpers
{
    public class ExportarImportarExcel
    {
        private static OleDbConnection DBXlsConnect(string pathBDXls)
        {
            string objRetVal = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                "Data Source=" + pathBDXls + ";" +
                                "Extended Properties=" + Convert.ToChar(34).ToString() +
                                "Excel 8.0;Imex=2;HDR=Yes;" + Convert.ToChar(34).ToString();
            return new OleDbConnection(objRetVal);
        }

        private static OleDbConnection DBXls2007Connect(string pathBDXls)
        {
            string objRetVal = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                "Data Source=" + pathBDXls + ";" +
                                "Extended Properties=" + Convert.ToChar(34).ToString() +
                                "Excel 12.0 Xml;HDR=Yes;" + Convert.ToChar(34).ToString();
            return new OleDbConnection(objRetVal);
        }

        private static Boolean DBXlsExistDelete(string pathBDXls, Boolean delete)
        {
            Boolean objRetVal = false;
            try
            {
                if (File.Exists(pathBDXls))
                {
                    if (delete) { File.Delete(pathBDXls); }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objRetVal;
        }

        private static Boolean DBXlsCreateTable(OleDbConnection dbCnnt, string nameTb, string namesCm)
        {
            Boolean objRetVal = false;
            try
            {
                if (DBXlsExistTable(dbCnnt, nameTb))
                {
                    return true;
                }
                string sCmd = "CREATE TABLE [" + nameTb + "]" + namesCm;
                OleDbCommand dbCmd = new OleDbCommand(sCmd, dbCnnt);
                dbCnnt.Open();
                dbCmd.ExecuteNonQuery();
                dbCnnt.Close();
                objRetVal = true;
            }
            catch (Exception ex)
            {
                if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
                throw ex;
            }
            finally
            {
                if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
            }
            return objRetVal;
        }

        private static Boolean DBXlsExistTable(OleDbConnection dbCnnt, string nameTb)
        {
            DataTable dtbTabla = null;
            Boolean objRetVal = false;
            try
            {
                dbCnnt.Open();
                dtbTabla = dbCnnt.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                dbCnnt.Close();
                foreach (DataRow dr in dtbTabla.Rows)
                {
                    if (dr["TABLE_NAME"].ToString() == nameTb.Replace(".", "#"))
                    {
                        objRetVal = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
                throw ex;
            }
            finally
            {
                if (dtbTabla != null)
                {
                    dtbTabla.Dispose();
                    dtbTabla = null;
                }
            }
            return objRetVal;
        }

        private static Boolean TestCnnt(OleDbConnection dbcnnt)
        {
            Boolean objRetVal = false;
            try
            {
                dbcnnt.Open();
                dbcnnt.Close();
                objRetVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objRetVal;
        }

        public static Boolean DataTableToXls(string pathBDXls, DataTable dtXml)
        {
            return DataTableToXls(pathBDXls, dtXml, null, false);
        }

        public static Boolean DataTableToXls(string pathBDXls, DataTable dtXml, String[] maping, Boolean append)
        {
            Boolean exportado = false;
            OleDbConnection dbCnnt = null;
            Object value = null;
            int index = 0;
            string valor = "";
            string table = "";
            string insert = "";
            try
            {
                if (dtXml == null)
                {
                    //El Datatable es nulo
                    throw new NullReferenceException("El Objeto Datatable es nulo. No se puede realizar la exportación");
                    //return false; 
                }
                if (dtXml.TableName.Equals(""))
                {
                    //No se ha definido el nombre del Datatable
                    return false;
                }
                if (pathBDXls.Trim().Equals(""))
                {
                    //No se ha definido una ruta valida para la exportación
                    return false;
                }
                dbCnnt = DBXlsConnect(pathBDXls);
                Int32 iRows = dtXml.Rows.Count;
                Int32 iCount = 0;
                string sTb = dtXml.TableName;
                string sCm = String.Empty;
                Boolean bMp = false;
                StringBuilder sCm0 = new StringBuilder();
                StringBuilder sCm1 = new StringBuilder();
                sCm0.Append("(");
                sCm1.Append("(");
                foreach (DataColumn dc in dtXml.Columns)
                {
                    if (dc.Ordinal == 0)
                    {
                        sCm0.Append("[");
                        sCm1.Append("[");
                    }
                    else
                    {
                        sCm0.Append(", [");
                        sCm1.Append(", [");
                    }
                    if (bMp)
                    {
                        sCm = maping[dc.Ordinal].Trim();
                    }
                    else
                    {
                        sCm = dc.ColumnName.Trim();
                    }

                    sCm0.Append(sCm);
                    sCm1.Append(sCm);
                    if (dc.DataType.Equals(System.Type.GetType("System.String")))
                    {
                        sCm0.Append("] NVARCHAR");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.DateTime")))
                    {
                        sCm0.Append("] DateTime");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Decimal")) || dc.DataType.Equals(System.Type.GetType("System.Int32")) || dc.DataType.Equals(System.Type.GetType("System.Double")))
                    {
                        sCm0.Append("] Decimal");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Byte")) || dc.DataType.Equals(System.Type.GetType("System.Int16")) || dc.DataType.Equals(System.Type.GetType("System.Int32")))
                    {
                        sCm0.Append("] Int");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Boolean")))
                    {
                        sCm0.Append("] Boolean");
                    }
                    else
                    {
                        sCm0.Append("] NVARCHAR");
                    }
                    sCm1.Append("]");
                }

                sCm0.Append(")");
                sCm1.Append(")");
                table = sCm0.ToString();
                if (DBXlsCreateTable(dbCnnt, dtXml.TableName, sCm0.ToString()) == false)
                {

                }
                if (dtXml.Rows.Count == 0)
                {

                }

                StringBuilder sCm2;
                string SCmd = String.Empty;
                OleDbCommand dbCmd = new OleDbCommand();
                int iCs = dtXml.Columns.Count - 1;
                Int16 i;
                string strValor = String.Empty;
                dbCmd.Connection = dbCnnt;
                dbCnnt.Open();

                foreach (DataRow dr in dtXml.Rows)
                {
                    value = dr;
                    iCount = iCount + 1;
                    sCm2 = new StringBuilder();
                    sCm2.Append("(");
                    for (i = 0; i <= iCs; i++)
                    {
                        index = i;
                        if (i == 0)
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                            {
                                sCm2.Append("'");
                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                if (dr[i] != System.DBNull.Value)
                                {
                                    sCm2.Append("'");
                                }
                            }
                        }
                        else
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                            {
                                sCm2.Append(", '");
                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                if (dr[i] != System.DBNull.Value)
                                {
                                    //Verificamos que la fecha sea mayor a 1900
                                    if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                    {
                                        sCm2.Append(", '");
                                    }
                                    else
                                    {
                                        sCm2.Append(", ");
                                    }

                                }
                                else
                                {
                                    sCm2.Append(", ");
                                }
                            }
                            else
                            {
                                sCm2.Append(", ");
                            }
                        }
                        if (dr[i] == System.DBNull.Value)
                        {
                            strValor = "";
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                strValor = "Null";
                            }
                        }
                        else
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                //Verificamos que la fecha sea mayor a 1900
                                if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                {
                                    strValor = Convert.ToDateTime(dr[i]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    strValor = "Null";
                                }

                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Boolean")))
                            {
                                if (Convert.ToBoolean(dtXml.Columns[i]) == false)
                                {
                                    strValor = "False";
                                }
                                else
                                {
                                    strValor = "True";
                                }
                            }
                            else
                            {
                                strValor = dr[i].ToString().Replace("'", "''");
                                valor = strValor;
                            }
                        }
                        sCm2.Append(strValor);
                        if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                        {
                            sCm2.Append("'");
                        }
                        else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                        {
                            if (dr[i] != System.DBNull.Value)
                            {
                                //Verificamos que la fecha sea mayor a 1900
                                if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                {
                                    sCm2.Append("'");
                                }

                            }
                        }
                        else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Decimal")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Int16")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Int32")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Double")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Byte")))
                        {
                            if (strValor.Length == 0)
                            {
                                sCm2.Append("0");
                            }
                        }
                    }
                    sCm2.Append(")");
                    SCmd = "INSERT INTO " + "[" + sTb + "]" + sCm1.ToString() + " Values " + sCm2.ToString();
                    insert = "INSERT INTO " + "[" + sTb + "]" + sCm1.ToString() + " Values " + sCm2.ToString();
                    dbCmd.CommandText = SCmd;
                    dbCmd.ExecuteNonQuery();

                }
                dbCnnt.Close();
            }
            catch (Exception ex)
            {
                if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
                throw ex;
            }
            finally
            {
            }
            return exportado;
        }

        /// <summary>
        /// Exporta el objeto Datatable a Excel compatible para la versión 2003
        /// </summary>
        /// <param name="pathBDXls">Ruta completa del archivo Excel 2003</param>
        /// <param name="dtXml">Objeto Datatable que contiene los registros a exportar</param>
        /// <returns></returns>
        public static Boolean DataTableToXls2003(string pathBDXls, DataTable dtXml)
        {
            Boolean exportado = false;
            OleDbConnection dbCnnt = null;
            try
            {
                if (dtXml == null)
                {
                    throw new NullReferenceException("El Objeto Datatable es nulo. No se puede realizar la exportación");
                }

                dbCnnt = DBXlsConnect(pathBDXls);
                if (dtXml.Rows.Count > 65534)
                {
                    int count = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(dtXml.Rows.Count) / 65534.00));
                    int rowIni = 0;
                    int rowFin = 0;
                    for (int i = 0; i < count; i++)
                    {
                        rowFin = (i == (count - 1)) ? (dtXml.Rows.Count) : (rowFin + 65534);
                        DataTableToXls2003(pathBDXls, dtXml, dbCnnt, rowIni, rowFin, i);
                        rowIni = rowFin + 1;
                    }
                }
                else
                {
                    int rowIni = 0;
                    int rowFin = dtXml.Rows.Count;
                    DataTableToXls2003(pathBDXls, dtXml, dbCnnt, rowIni, rowFin, 0);
                }
            }
            catch (Exception ex)
            {
                if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
                throw ex;
            }
            return exportado;
        }

        /// <summary>
        /// Exporta el objeto Datatable a Excel compatible para la versón 2007
        /// </summary>
        /// <param name="pathBDXls">Ruta completa del archivo Excel 2007</param>
        /// <param name="dtXml">Objeto Datatable que contiene los registros a exportar</param>
        /// <returns></returns>
        public static Boolean DataTableToXls2007(string pathBDXls, DataTable dtXml)
        {
            Boolean exportado = false;
            OleDbConnection dbCnnt = null;
            Object value = null;
            int index = 0;
            string valor = "";
            string table = "";
            string insert = "";
            FileInfo archivoexcel = null;
            string logarchivo = string.Empty;
            DateTime fechaarchivo = DateTime.Now;
            try
            {
                archivoexcel = new FileInfo(pathBDXls);
                logarchivo = "-------------------------------------------------------------------------------------------------------------------\n\n";
                logarchivo = logarchivo + "Registro de generacion de archivo: " + archivoexcel.Name + "\n";
                if (dtXml == null)
                {
                    //El Datatable es nulo

                    throw new NullReferenceException("El Objeto Datatable es nulo. No se puede realizar la exportación");
                    //return false; 
                }
                if (dtXml.TableName.Equals(""))
                {
                    //No se ha definido el nombre del Datatable
                    return false;
                }
                if (pathBDXls.Trim().Equals(""))
                {
                    //No se ha definido una ruta valida para la exportación
                    return false;
                }
                logarchivo = logarchivo + "Generando conexion con origen de datos excel 2007...\n";
                dbCnnt = DBXls2007Connect(pathBDXls);
                logarchivo = logarchivo + "Conexion generada\n";
                Int32 iRows = dtXml.Rows.Count;
                Int32 iCount = 0;
                string sTb = dtXml.TableName;
                string sCm = String.Empty;
                Boolean bMp = false;
                StringBuilder sCm0 = new StringBuilder();
                StringBuilder sCm1 = new StringBuilder();
                sCm0.Append("(");
                sCm1.Append("(");

                foreach (DataColumn dc in dtXml.Columns)
                {
                    if (dc.Ordinal == 0)
                    {
                        sCm0.Append("[");
                        sCm1.Append("[");
                    }
                    else
                    {
                        sCm0.Append(", [");
                        sCm1.Append(", [");
                    }
                    if (bMp)
                    {
                        //sCm = maping[dc.Ordinal].Trim();
                    }
                    else
                    {
                        sCm = dc.ColumnName.Trim();
                    }

                    sCm0.Append(sCm);
                    sCm1.Append(sCm);
                    if (dc.DataType.Equals(System.Type.GetType("System.String")))
                    {
                        sCm0.Append("] NVARCHAR");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.DateTime")))
                    {
                        sCm0.Append("] DateTime");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Decimal")) || dc.DataType.Equals(System.Type.GetType("System.Int32")) || dc.DataType.Equals(System.Type.GetType("System.Double")))
                    {
                        sCm0.Append("] Decimal");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Byte")) || dc.DataType.Equals(System.Type.GetType("System.Int16")) || dc.DataType.Equals(System.Type.GetType("System.Int32")))
                    {
                        sCm0.Append("] Int");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Boolean")))
                    {
                        sCm0.Append("] Boolean");
                    }
                    else
                    {
                        sCm0.Append("] NVARCHAR");
                    }
                    sCm1.Append("]");
                }

                sCm0.Append(")");
                sCm1.Append(")");
                table = sCm0.ToString();
                logarchivo = logarchivo + "Generando archivo Excel 2007...\n";
                if (DBXlsCreateTable(dbCnnt, dtXml.TableName, sCm0.ToString()) == false)
                {

                }
                logarchivo = logarchivo + "Archivo Excel 2007 generado con exito en : " + pathBDXls + "\n";
                StringBuilder sCm2;
                string SCmd = String.Empty;
                OleDbCommand dbCmd = new OleDbCommand();
                int iCs = dtXml.Columns.Count - 1;
                Int16 i;
                string strValor = String.Empty;
                dbCmd.Connection = dbCnnt;
                dbCnnt.Open();
                logarchivo = logarchivo + "Cantidad de registros a insertar en el archivo Excel: " + dtXml.Rows.Count + "\n";
                logarchivo = logarchivo + "Insertando registros en archivo Excel...\n";
                //for (int fila = 0; fila < dtXml.Rows.Count;fila++ ){
                //}
                foreach (DataRow dr in dtXml.Rows)
                {
                    value = dr;
                    iCount = iCount + 1;
                    sCm2 = new StringBuilder();
                    sCm2.Append("(");
                    for (i = 0; i <= iCs; i++)
                    {
                        index = i;
                        if (i == 0)
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                            {
                                sCm2.Append("'");
                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                if (dr[i] != System.DBNull.Value)
                                {
                                    sCm2.Append("'");
                                }
                            }
                        }
                        else
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                            {
                                sCm2.Append(", '");
                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                if (dr[i] != System.DBNull.Value)
                                {
                                    //Verificamos que la fecha sea mayor a 1900
                                    if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                    {
                                        sCm2.Append(", '");
                                    }
                                    else
                                    {
                                        sCm2.Append(", ");
                                    }
                                }
                                else
                                {
                                    sCm2.Append(", ");
                                }
                            }
                            else
                            {
                                sCm2.Append(", ");
                            }
                        }
                        if (dr[i] == System.DBNull.Value)
                        {
                            strValor = "";
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                strValor = "Null";
                            }
                        }
                        else
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                //Verificamos que la fecha sea mayor a 1900
                                if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                {
                                    strValor = Convert.ToDateTime(dr[i]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    strValor = "Null";
                                }
                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Boolean")))
                            {
                                if (Convert.ToBoolean(dtXml.Columns[i]) == false)
                                {
                                    strValor = "False";
                                }
                                else
                                {
                                    strValor = "True";
                                }
                            }
                            else
                            {
                                strValor = dr[i].ToString().Replace("'", "''");
                                valor = strValor;
                            }
                        }
                        sCm2.Append(strValor.Replace("\n", " ").Replace("\r", " "));
                        if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                        {
                            sCm2.Append("'");
                        }
                        else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                        {
                            if (dr[i] != System.DBNull.Value)
                            {
                                //Verificamos que la fecha sea mayor a 1900
                                if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                {
                                    sCm2.Append("'");
                                }
                            }
                        }
                        else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Decimal")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Int16")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Int32")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Double")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Byte")))
                        {
                            if (strValor.Length == 0)
                            {
                                sCm2.Append("0");
                            }
                        }
                    }
                    sCm2.Append(")");
                    SCmd = "INSERT INTO " + "[" + sTb + "]" + sCm1.ToString() + " Values " + sCm2.ToString();
                    insert = "INSERT INTO " + "[" + sTb + "]" + sCm1.ToString() + " Values " + sCm2.ToString();
                    dbCmd.CommandText = SCmd;
                    dbCmd.ExecuteNonQuery();
                }
                logarchivo = logarchivo + "Insertados " + iCount + " registros en archivo Excel con exito\n";
                logarchivo = logarchivo + "Cerrando conexion en archivo Excel: " + pathBDXls + "...\n";
                dbCnnt.Close();
                logarchivo = logarchivo + "Conexion en archivo Excel " + pathBDXls + " cerrada con exito\n\n";
                Log_Archivo(logarchivo, archivoexcel.DirectoryName + "\\" + "LogExcel_" + fechaarchivo.ToString("dd-MM-yyyy") + ".txt");
            }
            catch (Exception ex)
            {
                insert = "Valor: " + insert;
                if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
                throw ex;
            }
            finally
            {
                if (dbCnnt != null)
                {
                    if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
                }

            }
            return exportado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathBDXls">Ruta completa del archivo Excel</param>
        /// <param name="dtXml">Objeto Datatable que contiene los datos a exportar</param>
        /// <param name="dbCnnt">Conexion Oledb</param>
        /// <param name="rowIni">Fila inicial</param>
        /// <param name="rowFin">Fila final</param>
        /// <param name="count">Contador</param>
        private static void DataTableToXls2003(string pathBDXls, DataTable dtXml, OleDbConnection dbCnnt, int rowIni, int rowFin, int count)
        {
            try
            {
                Int32 iRows = dtXml.Rows.Count;
                Int32 iCount = 0;
                string sTb = dtXml.TableName;
                string sCm = String.Empty;
                Boolean bMp = false;
                StringBuilder sCm0 = new StringBuilder();
                StringBuilder sCm1 = new StringBuilder();
                sCm0.Append("(");
                sCm1.Append("(");
                FileInfo archivoexcel = null;
                string logarchivo = string.Empty;
                DateTime fechaarchivo = DateTime.Now;

                archivoexcel = new FileInfo(pathBDXls);
                logarchivo = "-------------------------------------------------------------------------------------------------------------------\n\n";
                logarchivo = logarchivo + "Registro de generacion de archivo: " + archivoexcel.Name + "\n";

                logarchivo = logarchivo + "Generando conexion con origen de datos excel 2003...\n";
                logarchivo = logarchivo + "Conexion generada\n";

                foreach (DataColumn dc in dtXml.Columns)
                {
                    if (dc.Ordinal == 0)
                    {
                        sCm0.Append("[");
                        sCm1.Append("[");
                    }
                    else
                    {
                        sCm0.Append(", [");
                        sCm1.Append(", [");
                    }
                    if (bMp)
                    {
                        //sCm = maping[dc.Ordinal].Trim();
                    }
                    else
                    {
                        sCm = dc.ColumnName.Trim();
                    }

                    sCm0.Append(sCm);
                    sCm1.Append(sCm);
                    if (dc.DataType.Equals(System.Type.GetType("System.String")))
                    {
                        sCm0.Append("] NVARCHAR");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.DateTime")))
                    {
                        sCm0.Append("] DateTime");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Decimal")) || dc.DataType.Equals(System.Type.GetType("System.Int32")) || dc.DataType.Equals(System.Type.GetType("System.Double")))
                    {
                        sCm0.Append("] Decimal");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Byte")) || dc.DataType.Equals(System.Type.GetType("System.Int16")) || dc.DataType.Equals(System.Type.GetType("System.Int32")))
                    {
                        sCm0.Append("] Int");
                    }
                    else if (dc.DataType.Equals(System.Type.GetType("System.Boolean")))
                    {
                        sCm0.Append("] Boolean");
                    }
                    else
                    {
                        sCm0.Append("] NVARCHAR");
                    }
                    sCm1.Append("]");
                }

                sCm0.Append(")");
                sCm1.Append(")");
                logarchivo = logarchivo + "Generando archivo Excel 2003...\n";
                if (DBXlsCreateTable(dbCnnt, (dtXml.TableName + (count + 1).ToString()), sCm0.ToString()) == false)
                {

                }
                logarchivo = logarchivo + "Archivo Excel 2003 generado con exito en : " + pathBDXls + "\n";
                StringBuilder sCm2;
                string SCmd = String.Empty;
                OleDbCommand dbCmd = new OleDbCommand();
                int iCs = dtXml.Columns.Count - 1;

                Int16 i;
                string strValor = String.Empty;
                dbCmd.Connection = dbCnnt;
                dbCnnt.Open();
                logarchivo = logarchivo + "Cantidad de registros a insertar en el archivo Excel: " + dtXml.Rows.Count + "\n";
                logarchivo = logarchivo + "Insertando registros en archivo Excel...\n";
                for (int fila = rowIni; fila < rowFin; fila++)
                {
                    DataRow dr = dtXml.Rows[fila];
                    iCount = iCount + 1;
                    sCm2 = new StringBuilder();
                    sCm2.Append("(");
                    for (i = 0; i <= iCs; i++)
                    {

                        if (i == 0)
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                            {
                                sCm2.Append("'");
                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                if (dr[i] != System.DBNull.Value)
                                {
                                    sCm2.Append("'");
                                }
                            }
                        }
                        else
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                            {
                                sCm2.Append(", '");
                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                if (dr[i] != System.DBNull.Value)
                                {
                                    //Verificamos que la fecha sea mayor a 1900
                                    if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                    {
                                        sCm2.Append(", '");
                                    }
                                    else
                                    {
                                        sCm2.Append(", ");
                                    }
                                }
                                else
                                {
                                    sCm2.Append(", ");
                                }
                            }
                            else
                            {
                                sCm2.Append(", ");
                            }
                        }
                        if (dr[i] == System.DBNull.Value)
                        {
                            strValor = "";
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                strValor = "Null";
                            }
                        }
                        else
                        {
                            if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                            {
                                //Verificamos que la fecha sea mayor a 1900
                                if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                {
                                    strValor = Convert.ToDateTime(dr[i]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    strValor = "Null";
                                }

                            }
                            else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Boolean")))
                            {
                                if (Convert.ToBoolean(dtXml.Columns[i]) == false)
                                {
                                    strValor = "False";
                                }
                                else
                                {
                                    strValor = "True";
                                }
                            }
                            else
                            {
                                strValor = dr[i].ToString().Replace("'", "''");
                                //valor = strValor;
                            }
                        }
                        sCm2.Append(strValor);
                        if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.String")))
                        {
                            sCm2.Append("'");
                        }
                        else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                        {
                            if (dr[i] != System.DBNull.Value)
                            {
                                //Verificamos que la fecha sea mayor a 1900
                                if (Convert.ToDateTime(dr[i]).Year >= 1900)
                                {
                                    sCm2.Append("'");
                                }
                            }
                        }
                        else if (dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Decimal")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Int16")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Int32")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Double")) || dtXml.Columns[i].DataType.Equals(System.Type.GetType("System.Byte")))
                        {
                            if (strValor.Length == 0)
                            {
                                sCm2.Append("0");
                            }
                        }
                    }
                    sCm2.Append(")");
                    SCmd = "INSERT INTO " + "[" + (sTb + (count + 1).ToString()) + "]" + sCm1.ToString() + " Values " + sCm2.ToString();
                    //insert = "INSERT INTO " + "[" + sTb + "]" + sCm1.ToString() + " Values " + sCm2.ToString();

                    dbCmd.CommandText = SCmd;
                    dbCmd.ExecuteNonQuery();

                }
                logarchivo = logarchivo + "Insertados " + iCount + " registros en archivo Excel con exito\n";
                logarchivo = logarchivo + "Cerrando conexion en archivo Excel: " + pathBDXls + "...\n";
                logarchivo = logarchivo + "Conexion en archivo Excel " + pathBDXls + " cerrada con exito\n\n";
                Log_Archivo(logarchivo, archivoexcel.DirectoryName + "\\" + "LogExcel_" + fechaarchivo.ToString("dd-MM-yyyy") + ".txt");
                dbCnnt.Close();
            }
            catch (Exception ex)
            {
                if (dbCnnt.State == ConnectionState.Open) { dbCnnt.Close(); }
                throw ex;
            }
        }

        public static void Log_Archivo(string texto, string filename)
        {
            try
            {
                FileInfo archivo = new FileInfo(filename);
                FileStream stream = null;
                string textoanterior = string.Empty;
                if (archivo.Exists)
                {
                    stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    stream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
                }
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);
                textoanterior = reader.ReadToEnd();
                writer.WriteLine(texto);
                writer.Close();
                writer.Dispose();
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
