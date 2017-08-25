using DAAB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Base;
using UPC.SISGFRAN.EL.Comunes;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.DAL.Repositorios
{
    public class OpcionDA
    {
        public List<OpcionEL> GetOpciones(OpcionEL opcion)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@idOpcion", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idapp", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@estado", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = opcion.Id;
            arrParam[1].Value = opcion.Aplicacion.Id;
            arrParam[2].Value = opcion.Nombre;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Opcion";
            objRequest.Parameters.AddRange(arrParam);

            List<OpcionEL> lstOpciones = new List<OpcionEL>();
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    OpcionEL item = new OpcionEL();
                    item.Id = Funciones.CheckInt(dr["Id"]);

                    AplicacionEL aplicacion = new AplicacionEL()
                    {
                        Id = Funciones.CheckInt(dr["AplicacionId"]),
                        Descripcion = Funciones.CheckStr(dr["Aplicacion"])
                    };

                    item.Aplicacion = aplicacion;
                    item.Nombre = Funciones.CheckStr(dr["Nombre"]);
                    item.PadreId = Funciones.CheckInt(dr["PadreId"]);
                    item.Nivel = Funciones.CheckInt(dr["Nivel"]);
                    item.NivelPadre = Funciones.CheckInt(dr["NivelPadre"]);
                    item.Imagen = Funciones.CheckStr(dr["Imagen"]);
                    item.Controlador = Funciones.CheckStr(dr["Controler"]);
                    item.Accion = Funciones.CheckStr(dr["Accion"]);
                    item.Orden = Funciones.CheckInt(dr["Orden"]);
                    item.Observacion = Funciones.CheckStr(dr["Observacion"]);
                    item.Estado = Funciones.CheckInt(dr["Estado"]);
                    lstOpciones.Add(item);
                }
            }
            catch (Exception e)
            {
                lstOpciones = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return lstOpciones;
        }

        public OpcionEL GetOpcionByID(int? idOpcion)
        {
            DAABRequest.Parameter[] arrParam = {
                new DAABRequest.Parameter("@idOpcion", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@idapp", DbType.Int32, ParameterDirection.Input),
                new DAABRequest.Parameter("@estado", DbType.Int32 ,ParameterDirection.Input),
                new DAABRequest.Parameter("@coderr", DbType.Int32,ParameterDirection.Output),
                new DAABRequest.Parameter("@msgerr", DbType.String, 1000,ParameterDirection.Output)
            };

            arrParam[0].Value = idOpcion;
            arrParam[1].Value = Constantes.Filtros.Todos;
            arrParam[2].Value = Constantes.Estado.Todos;

            configPARDOSDB objPardosDb = new configPARDOSDB();
            DAABRequest objRequest = objPardosDb.CreaRequest();
            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = "USPS_Opcion";
            objRequest.Parameters.AddRange(arrParam);

            OpcionEL opcion = null;
            IDataReader dr = null;
            try
            {
                dr = objRequest.Factory.ExecuteReader(ref objRequest).ReturnDataReader;
                while (dr.Read())
                {
                    opcion = new OpcionEL();
                    opcion.Id = Funciones.CheckInt(dr["Id"]);

                    AplicacionEL aplicacion = new AplicacionEL()
                    {
                        Id = Funciones.CheckInt(dr["AplicacionId"]),
                        Descripcion = Funciones.CheckStr(dr["Aplicacion"])
                    };

                    opcion.Aplicacion = aplicacion;
                    opcion.Nombre = Funciones.CheckStr(dr["Nombre"]);
                    opcion.PadreId = Funciones.CheckInt(dr["PadreId"]);
                    opcion.Nivel = Funciones.CheckInt(dr["Nivel"]);
                    opcion.NivelPadre = Funciones.CheckInt(dr["NivelPadre"]);
                    opcion.Imagen = Funciones.CheckStr(dr["Imagen"]);
                    opcion.Controlador = Funciones.CheckStr(dr["Controler"]);
                    opcion.Accion = Funciones.CheckStr(dr["Accion"]);
                    opcion.Orden = Funciones.CheckInt(dr["Orden"]);
                    opcion.Observacion = Funciones.CheckStr(dr["Observacion"]);
                    opcion.Estado = Funciones.CheckInt(dr["Estado"]);
                }
            }
            catch (Exception e)
            {
                opcion = null;
                throw e;
            }
            finally
            {
                if (dr != null && dr.IsClosed == false) dr.Close();
                objRequest.Parameters.Clear();
                objRequest.Factory.Dispose();
            }
            return opcion;
        }
        
    }
}
