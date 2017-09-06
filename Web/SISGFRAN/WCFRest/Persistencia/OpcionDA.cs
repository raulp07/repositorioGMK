using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class OpcionDA
    {
        public OpcionEL GetOpcionByID(int? idOpcion) {

            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("USPS_Opcion", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@idOpcion", SqlDbType.Int).Value = idOpcion;
                    com.Parameters.Add("@idapp", SqlDbType.Int).Value = -1;
                    com.Parameters.Add("@estado", SqlDbType.Int).Value = -1;
                    com.Parameters.Add("@coderr", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@msgerr", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;


                    OpcionEL opcion = null;
                    IDataReader dr = null;

                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            opcion = new OpcionEL();
                            if (dataReader["Id"] != DBNull.Value) { opcion.Id = (int)dataReader["Id"]; }
                            AplicacionEL aplicacion = new AplicacionEL()
                            {
                                Id = (int)dataReader["AplicacionId"],
                                Descripcion = (string)dataReader["Aplicacion"]
                            };
                            opcion.Aplicacion = aplicacion;

                            if (dataReader["Nombre"] != DBNull.Value) { opcion.Nombre = (string)dataReader["Nombre"]; }
                            if (dataReader["PadreId"] != DBNull.Value) { opcion.PadreId = (int)dataReader["PadreId"]; }
                            if (dataReader["Nivel"] != DBNull.Value) { opcion.Nivel = (int)dataReader["Nivel"]; }
                            if (dataReader["NivelPadre"] != DBNull.Value) { opcion.NivelPadre = (int)dataReader["NivelPadre"]; }
                            if (dataReader["Imagen"] != DBNull.Value) { opcion.Imagen = (string)dataReader["Imagen"]; }
                            if (dataReader["Controler"] != DBNull.Value) { opcion.Controlador = (string)dataReader["Controler"]; }
                            if (dataReader["Accion"] != DBNull.Value) { opcion.Accion = (string)dataReader["Accion"]; }
                            if (dataReader["Orden"] != DBNull.Value) { opcion.Orden = (int)dataReader["Orden"]; }
                            if (dataReader["Observacion"] != DBNull.Value) { opcion.Observacion = (string)dataReader["Observacion"]; }
                            if (dataReader["Estado"] != DBNull.Value) { opcion.Estado = Convert.ToInt16(dataReader["Estado"]); }
                            
                        }
                    }
                    return opcion;
                }
            }

        }
    }
}