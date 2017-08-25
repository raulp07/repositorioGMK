using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class EntrevistaEL : BaseEL
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public UsuarioEL Entrevistador { get; set; }
        public ParametroEL Lugar { get; set; }
        public string Observacion { get; set; }
        public ParametroEL Estado { get; set; }
    }
}
