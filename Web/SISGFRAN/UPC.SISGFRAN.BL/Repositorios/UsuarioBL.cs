using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class UsuarioBL
    {

        private static UsuarioBL usuario;
        private UsuarioBL() { }

        public static UsuarioBL Usuario {
            get {
                if (usuario == null)
                {
                    usuario = new UsuarioBL();
                }
                return usuario;
            }
        }

        REST conecRest = new REST();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public UsuarioEL Login(UsuarioEL usuario)
        {
            string postdata = js.Serialize(usuario);
            return js.Deserialize<UsuarioEL>(conecRest.ConectREST("Login", "POST", postdata));
            
            //return new UsuarioDA().Login(usuario);
        }

        public List<UsuarioEL> GetUsuarios(UsuarioEL usuario)
        {
            string postdata = js.Serialize(usuario);
            return js.Deserialize<List<UsuarioEL>>(conecRest.ConectREST("Usuarios", "POST", postdata));
            //return new UsuarioDA().GetUsuarios(usuario);
        }

        public UsuarioEL GetUsuarioById(int? idUsuario)
        {
            string postdata = js.Serialize(idUsuario);
            return js.Deserialize<UsuarioEL>(conecRest.ConectREST("Usuario", "POST", postdata));
            //return new UsuarioDA().GetUsuarioById(idUsuario);
        }

    }
}
