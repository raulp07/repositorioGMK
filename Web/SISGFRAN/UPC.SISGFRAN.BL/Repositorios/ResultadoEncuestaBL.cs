using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Inherited;
using UPC.SISGFRAN.DAL.Repositorios;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class ResultadoEncuestaBL
    {

        public List<LocalEL> GetAllDatoMedio_X_Local() {
            return new PropuestaPublicitariaDA().GetAllDatoMedio_X_Local();
        }

        public List<ResultadoEncuestaEL> GetAllDatoMedioPublicitario() {
            return new PropuestaPublicitariaDA().GetAllDatoMedioPublicitario();
        }
        public int RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad)
        {
            PropuestaPublicitariaDA obj = new PropuestaPublicitariaDA();
            return obj.RegistroPropuesta(PropuestaPublicidad);
        }
        public int RegistroDetallePropuesta(DetallePropuestaPublicidadEL DetallePropuestaPublicidad)
        {
            return new PropuestaPublicitariaDA().RegistroDetallePropuesta(DetallePropuestaPublicidad);
        }
    }
}
