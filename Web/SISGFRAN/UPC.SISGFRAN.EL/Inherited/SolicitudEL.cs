using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class SolicitudEL : BaseEL
    {
        public int Id { get; set; }
        public SolicitanteEL Solicitante { get; set; }
        public string NumSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string CiudadInteres { get; set; }
        public string UbigeoCiudadInteres { get; set; }
        public decimal MontoCapital { get; set; }
        public string FuenteFinanciamiento { get; set; }
        public DateTime FechaInicioOperacion { get; set; }
        public Int16 LocalDisponible { get; set; }
        public ParametroEL CondicionLocal { get; set; }
        public string UbigeoLocal { get; set; }
        public string DireccionLocal { get; set; }
        public ParametroEL TipoUbicacion { get; set; }
        public string ReferenciaComercial { get; set; }
        public string ReferenciaBancaria { get; set; }
        public ParametroEL Estado { get; set; }

        public string NumeroDocumento { get; set; }

        public EntrevistaEL Entrevista { get; set; }
        public ReporteEvaluacionEL ReporteEvaluacion { get; set; }

        public ListaPaginada<SolicitudEL> ListaSolicitudes { get; set; }
    }
}
