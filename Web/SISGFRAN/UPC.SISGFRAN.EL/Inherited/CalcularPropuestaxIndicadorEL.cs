using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class CalcularPropuestaxIndicadorEL
    {
        
        public string listLocal { get; set; }
        
        public int codLocal { get; set; }
        
        public int codCombo { get; set; }
        
        public int indConsumo { get; set; }
        
        public int indSabor { get; set; }
        
        public int indCosto { get; set; }
        
        public int cantPuntuacionMax { get; set; }
        
        public int cantProyeccionVenta { get; set; }
        
        public string nombreCaractComboVenta { get; set; }
        
        public decimal impProyeccionCosto { get; set; }
    }
}
