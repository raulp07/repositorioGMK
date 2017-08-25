using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class MedioComunicacionEL
    {
        ///

        /// Gets or Sets codMedioComunicacion
        ///

        public int codMedioComunicacion
        {
            get { return _codMedioComunicacion; }
            set { _codMedioComunicacion = value; }
        }
        private int _codMedioComunicacion;

        ///

        /// Gets or Sets nombreMedioComunicacion
        ///

        public string nombreMedioComunicacion
        {
            get { return _nombreMedioComunicacion; }
            set { _nombreMedioComunicacion = value; }
        }
        private string _nombreMedioComunicacion;

        ///

        /// Gets or Sets costoUnitarioMedioComunicacion
        ///

        public decimal costoUnitarioMedioComunicacion
        {
            get { return _costoUnitarioMedioComunicacion; }
            set { _costoUnitarioMedioComunicacion = value; }
        }
        private decimal _costoUnitarioMedioComunicacion;
    }
}
