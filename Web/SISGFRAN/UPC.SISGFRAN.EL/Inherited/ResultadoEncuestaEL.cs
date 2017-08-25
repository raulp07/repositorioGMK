using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class ResultadoEncuestaEL : MedioComunicacionEL
    {
        ///

        /// Gets or Sets codResultadoEncuesta
        ///

        public int codResultadoEncuesta
        {
            get { return _codResultadoEncuesta; }
            set { _codResultadoEncuesta = value; }
        }
        private int _codResultadoEncuesta;

        ///

        /// Gets or Sets puntajeCaracteristicaCombo
        ///

        public int puntajeCaracteristicaCombo
        {
            get { return _puntajeCaracteristicaCombo; }
            set { _puntajeCaracteristicaCombo = value; }
        }
        private int _puntajeCaracteristicaCombo;

        ///

        /// Gets or Sets codEncuesta
        ///

        public int codEncuesta
        {
            get { return _codEncuesta; }
            set { _codEncuesta = value; }
        }
        private int _codEncuesta;

        ///

        /// Gets or Sets codMedioComunicacion
        ///

        public int codMedioComunicacion
        {
            get { return _codMedioComunicacion; }
            set { _codMedioComunicacion = value; }
        }
        private int _codMedioComunicacion;

        public int Porcentaje { get; set; }

        public int sumatoria { get; set; }
        public int cantidad { get; set; }
        public int promedio { get; set; }
    }
}
