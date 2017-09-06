using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class OpcionXPerfilDA
    {
        public List<OpcionXPerfilEL> ListMenu(OpcionXPerfilEL opcionPerfil)
        {

            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("USPS_OpcionXPerfil", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@idapp", SqlDbType.Int).Value = opcionPerfil.Aplicacion.Id;
                    com.Parameters.Add("@idperfil", SqlDbType.Int).Value = opcionPerfil.Perfil.Id;
                    com.Parameters.Add("@coderr", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@msgerr", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    List<OpcionXPerfilEL> lstMenu = new List<OpcionXPerfilEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {

                        while (dataReader.Read())
                        {
                            OpcionXPerfilEL item = new OpcionXPerfilEL();
                            OpcionEL opcion = new OpcionDA().GetOpcionByID((int)dataReader["OpcionId"]);
                            item.Opcion = opcion;

                            AplicacionEL aplicacion = new AplicacionEL()
                            {
                                Id = dataReader["AplicacionId"] != DBNull.Value ? (int)dataReader["AplicacionId"] : 0,
                                Descripcion = dataReader["Aplicacion"] != DBNull.Value ? (string)dataReader["Aplicacion"] : ""
                            };
                            item.Aplicacion = aplicacion;

                            PerfilEL perfil = new PerfilEL()
                            {
                                Id = dataReader["PerfilId"] != DBNull.Value ? (int)dataReader["PerfilId"] : 0,
                                Nombre = dataReader["Perfil"] != DBNull.Value ? (string)dataReader["Perfil"] : ""
                            };
                            item.Aplicacion = aplicacion;
                            if (dataReader["Escritura"] != DBNull.Value) { item.Escritura = (bool)dataReader["Escritura"]; }

                            lstMenu.Add(item);
                        }
                    }

                    return lstMenu;
                }
            }

        }

    }
}