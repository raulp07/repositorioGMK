using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Inherited;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class ResultadoEncuestaBL
    {

        private static ResultadoEncuestaBL resultadoEncuesta;

        private ResultadoEncuestaBL() { }
        public static ResultadoEncuestaBL ResultadoEncuesta {
            get {
                if (resultadoEncuesta == null)
                {
                    resultadoEncuesta = new ResultadoEncuestaBL();
                }
                return resultadoEncuesta;
            }
        }

        REST conecRest = new REST();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public List<LocalEL> GetAllDatoMedio_X_Local()
        {
            List<LocalEL> alumnoObtenido = js.Deserialize<List<LocalEL>>(conecRest.ConectREST("local", "GET"));
            return alumnoObtenido;
        }

        public List<ResultadoEncuestaEL> GetAllDatoMedioPublicitario()
        {
            List<ResultadoEncuestaEL> alumnoObtenido = js.Deserialize<List<ResultadoEncuestaEL>>(conecRest.ConectREST("encuesta", "GET"));
            return alumnoObtenido;
        }


        public int RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad)
        {
            string postdata = js.Serialize(PropuestaPublicidad);
            return js.Deserialize<int>(conecRest.ConectREST("Propuesta", "POST", postdata));
        }


        public int RegistroDetallePropuesta(DetallePropuestaPublicidadEL DetallePropuestaPublicidad)
        {
            string postdata = js.Serialize(DetallePropuestaPublicidad);
            return js.Deserialize<int>(conecRest.ConectREST("DetallePropuesta", "POST", postdata));
        }

    }
}
