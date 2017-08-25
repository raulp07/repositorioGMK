using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class UsuarioBL
    {
        public UsuarioEL Login(UsuarioEL usuario)
        {
            return new UsuarioDA().Login(usuario);
        }

        public List<UsuarioEL> GetUsuarios(UsuarioEL usuario)
        {
            return new UsuarioDA().GetUsuarios(usuario);
        }

        public UsuarioEL GetUsuarioById(int? idUsuario)
        {
            return new UsuarioDA().GetUsuarioById(idUsuario);
        }

    }
}
