using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class OpcionXPerfilBL
    {
        public List<OpcionXPerfilEL> ListMenu(OpcionXPerfilEL opcionPerfil)
        {
            return new OpcionXPerfilDA().ListMenu(opcionPerfil);
        }
    }
}
