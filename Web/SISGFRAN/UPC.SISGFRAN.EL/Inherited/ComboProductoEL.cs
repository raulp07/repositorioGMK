using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class ComboProductoEL
    {

        ///

        /// Gets or Sets codCombo
        ///

        public long codCombo
        {
            get { return _codCombo; }
            set { _codCombo = value; }
        }
        private long _codCombo;

        ///

        /// Gets or Sets codProducto
        ///

        public long codProducto
        {
            get { return _codProducto; }
            set { _codProducto = value; }
        }
        private long _codProducto;

        ///

        /// Gets or Sets cantidad
        ///

        public int cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private int _cantidad;

        ///

        /// Gets or Sets nombreComboProducto
        ///

        public string nombreComboProducto
        {
            get { return _nombreComboProducto; }
            set { _nombreComboProducto = value; }
        }
        private string _nombreComboProducto;

    }
}
