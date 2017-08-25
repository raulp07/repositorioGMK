using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class EncuestaEL : ResultadoEncuestaEL
    {
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

        /// Gets or Sets nombreEncuesta
        ///

        public string nombreEncuesta
        {
            get { return _nombreEncuesta; }
            set { _nombreEncuesta = value; }
        }
        private string _nombreEncuesta;

        ///

        /// Gets or Sets fechaInicioEncuesta
        ///

        public DateTime fechaInicioEncuesta
        {
            get { return _fechaInicioEncuesta; }
            set { _fechaInicioEncuesta = value; }
        }
        private DateTime _fechaInicioEncuesta;

        ///

        /// Gets or Sets fechaFinEncuesta
        ///

        public DateTime fechaFinEncuesta
        {
            get { return _fechaFinEncuesta; }
            set { _fechaFinEncuesta = value; }
        }
        private DateTime _fechaFinEncuesta;

        ///

        /// Gets or Sets cantidadClientesEncuestados
        ///

        public int cantidadClientesEncuestados
        {
            get { return _cantidadClientesEncuestados; }
            set { _cantidadClientesEncuestados = value; }
        }
        private int _cantidadClientesEncuestados;

        ///

        /// Gets or Sets codLocal
        ///

        public int codLocal
        {
            get { return _codLocal; }
            set { _codLocal = value; }
        }
        private int _codLocal;
    }
}
