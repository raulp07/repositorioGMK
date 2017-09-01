using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class ComboEL :comboPlanMarketingEL
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

        /// Gets or Sets nombre
        ///
        
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _nombre;

        ///

        /// Gets or Sets descripcion
        ///
        
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private string _descripcion;

        ///

        /// Gets or Sets precio
        ///
        
        public decimal precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        private decimal _precio;

        ///

        /// Gets or Sets descuento
        ///
        
        public decimal descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }
        private decimal _descuento;

        ///

        /// Gets or Sets estado
        ///
        
        public bool estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        private bool _estado;

        ///

        /// Gets or Sets codCategoria
        ///
        
        public long codCategoria
        {
            get { return _codCategoria; }
            set { _codCategoria = value; }
        }
        private long _codCategoria;

        ///

        /// Gets or Sets fechaCreacion
        ///
        
        public DateTime fechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        private DateTime _fechaCreacion;

        ///

        /// Gets or Sets fechaModificacion
        ///
        
        public DateTime fechaModificacion
        {
            get { return _fechaModificacion; }
            set { _fechaModificacion = value; }
        }
        private DateTime _fechaModificacion;

        public int codPlanMkt
        {
            get { return _codPlanMkt; }
            set { _codPlanMkt = value; }
        }
        private int _codPlanMkt;
    }
}
