using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class SolicitudBL
    {
        public List<SolicitudEL> GetSolicitudesPendientes(string desc)
        {
            return new SolicitudDA().GetSolicitudesPendientes(desc);
        }

        public SolicitudEL GetResultadoEvaluacion(int IdSolicitud)
        {
            return new SolicitudDA().GetResultadoEvaluacion(IdSolicitud);
        }

        public SolicitudEL Actualizar(SolicitudEL solicitud)
        {
            return new SolicitudDA().Actualizar(solicitud);
        }

        public SolicitudEL GetSolicitudById(int solicitudId) 
        {
            return new SolicitudDA().GetSolicitudById(solicitudId);
        }

        public SolicitudEL RegistrarReporteEvaluacion(SolicitudEL solicitud)
        {
            return new SolicitudDA().RegistrarReporteEvaluacion(solicitud);
        }

    }
}
