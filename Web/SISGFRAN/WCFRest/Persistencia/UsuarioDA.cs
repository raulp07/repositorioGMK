using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class UsuarioDA
    {
        public UsuarioEL Login(UsuarioEL usuario)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("USPS_Login", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario.CtaUsuario;
                    com.Parameters.Add("@clave", SqlDbType.VarChar).Value = usuario.Password;
                    com.Parameters.Add("@idAplicativo", SqlDbType.Int).Value = usuario.Perfil.Aplicacion.Id;
                    com.Parameters.Add("@userID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@coderr", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@msgerr", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;

                    com.ExecuteNonQuery();
                    UsuarioEL usuarioLogueado = null;
                    int id = Convert.ToInt32(com.Parameters["@userID"].Value.ToString());
                    int result = Convert.ToInt32(com.Parameters["@coderr"].Value.ToString());
                    string message = Convert.ToString(com.Parameters["@msgerr"].Value.ToString());

                    usuarioLogueado = usuario;
                    usuarioLogueado.Id = id;
                    usuarioLogueado.CodeMessage = result;
                    usuarioLogueado.MessageErr = message;
                    return usuarioLogueado;
                }
            }
        }

        public List<UsuarioEL> GetUsuarios(UsuarioEL usuario)
        {

            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("USPS_Usuario", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@idUsuario", SqlDbType.Int).Value = usuario.Id;
                    com.Parameters.Add("@nombres", SqlDbType.VarChar).Value = usuario.Nombres;
                    //com.Parameters.Add("@idPerfil", SqlDbType.Int).Value = usuario.Perfil.Id;
                    com.Parameters.Add("@estado", SqlDbType.Int).Value = usuario.Estado;
                    com.Parameters.Add("@coderr", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@msgerr", SqlDbType.VarChar).Direction = ParameterDirection.Output;

                    List<UsuarioEL> lstUsuarios = new List<UsuarioEL>();
                    using (IDataReader dataReader = com.ExecuteReader()) {

                        while (dataReader.Read())
                        {
                            UsuarioEL item = new UsuarioEL();
                            if (dataReader["Id"] != DBNull.Value) { item.Id = (int)dataReader["Id"]; }
                            if (dataReader["CtaUsuario"] != DBNull.Value) { item.CtaUsuario = (string)dataReader["CtaUsuario"]; }

                            PerfilEL perfil = new PerfilEL()
                            {
                                Id = dataReader["PerfilId"] != DBNull.Value ? (int)dataReader["PerfilId"]:0,
                                Nombre = dataReader["perfil"] != DBNull.Value ? (string)dataReader["perfil"]:""
                            };
                            //item.Perfil = perfil;
                            if (dataReader["Apellidos"] != DBNull.Value) { item.Apellidos = (string)dataReader["Apellidos"]; }
                            if (dataReader["Nombres"] != DBNull.Value) { item.Nombres = (string)dataReader["Nombres"]; }
                            if (dataReader["Cargo"] != DBNull.Value) { item.Cargo = (string)dataReader["Cargo"]; }
                            if (dataReader["Email"] != DBNull.Value) { item.Email = (string)dataReader["Email"]; }
                            if (dataReader["telefono"] != DBNull.Value) { item.Telefono = (string)dataReader["telefono"]; }
                            if (dataReader["CambiarContrasenia"] != DBNull.Value) { item.CambiarContrasenia = (bool)dataReader["CambiarContrasenia"]; }
                            if (dataReader["FechaVencimientoCta"] != DBNull.Value) { item.FechaVenceCuenta = (DateTime)dataReader["FechaVencimientoCta"]; }
                            if (dataReader["FechaVencimiento"] != DBNull.Value) { item.FechaVencePass = (DateTime)dataReader["FechaVencimiento"]; }
                            if (dataReader["Estado"] != DBNull.Value) { item.Estado = (int)dataReader["Estado"]; }
                            lstUsuarios.Add(item);
                        }
                    }

                    return lstUsuarios;
                }
            }
        }

        public UsuarioEL GetUsuarioById(int? idUsuario)
        {


            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("USPS_Usuario", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
                    com.Parameters.Add("@nombres", SqlDbType.VarChar).Value = string.Empty;
                    com.Parameters.Add("@idPerfil", SqlDbType.Int).Value = -1;
                    com.Parameters.Add("@estado", SqlDbType.Int).Value = -1;
                    com.Parameters.Add("@coderr", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@msgerr", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;

                    UsuarioEL usuario = new UsuarioEL();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataReader["Id"] != DBNull.Value) { usuario.Id = (int)dataReader["Id"]; }
                            if (dataReader["CtaUsuario"] != DBNull.Value) { usuario.CtaUsuario = (string)dataReader["CtaUsuario"]; }
                            
                            PerfilEL perfil = new PerfilEL()
                            {
                                Id = dataReader["PerfilId"] != DBNull.Value ? (int)dataReader["PerfilId"] : 0,
                                Nombre = dataReader["perfil"] != DBNull.Value ? (string)dataReader["perfil"] : ""
                            };
                            usuario.Perfil = perfil;
                            if (dataReader["Nombres"] != DBNull.Value) { usuario.Nombres = (string)dataReader["Nombres"]; }
                            if (dataReader["Apellidos"] != DBNull.Value) { usuario.Apellidos = (string)dataReader["Apellidos"]; }
                            if (dataReader["Cargo"] != DBNull.Value) { usuario.Cargo = (string)dataReader["Cargo"]; }
                            if (dataReader["Email"] != DBNull.Value) { usuario.Email = (string)dataReader["Email"]; }
                            if (dataReader["telefono"] != DBNull.Value) { usuario.Telefono = (string)dataReader["telefono"]; }
                            if (dataReader["CambiarContrasenia"] != DBNull.Value) { usuario.CambiarContrasenia = (bool)dataReader["CambiarContrasenia"]; }
                            if (dataReader["FechaVencimientoCta"] != DBNull.Value) { usuario.FechaVenceCuenta = (DateTime)dataReader["FechaVencimientoCta"]; }
                            if (dataReader["FechaVencimiento"] != DBNull.Value) { usuario.FechaVencePass = (DateTime)dataReader["FechaVencimiento"]; }
                            if (dataReader["Estado"] != DBNull.Value) { usuario.Estado = Convert.ToInt32(dataReader["Estado"]); }

                        }
                    }
                    return usuario;
                }
            }
        }

    }
}