using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class AplicacionEL : BaseEL
    {
        public int Id { get; set; }
        public ParametroEL TipoAplicacion { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
    }
}
