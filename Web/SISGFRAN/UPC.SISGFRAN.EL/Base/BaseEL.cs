using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Base
{
    public class BaseEL
    {
        public int CodeMessage { get; set; }
        public string MessageErr { get; set; }

        #region "Auditoria"
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModifica { get; set; }
        public DateTime FechaModifica { get; set; }
        #endregion
    }
}
