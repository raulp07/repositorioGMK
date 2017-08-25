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
    public class UsuarioDA
    {
        public UsuarioEL Login(UsuarioEL usuario)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@usuario", DbType.String, 50, ParameterDirection.Input),
				new DAABRequest.Parameter("@clave", DbType.String, 50 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@idAplicativo", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@userID", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = usuario.CtaUsuario;
            arrParam[1].Value = usuario.Password;
            arrParam[2].Value = usuario.Perfil.Aplicacion.Id;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Login";
            objRequest.Parameters.AddRange(arrParam);

            UsuarioEL usuarioLogueado = null;
            int result; string message; int id;
            try
            {
                objRequest.Factory.ExecuteNonQuery(ref objRequest);
                IDataParameter pCodMsg, pMensaje, pUsuarioID;
                pUsuarioID = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 3];
                pCodMsg = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 2];
                pMensaje = (IDataParameter)objRequest.Parameters[objRequest.Parameters.Count - 1];
                Int32.TryParse(pCodMsg.Value.ToString(), out result);
                Int32.TryParse(pUsuarioID.Value.ToString(), out id);
                message = pMensaje.Value.ToString();

                usuarioLogueado = usuario;
                usuarioLogueado.Id = id;
                usuarioLogueado.CodeMessage = result;
                usuarioLogueado.MessageErr = message;
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
            return usuarioLogueado;
        }

        public List<UsuarioEL> GetUsuarios(UsuarioEL usuario)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@idUsuario", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@nombres", DbType.String ,200, ParameterDirection.Input),
                new DAABRequest.Parameter("@idPerfil", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@estado", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = usuario.Id;
            arrParam[1].Value = usuario.Nombres;
            arrParam[2].Value = usuario.Perfil.Id;
            arrParam[3].Value = usuario.Estado;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Usuario";
            objRequest.Parameters.AddRange(arrParam);

            List<UsuarioEL> lstUsuarios = new List<UsuarioEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    UsuarioEL item = new UsuarioEL();
                    item.Id = Funciones.CheckInt(dr["Id"]);
                    item.CtaUsuario = Funciones.CheckStr(dr["CtaUsuario"]);

                    PerfilEL perfil = new PerfilEL()
                    {
                        Id = Funciones.CheckInt(dr["PerfilId"]),
                        Nombre = Funciones.CheckStr(dr["perfil"])
                    };
                    item.Perfil = perfil;
                    item.Apellidos = Funciones.CheckStr(dr["Apellidos"]);
                    item.Nombres = Funciones.CheckStr(dr["Nombres"]);
                    item.Cargo = Funciones.CheckStr(dr["Cargo"]);
                    item.Email = Funciones.CheckStr(dr["Email"]);
                    item.Telefono = Funciones.CheckStr(dr["telefono"]);
                    item.CambiarContrasenia = Convert.ToBoolean(dr["CambiarContrasenia"]);
                    item.FechaVenceCuenta = Funciones.CheckDate(dr["FechaVencimientoCta"]);
                    item.FechaVencePass = Funciones.CheckDate(dr["FechaVencimiento"]);
                    item.Estado = Funciones.CheckInt(dr["Estado"]);
                    lstUsuarios.Add(item);
                }
            }
            catch (Exception e)
            {
                lstUsuarios = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstUsuarios;
        }

        public UsuarioEL GetUsuarioById(int? idUsuario)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@idUsuario", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@nombres", DbType.String ,ParameterDirection.Input),
                new DAABRequest.Parameter("@idPerfil", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@estado", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = idUsuario;
            arrParam[1].Value = string.Empty;
            arrParam[2].Value = Constantes.Filtros.Todos;
            arrParam[3].Value = Constantes.Estado.Todos;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Usuario";
            objRequest.Parameters.AddRange(arrParam);

            UsuarioEL usuario = null;
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    usuario = new UsuarioEL();
                    usuario.Id = Funciones.CheckInt(dr["Id"]);
                    usuario.CtaUsuario = Funciones.CheckStr(dr["CtaUsuario"]);

                    PerfilEL perfil = new PerfilEL()
                    {
                        Id = Funciones.CheckInt(dr["PerfilId"]),
                        Nombre = Funciones.CheckStr(dr["perfil"])
                    };
                    usuario.Perfil = perfil;
                    usuario.Nombres = Funciones.CheckStr(dr["Nombres"]);
                    usuario.Apellidos = Funciones.CheckStr(dr["Apellidos"]);
                    usuario.Cargo = Funciones.CheckStr(dr["Cargo"]);
                    usuario.Email = Funciones.CheckStr(dr["Email"]);
                    usuario.Telefono = Funciones.CheckStr(dr["telefono"]);
                    usuario.CambiarContrasenia = Convert.ToBoolean(dr["CambiarContrasenia"]);
                    usuario.FechaVenceCuenta = Funciones.CheckDate(dr["FechaVencimientoCta"]);
                    usuario.FechaVencePass = Funciones.CheckDate(dr["FechaVencimiento"]);
                    usuario.Estado = Funciones.CheckInt(dr["Estado"]);
                }
            }
            catch (Exception e)
            {
                usuario = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return usuario;
        }

    }
}
