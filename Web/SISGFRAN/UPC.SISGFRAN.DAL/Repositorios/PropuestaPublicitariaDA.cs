using DAAB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Base;
using UPC.SISGFRAN.EL.Comunes;
using UPC.SISGFRAN.EL.Inherited;
using UPC.SISGFRAN.EL.NonInherited;
namespace UPC.SISGFRAN.DAL.Repositorios
{
    public class PropuestaPublicitariaDA
    {
        public List<LocalEL> GetAllDatoMedio_X_Local()
        {
            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "sp_Lista_Medios_X_Locales";

            List<LocalEL> lstLocal = new List<LocalEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    LocalEL local = new LocalEL();
                    local.puntajeCaracteristicaCombo = Funciones.CheckInt(dr["puntajeCaracteristicaCombo"]);
                    local.Porcentaje = Funciones.CheckInt(dr["Porcentaje"]);
                    local.codEncuesta = Funciones.CheckInt(dr["codEncuesta"]);
                    local.nombreEncuesta = Funciones.CheckStr(dr["nombreEncuesta"]);
                    local.codMedioComunicacion = Funciones.CheckInt(dr["codMedioComunicacion"]);
                    local.nombreMedioComunicacion = Funciones.CheckStr(dr["nombreMedioComunicacion"]);
                    local.costoUnitarioMedioComunicacion = Funciones.CheckDecimal(dr["costoUnitarioMedioComunicacion"]);
                    local.codLocal = Funciones.CheckInt(dr["codLocal"]);
                    local.nombreLocal = Funciones.CheckStr(dr["nombreLocal"]);
                    local.latitudLocal = Funciones.CheckStr(dr["latitudLocal"]);
                    local.longitudLocal = Funciones.CheckStr(dr["longitudLocal"]);

                    lstLocal.Add(local);
                }
            }
            catch (Exception e)
            {
                lstLocal = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstLocal;
        }

        public List<ResultadoEncuestaEL> GetAllDatoMedioPublicitario()
        {
            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "sp_ListaMedioComunicaion";

            List<ResultadoEncuestaEL> lstResultadoEncuesta = new List<ResultadoEncuestaEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    ResultadoEncuestaEL ResultadoEncuesta = new ResultadoEncuestaEL();
                    ResultadoEncuesta.sumatoria = Funciones.CheckInt(dr["sumatoria"]);
                    ResultadoEncuesta.cantidad = Funciones.CheckInt(dr["cantidad"]);
                    ResultadoEncuesta.promedio = Funciones.CheckInt(dr["promedio"]);
                    ResultadoEncuesta.codMedioComunicacion = Funciones.CheckInt(dr["codMedioComunicacion"]);
                    ResultadoEncuesta.nombreMedioComunicacion = Funciones.CheckStr(dr["nombreMedioComunicacion"]);
                    ResultadoEncuesta.costoUnitarioMedioComunicacion = Funciones.CheckDecimal(dr["costoUnitarioMedioComunicacion"]);

                    lstResultadoEncuesta.Add(ResultadoEncuesta);
                }
            }
            catch (Exception e)
            {
                lstResultadoEncuesta = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstResultadoEncuesta;
        }

        public int RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@fechaPropuestapublicidad", DbType.DateTime,ParameterDirection.Input),
                new DAABRequest.Parameter("@precioPropuestaPublicidad", DbType.Decimal,ParameterDirection.Input),
                new DAABRequest.Parameter("@ObservacionPropuestaPublicidad", DbType.String,ParameterDirection.Input),                
                new DAABRequest.Parameter("@codPropuestapublicidad", DbType.Int32,ParameterDirection.Output)
            };

            arrParam[0].Value = PropuestaPublicidad.fechaPropuestapublicidad;
            arrParam[1].Value = PropuestaPublicidad.precioPropuestaPublicidad;
            arrParam[2].Value = PropuestaPublicidad.ObservacionPropuestaPublicidad;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "sp_RegistroPropuestaPublicidad";
            objRequest.Parameters.AddRange(arrParam);

            try
            {
                int result = objRequest.Factory.ExecuteNonQuery(ref objRequest);
                IDataParameter codPropuestapublicidad;
                codPropuestapublicidad = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 1];
                return Convert.ToInt32(codPropuestapublicidad.Value);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int RegistroDetallePropuesta(DetallePropuestaPublicidadEL DetallePropuestaPublicidad)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@codPropuestapublicidad", DbType.Int32,ParameterDirection.Input),
                new DAABRequest.Parameter("@codMedioComunicacion", DbType.Int32,ParameterDirection.Input),
                new DAABRequest.Parameter("@codLocal", DbType.Int32,ParameterDirection.Input),
                new DAABRequest.Parameter("@porcentaje", DbType.Int32,ParameterDirection.Input),
                new DAABRequest.Parameter("@promedio", DbType.Int32,ParameterDirection.Input),                
            };

            arrParam[0].Value = DetallePropuestaPublicidad.codPropuestapublicidad;
            arrParam[1].Value = DetallePropuestaPublicidad.codMedioComunicacion;
            arrParam[2].Value = DetallePropuestaPublicidad.codLocal;
            arrParam[3].Value = DetallePropuestaPublicidad.porcentaje;
            arrParam[4].Value = DetallePropuestaPublicidad.promedio;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "sp_RegistroDetallePropuestaPublicidad";
            objRequest.Parameters.AddRange(arrParam);

            try
            {
                int result = objRequest.Factory.ExecuteNonQuery(ref objRequest);
                return result;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
