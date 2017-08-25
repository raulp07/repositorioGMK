using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class PerfilBL
    {
        public List<PerfilEL> GetPerfil(PerfilEL perfil)
        {
            return new PerfilDA().GetPerfil(perfil);
        }

        public PerfilEL GetPerfilByID(int? idPerfil)
        {
            return new PerfilDA().GetPerfilByID(idPerfil);
        }
    }
}
