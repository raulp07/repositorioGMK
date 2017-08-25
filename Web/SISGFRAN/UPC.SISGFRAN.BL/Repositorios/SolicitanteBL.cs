using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class SolicitanteBL
    {
        public List<SolicitudEL> GetSolicitantes()
        {
            return new SolicitanteDA().GetSolicitantes();
        }

        public SolicitudEL GetSolicitante(int id)
        {
            return new SolicitanteDA().GetSolicitante(id);
        }

        public SolicitanteEL SetResultadoEvaluacion(int idSolicitante, bool fueRechazado = false)
        {

            Random rdn = new Random();
            int probabilidad = rdn.Next(100);

            if (idSolicitante == 3)
                fueRechazado = true;

            var solicitante = new SolicitanteEL()
            {
                FueAprobado = (fueRechazado ? false : (probabilidad <= 55)) 
            };

            if (idSolicitante == 5)
                solicitante.FueAprobado = true;

            SolicitanteDA dao = new SolicitanteDA();
            dao.UpdateApprovalStatus(idSolicitante, solicitante.FueAprobado);

            return solicitante;
        }
    }
}
