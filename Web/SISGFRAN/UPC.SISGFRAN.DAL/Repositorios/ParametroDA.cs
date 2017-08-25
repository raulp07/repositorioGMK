using DAAB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Base;
using UPC.SISGFRAN.EL.Comunes;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.DAL.Repositorios
{
    public class ParametroDA
    {
        public List<ParametroEL> GetParametros(ParametroEL codigo)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@codigo", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idAgrupador", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = codigo.Codigo;
            arrParam[1].Value = codigo.CodigoGrupo;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Parametro";
            objRequest.Parameters.AddRange(arrParam);

            List<ParametroEL> lstCodigos = new List<ParametroEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    ParametroEL item = new ParametroEL();

                    item.Codigo = Funciones.CheckInt(dr["Codigo"]);
                    item.Descripcion = Funciones.CheckStr(dr["Nombre"]);
                    item.Valor = Funciones.CheckStr(dr["Valor"]);
                    item.CodigoGrupo = Funciones.CheckInt(dr["CodigoGrupo"]);
                    item.Grupo = Funciones.CheckStr(dr["Grupo"]);
                    item.Descripcion = Funciones.CheckStr(dr["Descripcion"]);
                    item.Value1 = Funciones.CheckStr(dr["ValueText1"]);
                    item.Value2 = Funciones.CheckStr(dr["ValueText2"]);
                    item.Value3 = Funciones.CheckStr(dr["ValueText3"]);
                    lstCodigos.Add(item);
                }
            }
            catch (Exception e)
            {
                lstCodigos = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstCodigos;
        }

        public ParametroEL GetParametroByID(int? codigoId)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@codigo", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idAgrupador", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = codigoId;
            arrParam[1].Value = string.Empty;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Parametro";
            objRequest.Parameters.AddRange(arrParam);

            ParametroEL codigo = null;
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    codigo = new ParametroEL();
                    codigo.Codigo = Funciones.CheckInt(dr["Codigo"]);
                    codigo.Descripcion = Funciones.CheckStr(dr["Nombre"]);
                    codigo.Valor = Funciones.CheckStr(dr["Valor"]);
                    codigo.CodigoGrupo = Funciones.CheckInt(dr["CodigoGrupo"]);
                    codigo.Grupo = Funciones.CheckStr(dr["Grupo"]);
                    codigo.Descripcion = Funciones.CheckStr(dr["Descripcion"]);
                    codigo.Value1 = Funciones.CheckStr(dr["ValueText1"]);
                    codigo.Value2 = Funciones.CheckStr(dr["ValueText2"]);
                    codigo.Value3 = Funciones.CheckStr(dr["ValueText3"]);
                }
            }
            catch (Exception e)
            {
                codigo = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return codigo;
        }
    }
}
