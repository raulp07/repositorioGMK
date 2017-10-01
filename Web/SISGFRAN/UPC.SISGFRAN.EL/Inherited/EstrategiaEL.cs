using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class EstrategiaEL
    {
        ///

        /// Gets or Sets codEstrategia
        ///

        public int codEstrategia
        {
            get { return _codEstrategia; }
            set { _codEstrategia = value; }
        }
        private int _codEstrategia;

        ///

        /// Gets or Sets descripcionEstrategia
        ///

        public string descripcionEstrategia
        {
            get { return _descripcionEstrategia; }
            set { _descripcionEstrategia = value; }
        }
        private string _descripcionEstrategia;

        ///

        /// Gets or Sets fechaCumplimiento
        ///

        public DateTime fechaCumplimiento
        {
            get { return _fechaCumplimiento; }
            set { _fechaCumplimiento = value; }
        }
        private DateTime _fechaCumplimiento;

        ///

        /// Gets or Sets estadoEstrategia
        ///

        public string estadoEstrategia
        {
            get { return _estadoEstrategia; }
            set { _estadoEstrategia = value; }
        }
        private string _estadoEstrategia;

        ///

        /// Gets or Sets codObjetivo
        ///

        public int codObjetivo
        {
            get { return _codObjetivo; }
            set { _codObjetivo = value; }
        }
        private int _codObjetivo;
    }
}
