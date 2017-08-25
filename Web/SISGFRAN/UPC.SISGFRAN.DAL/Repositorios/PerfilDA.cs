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

namespace UPC.SISGFRAN.DAL.Repositorios
{
    public class PerfilDA
    {

        public List<PerfilEL> GetPerfil(PerfilEL perfil)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@idperfil", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idapp", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@nombres", DbType.String ,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = perfil.Id;
            arrParam[1].Value = perfil.Aplicacion.Id;
            arrParam[2].Value = perfil.Nombre;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Perfil";
            objRequest.Parameters.AddRange(arrParam);

            List<PerfilEL> lstPerfil = new List<PerfilEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    PerfilEL item = new PerfilEL();
                    item.Id = Funciones.CheckInt(dr["Id"]);
                    item.Nombre = Funciones.CheckStr(dr["Nombre"]);
                    item.Descripcion = Funciones.CheckStr(dr["Descripcion"]);

                    AplicacionEL aplicacion = new AplicacionEL() 
                    {
                        Id = Funciones.CheckInt(dr["AplicacionId"]),
                        Descripcion = Funciones.CheckStr(dr["Aplicacion"])
                    };
                    
                    item.Aplicacion = aplicacion;
                    lstPerfil.Add(item);
                }
            }
            catch (Exception e)
            {
                lstPerfil = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstPerfil;
        }

        public PerfilEL GetPerfilByID(int? idPerfil)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@idperfil", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idapp", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@nombres", DbType.String ,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = idPerfil;
            arrParam[1].Value = Constantes.Filtros.Todos;
            arrParam[2].Value = string.Empty;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Perfil";
            objRequest.Parameters.AddRange(arrParam);

            PerfilEL perfil = null;
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    perfil = new PerfilEL();
                    perfil.Id = Funciones.CheckInt(dr["Id"]);
                    perfil.Nombre = Funciones.CheckStr(dr["Nombre"]);
                    perfil.Descripcion = Funciones.CheckStr(dr["Descripcion"]);

                    AplicacionEL aplicacion = new AplicacionEL()
                    {
                        Id = Funciones.CheckInt(dr["AplicacionId"]),
                        Descripcion = Funciones.CheckStr(dr["Aplicacion"])
                    };

                    perfil.Aplicacion = aplicacion;
                }
            }
            catch (Exception e)
            {
                perfil = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return perfil;
        }
    }
}