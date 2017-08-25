using DAAB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Base;
using UPC.SISGFRAN.EL.Comunes;
using UPC.SISGFRAN.EL.Inherited;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.DAL.Repositorios
{
    public class SolicitanteDA
    {

        public void UpdateApprovalStatus(int idSolicitante, bool fueAprobado)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@IdSolicitante", DbType.Int32,ParameterDirection.Input),
                new DAABRequest.Parameter("@FueAprobado", DbType.Boolean,ParameterDirection.Input)
            };

            arrParam[0].Value = idSolicitante;
            arrParam[1].Value = fueAprobado;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPI_EvaluacionSolicitante";
            objRequest.Parameters.AddRange(arrParam);

            try
            {
                int result = objRequest.Factory.ExecuteNonQuery(ref objRequest);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public List<SolicitudEL> GetSolicitantes()
        {
            DAABRequest.Parameter[] arrParam = {            
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };            

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_SolicitanteXEvaluar";
            objRequest.Parameters.AddRange(arrParam);

            List<SolicitudEL> lstSolicitudes = new List<SolicitudEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    SolicitudEL item = new SolicitudEL();
                    item.Id = Funciones.CheckInt(dr["IdSolicitud"]);
                    item.NumSolicitud = Funciones.CheckStr(dr["NumSolicitud"]);                    
                    
                    SolicitanteEL solicitante = new SolicitanteEL();
                    solicitante.Id = Funciones.CheckInt(dr["IdSolicitante"]);
                    solicitante.ApellidoPaterno = Funciones.CheckStr(dr["ApellidoPaterno"]);
                    solicitante.ApellidoMaterno = Funciones.CheckStr(dr["ApellidoMaterno"]);
                    solicitante.Nombres = Funciones.CheckStr(dr["Nombres"]);
                    solicitante.Sexo = new ParametroEL()
                    {
                        Nombre = Funciones.CheckStr(dr["Sexo"])
                    };
                        
                    ParametroEL oTipoDocumento = new ParametroEL()
                    {
                        Codigo = Funciones.CheckInt(dr["TipoDocumentoId"]),
                        Nombre = Funciones.CheckStr(dr["TipoDocumento"])
                    };
                    solicitante.TituloObtenido = Funciones.CheckStr(dr["TituloObtenido"]);
                    solicitante.MontoIngresosMes = Funciones.CheckDecimal(dr["MontoIngresosMes"]);
                    solicitante.MontoGastosMes = Funciones.CheckDecimal(dr["MontoGastosMes"]);
                    solicitante.Cargo = Funciones.CheckStr(dr["Cargo"]);
                    solicitante.TipoDocumento = oTipoDocumento;
                    solicitante.FechaNacimiento = Funciones.CheckDate(dr["FechaNacimiento"]);
                    item.NumeroDocumento = Funciones.CheckStr(dr["NumeroDocumento"]);
                               
                    item.Solicitante = solicitante;
                    lstSolicitudes.Add(item);
                }
            }
            catch (Exception e)
            {
                lstSolicitudes = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstSolicitudes;
        }

        public SolicitudEL GetSolicitante(int id)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@solId", DbType.Int32,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = id;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Solicitante";
            objRequest.Parameters.AddRange(arrParam);

            SolicitudEL oSolicitud = null;
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    oSolicitud = new SolicitudEL();
                    oSolicitud.Id = Funciones.CheckInt(dr["IdSolicitud"]);
                    oSolicitud.NumSolicitud = Funciones.CheckStr(dr["NumSolicitud"]);

                    SolicitanteEL solicitante = new SolicitanteEL();
                    solicitante.Id = Funciones.CheckInt(dr["IdSolicitante"]);
                    solicitante.ApellidoPaterno = Funciones.CheckStr(dr["ApellidoPaterno"]);
                    solicitante.ApellidoMaterno = Funciones.CheckStr(dr["ApellidoMaterno"]);
                    solicitante.Nombres = Funciones.CheckStr(dr["Nombres"]);
                    solicitante.Sexo = new ParametroEL()
                    {
                        Nombre = Funciones.CheckStr(dr["Sexo"])
                    };

                    ParametroEL oTipoDocumento = new ParametroEL()
                    {
                        Codigo = Funciones.CheckInt(dr["TipoDocumentoId"]),
                        Nombre = Funciones.CheckStr(dr["TipoDocumento"])
                    };
                    solicitante.TituloObtenido = Funciones.CheckStr(dr["TituloObtenido"]);
                    solicitante.MontoIngresosMes = Funciones.CheckDecimal(dr["MontoIngresosMes"]);
                    solicitante.MontoGastosMes = Funciones.CheckDecimal(dr["MontoGastosMes"]);
                    solicitante.Cargo = Funciones.CheckStr(dr["Cargo"]);
                    solicitante.TipoDocumento = oTipoDocumento;
                    solicitante.FechaNacimiento = Funciones.CheckDate(dr["FechaNacimiento"]);
                    oSolicitud.NumeroDocumento = Funciones.CheckStr(dr["NumeroDocumento"]);
                    oSolicitud.MontoCapital = Funciones.CheckDecimal(dr["MontoCapital"]);

                    oSolicitud.Solicitante = solicitante;
                }
            }
            catch (Exception e)
            {
                oSolicitud = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return oSolicitud;
        }
    }
}
