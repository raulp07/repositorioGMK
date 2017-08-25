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
    public class OpcionXPerfilDA
    {
        public List<OpcionXPerfilEL> ListMenu(OpcionXPerfilEL opcionPerfil)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@idapp", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idperfil", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = opcionPerfil.Aplicacion.Id;
            arrParam[1].Value = opcionPerfil.Perfil.Id;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_OpcionXPerfil";
            objRequest.Parameters.AddRange(arrParam);

            List<OpcionXPerfilEL> lstMenu = new List<OpcionXPerfilEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    OpcionXPerfilEL item = new OpcionXPerfilEL();
                    int? opcionId = Funciones.CheckInt(dr["OpcionId"]);
                    OpcionEL opcion = new OpcionDA().GetOpcionByID(opcionId);

                    item.Opcion = opcion;

                    AplicacionEL aplicacion = new AplicacionEL()
                    {
                        Id = Funciones.CheckInt(dr["AplicacionId"]),
                        Descripcion = Funciones.CheckStr(dr["Aplicacion"])
                    };

                    item.Aplicacion = aplicacion;

                    PerfilEL perfil = new PerfilEL()
                    {
                        Id = Funciones.CheckInt(dr["PerfilId"]),
                        Nombre = Funciones.CheckStr(dr["Perfil"])
                    };

                    item.Aplicacion = aplicacion;
                    item.Escritura = Convert.ToBoolean(dr["Escritura"]);

                    lstMenu.Add(item);
                }
            }
            catch (Exception e)
            {
                lstMenu = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstMenu;
        }
    }
}
