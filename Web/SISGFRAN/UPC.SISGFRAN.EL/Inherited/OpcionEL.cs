using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class OpcionEL : BaseEL
    {
        public int Id { get; set; }
        public AplicacionEL Aplicacion { get; set; }
        public int Nivel { get; set; }
        public string Nombre { get; set; }
        public int PadreId { get; set; }
        public int NivelPadre { get; set; }
        public string Imagen { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public int Orden { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }
    }
}
