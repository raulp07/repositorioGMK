using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class ReporteEvaluacionEL : BaseEL
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string ResultadoEjercicio { get; set; }
        public string ErroresEncontrados { get; set; }

        public string ImageUrl { get; set; }
        public bool Resultado { get; set; }
    }
}
