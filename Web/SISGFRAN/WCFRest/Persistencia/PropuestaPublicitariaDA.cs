using WCFRest.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
namespace WCFRest.Persistencia
{
    public class PropuestaPublicitariaDA
    {


        public List<LocalEL> GetAllDatoMedio_X_Local()
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("sp_Lista_Medios_X_Locales", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    List<LocalEL> lstLocal = new List<LocalEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            LocalEL obj = new LocalEL();
                            if (dataReader["puntajeCaracteristicaCombo"] != DBNull.Value) { obj.puntajeCaracteristicaCombo = (int)dataReader["puntajeCaracteristicaCombo"]; }
                            if (dataReader["Porcentaje"] != DBNull.Value) { obj.Porcentaje = (int)dataReader["Porcentaje"]; }
                            if (dataReader["codEncuesta"] != DBNull.Value) { obj.codEncuesta = (int)dataReader["codEncuesta"]; }
                            if (dataReader["nombreEncuesta"] != DBNull.Value) { obj.nombreEncuesta = (string)dataReader["nombreEncuesta"]; }
                            if (dataReader["codMedioComunicacion"] != DBNull.Value) { obj.codMedioComunicacion = (int)dataReader["codMedioComunicacion"]; }
                            if (dataReader["nombreMedioComunicacion"] != DBNull.Value) { obj.nombreMedioComunicacion = (string)dataReader["nombreMedioComunicacion"]; }
                            if (dataReader["costoUnitarioMedioComunicacion"] != DBNull.Value) { obj.costoUnitarioMedioComunicacion = (decimal)dataReader["costoUnitarioMedioComunicacion"]; }
                            if (dataReader["codLocal"] != DBNull.Value) { obj.codLocal = (int)dataReader["codLocal"]; }
                            if (dataReader["nombreLocal"] != DBNull.Value) { obj.nombreLocal = (string)dataReader["nombreLocal"]; }
                            if (dataReader["latitudLocal"] != DBNull.Value) { obj.latitudLocal = (string)dataReader["latitudLocal"]; }
                            if (dataReader["longitudLocal"] != DBNull.Value) { obj.longitudLocal = (string)dataReader["longitudLocal"]; }
                            lstLocal.Add(obj);
                        }
                        return lstLocal;
                    }
                }
            }
        }

        public List<ResultadoEncuestaEL> GetAllDatoMedioPublicitario()
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("sp_ListaMedioComunicaion", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    List<ResultadoEncuestaEL> lstResultadoEncuesta = new List<ResultadoEncuestaEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ResultadoEncuestaEL obj = new ResultadoEncuestaEL();
                            if (dataReader["sumatoria"] != DBNull.Value) { obj.sumatoria = (int)dataReader["sumatoria"]; }
                            if (dataReader["cantidad"] != DBNull.Value) { obj.cantidad = (int)dataReader["cantidad"]; }
                            if (dataReader["promedio"] != DBNull.Value) { obj.promedio = (int)dataReader["promedio"]; }
                            if (dataReader["codMedioComunicacion"] != DBNull.Value) { obj.codMedioComunicacion = (int)dataReader["codMedioComunicacion"]; }
                            if (dataReader["nombreMedioComunicacion"] != DBNull.Value) { obj.nombreMedioComunicacion = (string)dataReader["nombreMedioComunicacion"]; }
                            if (dataReader["costoUnitarioMedioComunicacion"] != DBNull.Value) { obj.costoUnitarioMedioComunicacion = (decimal)dataReader["costoUnitarioMedioComunicacion"]; }
                            lstResultadoEncuesta.Add(obj);
                        }
                        return lstResultadoEncuesta;
                    }
                }
            }
        }

        public int RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("sp_RegistroPropuestaPublicidad", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@fechaPropuestapublicidad", SqlDbType.DateTime).Value = PropuestaPublicidad.fechaPropuestapublicidad;
                    com.Parameters.Add("@precioPropuestaPublicidad", SqlDbType.Decimal).Value = PropuestaPublicidad.precioPropuestaPublicidad;
                    com.Parameters.Add("@ObservacionPropuestaPublicidad", SqlDbType.VarChar).Value = PropuestaPublicidad.ObservacionPropuestaPublicidad;
                    com.Parameters.Add("@codPropuestapublicidad", SqlDbType.Int).Direction = ParameterDirection.Output;

                    com.ExecuteNonQuery();
                    int Id_Estrategia = Convert.ToInt32(com.Parameters["@codPropuestapublicidad"].Value.ToString());
                    return Id_Estrategia;

                }
            }
        }

        public int RegistroDetallePropuesta(DetallePropuestaPublicidadEL DetallePropuestaPublicidad)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("sp_RegistroDetallePropuestaPublicidad", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@codPropuestapublicidad", SqlDbType.Int).Value = DetallePropuestaPublicidad.codPropuestapublicidad;
                    com.Parameters.Add("@codMedioComunicacion", SqlDbType.Int).Value = DetallePropuestaPublicidad.codMedioComunicacion;
                    com.Parameters.Add("@codLocal", SqlDbType.Int).Value = DetallePropuestaPublicidad.codLocal;
                    com.Parameters.Add("@porcentaje", SqlDbType.Int).Value = DetallePropuestaPublicidad.porcentaje;
                    com.Parameters.Add("@promedio", SqlDbType.Int).Value = DetallePropuestaPublicidad.promedio;
                    return com.ExecuteNonQuery();
                }
            }
        }

    }
}