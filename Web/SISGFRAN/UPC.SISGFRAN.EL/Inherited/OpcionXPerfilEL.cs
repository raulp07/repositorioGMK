using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class OpcionXPerfilEL : BaseEL
    {
        public AplicacionEL Aplicacion { get; set; }
        public PerfilEL Perfil { get; set; }
        public OpcionEL Opcion { get; set; }
        public bool Escritura { get; set; }
        public ICollection<OpcionXPerfilEL> Hijos { get; set; }
    }
}
