using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.WS.Repositorios
{
    public class EvaluadorWS
    {

        public ReporteEvaluacionEL EvaluarSolicitud(SolicitudEL solicitud)
        {
            ReporteEvaluacionEL oResultado = new ReporteEvaluacionEL();
            bool bOk = false;
            if (solicitud.Id >4)
            {
                bOk = false;
            }
            else
            {
                bOk = true;
            }

            oResultado.ResultadoEjercicio = "Test";
            oResultado.ErroresEncontrados = "";
            oResultado.Resultado = bOk;

            return oResultado;
        }

    }
}
