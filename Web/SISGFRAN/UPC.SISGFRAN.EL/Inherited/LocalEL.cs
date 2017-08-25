using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class LocalEL : EncuestaEL
    {
        ///

        /// Gets or Sets codLocal
        ///

        public int codLocal
        {
            get { return _codLocal; }
            set { _codLocal = value; }
        }
        private int _codLocal;

        ///

        /// Gets or Sets nombreLocal
        ///

        public string nombreLocal
        {
            get { return _nombreLocal; }
            set { _nombreLocal = value; }
        }
        private string _nombreLocal;

        ///

        /// Gets or Sets fechaAperturaLocal
        ///

        public DateTime fechaAperturaLocal
        {
            get { return _fechaAperturaLocal; }
            set { _fechaAperturaLocal = value; }
        }
        private DateTime _fechaAperturaLocal;

        ///

        /// Gets or Sets responsableLocal
        ///

        public string responsableLocal
        {
            get { return _responsableLocal; }
            set { _responsableLocal = value; }
        }
        private string _responsableLocal;

        ///

        /// Gets or Sets distritoLocal
        ///

        public string distritoLocal
        {
            get { return _distritoLocal; }
            set { _distritoLocal = value; }
        }
        private string _distritoLocal;

        ///

        /// Gets or Sets direccionLocal
        ///

        public string direccionLocal
        {
            get { return _direccionLocal; }
            set { _direccionLocal = value; }
        }
        private string _direccionLocal;

        ///

        /// Gets or Sets latitudLocal
        ///

        public string latitudLocal
        {
            get { return _latitudLocal; }
            set { _latitudLocal = value; }
        }
        private string _latitudLocal;

        ///

        /// Gets or Sets longitudLocal
        ///

        public string longitudLocal
        {
            get { return _longitudLocal; }
            set { _longitudLocal = value; }
        }
        private string _longitudLocal;
    }
}
