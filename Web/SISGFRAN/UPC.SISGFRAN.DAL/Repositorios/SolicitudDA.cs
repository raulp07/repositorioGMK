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
    public class SolicitudDA
    {
        public List<SolicitudEL> GetSolicitudesPendientes(string desc)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@solicitudId", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@desc", DbType.String, 100 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@estado", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = Constantes.Filtros.Todos;
            arrParam[1].Value = desc;
            arrParam[2].Value = Constantes.EstadoSolicitud.Pendiente;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_SolicitudXSolicitante";
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
                    item.FechaSolicitud = Funciones.CheckDate(dr["FechaSolicitud"]);

                    SolicitanteEL solicitante = new SolicitanteEL();
                    solicitante.Id = Funciones.CheckInt(dr["IdSolicitante"]);
                    solicitante.ApellidoPaterno = Funciones.CheckStr(dr["ApellidoPaterno"]);
                    solicitante.ApellidoMaterno = Funciones.CheckStr(dr["ApellidoMaterno"]);
                    solicitante.Nombres = Funciones.CheckStr(dr["Nombres"]);
                    solicitante.Direccion = Funciones.CheckStr(dr["Direccion"]);
                    solicitante.Email = Funciones.CheckStr(dr["Email"]);

                    ParametroEL oTipoDocumento = new ParametroEL()
                    {
                        Codigo = Funciones.CheckInt(dr["TipoDocumentoId"]),
                        Nombre = Funciones.CheckStr(dr["TipoDocumento"])
                    };

                    solicitante.TipoDocumento = oTipoDocumento;
                    solicitante.NumeroDocumento = Funciones.CheckStr(dr["NumeroDocumento"]);

                    ParametroEL oEstado = new ParametroEL()
                    {
                        Codigo = Funciones.CheckInt(dr["EstadoId"]),
                        Nombre = Funciones.CheckStr(dr["Estado"])
                    };

                    item.Estado = oEstado;

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

        public SolicitudEL GetResultadoEvaluacion(int IdSolicitud)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@solicitudId", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = IdSolicitud;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_ResultadoEvaluacion";
            objRequest.Parameters.AddRange(arrParam);

            SolicitudEL item = null;
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    item = new SolicitudEL();
                    item.Id = Funciones.CheckInt(dr["SolicitudId"]);
                    item.NumSolicitud = Funciones.CheckStr(dr["NumSolicitud"]);
                    item.FechaSolicitud = Funciones.CheckDate(dr["FechaSolicitud"]);
                    item.MontoCapital = Funciones.CheckDecimal(dr["MontoCapital"]);

                    ParametroEL estado = new ParametroEL() {
                        Codigo = Funciones.CheckInt(dr["EstadoId"]),
                        Nombre = Funciones.CheckStr(dr["Estado"])
                    };

                    item.Estado = estado;

                    ReporteEvaluacionEL reporte = new ReporteEvaluacionEL()
                    {
                        Id = Funciones.CheckInt(dr["ReporteEvalId"]),
                        Fecha = Funciones.CheckDate(dr["FechaReporte"]),
                        ResultadoEjercicio = Funciones.CheckStr(dr["ResultadoEjercicio"]),
                        ErroresEncontrados = Funciones.CheckStr(dr["ErroresEncontrados"])
                    };

                    SolicitanteEL solicitante = new SolicitanteEL()
                    {
                        ApellidoPaterno = Funciones.CheckStr(dr["ApellidoPaterno"]),
                        ApellidoMaterno = Funciones.CheckStr(dr["ApellidoMaterno"]),
                        Nombres = Funciones.CheckStr(dr["Nombres"])
                    };

                    item.ReporteEvaluacion = reporte;
                    item.Solicitante = solicitante;
                }
            }
            catch (Exception e)
            {
                item = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return item;
        }

        public SolicitudEL Actualizar(SolicitudEL solicitud)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@solicitudId", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@estado", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@usuarioId", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = solicitud.Id;
            arrParam[1].Value = solicitud.Estado.Codigo;
            arrParam[2].Value = solicitud.UsuarioModifica;

            configPARDOSDB objTrackDb = new configPARDOSDB();
            DAABRequest objRequest = objTrackDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPU_Solicitud";
            objRequest.Parameters.AddRange(arrParam);

            SolicitudEL solicitudActualizado = null;
            try
            {
                objRequest.Factory.ExecuteNonQuery(ref objRequest);
                IDataParameter p1, p2;
                p2 = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 2];
                p1 = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 1];

                solicitudActualizado = solicitud;
                solicitudActualizado.CodeMessage = Funciones.CheckInt(p2.Value);
                solicitudActualizado.MessageErr = p1.Value.ToString();

                if (solicitudActualizado.CodeMessage == 0)
                {
                    solicitudActualizado = GetSolicitudById(solicitud.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return solicitudActualizado;
        }

        public SolicitudEL GetSolicitudById(int solicitudId)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@solicitudId", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@desc", DbType.String, 100 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@estado", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = solicitudId;
            arrParam[1].Value = string.Empty;
            arrParam[2].Value = Constantes.EstadoSolicitud.Todos;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_SolicitudXSolicitante";
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
                    oSolicitud.FechaSolicitud = Funciones.CheckDate(dr["FechaSolicitud"]);

                    SolicitanteEL solicitante = new SolicitanteEL();
                    solicitante.Id = Funciones.CheckInt(dr["IdSolicitante"]);
                    solicitante.ApellidoPaterno = Funciones.CheckStr(dr["ApellidoPaterno"]);
                    solicitante.ApellidoMaterno = Funciones.CheckStr(dr["ApellidoMaterno"]);
                    solicitante.Nombres = Funciones.CheckStr(dr["Nombres"]);
                    solicitante.Direccion = Funciones.CheckStr(dr["Direccion"]);
                    solicitante.Email = Funciones.CheckStr(dr["Email"]);

                    ParametroEL oTipoDocumento = new ParametroEL()
                    {
                        Codigo = Funciones.CheckInt(dr["TipoDocumentoId"]),
                        Nombre = Funciones.CheckStr(dr["TipoDocumento"])
                    };

                    solicitante.TipoDocumento = oTipoDocumento;
                    solicitante.NumeroDocumento = Funciones.CheckStr(dr["NumeroDocumento"]);

                    ParametroEL oEstado = new ParametroEL()
                    {
                        Codigo = Funciones.CheckInt(dr["EstadoId"]),
                        Nombre = Funciones.CheckStr(dr["Estado"])
                    };

                    oSolicitud.Estado = oEstado;

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

        public SolicitudEL RegistrarReporteEvaluacion(SolicitudEL solicitud)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@solicitudId", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@resultado", DbType.String, 250 , ParameterDirection.Input),
                new DAABRequest.Parameter("@errores", DbType.String, 400 , ParameterDirection.Input),
                new DAABRequest.Parameter("@usuarioId", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idResultadoEva", DbType.Int32, ParameterDirection.Output),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = solicitud.Id;
            arrParam[1].Value = Funciones.CheckStr(solicitud.ReporteEvaluacion.ResultadoEjercicio);
            arrParam[2].Value = Funciones.CheckStr(solicitud.ReporteEvaluacion.ErroresEncontrados);
            arrParam[3].Value = solicitud.ReporteEvaluacion.UsuarioCreacion;

            SolicitudEL solicitudRegistrado = null;

            configPARDOSDB objTrackDb = new configPARDOSDB();
            DAABRequest objRequest = objTrackDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPI_ResultadoEvaluacion";
            objRequest.Parameters.AddRange(arrParam);
            try
            {
                objRequest.Factory.ExecuteScalar(ref objRequest);
                IDataParameter p1, p2, pSalida;
                pSalida = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 3];
                p2 = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 2];
                p1 = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 1];

                solicitudRegistrado = solicitud;
                solicitudRegistrado.CodeMessage = Funciones.CheckInt(p2.Value);
                solicitudRegistrado.MessageErr = p1.Value.ToString();
                solicitudRegistrado.ReporteEvaluacion.Id = Funciones.CheckInt(pSalida.Value);
            }
            catch (Exception ex)
            {
                objRequest.Factory.RollBackTransaction();
                throw ex;
            }
            finally
            {
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return solicitudRegistrado;
        }

    }
}
