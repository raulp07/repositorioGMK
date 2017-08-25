using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class PropuestaPublicidadEL
    {
        ///

        /// Gets or Sets codPropuestapublicidad
        ///

        public int codPropuestapublicidad
        {
            get { return _codPropuestapublicidad; }
            set { _codPropuestapublicidad = value; }
        }
        private int _codPropuestapublicidad;

        ///

        /// Gets or Sets fechaPropuestapublicidad
        ///

        public DateTime fechaPropuestapublicidad
        {
            get { return _fechaPropuestapublicidad; }
            set { _fechaPropuestapublicidad = value; }
        }
        private DateTime _fechaPropuestapublicidad;

        ///

        /// Gets or Sets precioPropuestaPublicidad
        ///

        public decimal precioPropuestaPublicidad
        {
            get { return _precioPropuestaPublicidad; }
            set { _precioPropuestaPublicidad = value; }
        }
        private decimal _precioPropuestaPublicidad;


        public string ObservacionPropuestaPublicidad { get; set; }
    }
}
