using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class DetallePropuestaPublicidadEL : PropuestaPublicidadEL
    {
        ///

        /// Gets or Sets codDetallePropuestapublicidad
        ///
        
        public int codDetallePropuestapublicidad
        {
            get { return _codDetallePropuestapublicidad; }
            set { _codDetallePropuestapublicidad = value; }
        }
        private int _codDetallePropuestapublicidad;

        ///

        ///

        /// Gets or Sets codMedioComunicacion
        ///

        public int codMedioComunicacion
        {
            get { return _codMedioComunicacion; }
            set { _codMedioComunicacion = value; }
        }
        private int _codMedioComunicacion;

        /// Gets or Sets codLocal
        ///

        public int codLocal
        {
            get { return _codLocal; }
            set { _codLocal = value; }
        }
        private int _codLocal;

        ///

        /// Gets or Sets porcentaje
        ///

        public int porcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }
        private int _porcentaje;

        ///

        /// Gets or Sets promedio
        ///

        public int promedio
        {
            get { return _promedio; }
            set { _promedio = value; }
        }
        private int _promedio;
    }
}
