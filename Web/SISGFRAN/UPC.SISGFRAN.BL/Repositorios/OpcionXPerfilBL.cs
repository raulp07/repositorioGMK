using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class OpcionXPerfilBL
    {
        private OpcionXPerfilBL() { }

        private static OpcionXPerfilBL opcionXPerfil;

        public static OpcionXPerfilBL OpcionXPerfil {
            get {
                if (opcionXPerfil == null)
                {
                    opcionXPerfil = new OpcionXPerfilBL();
                }
                return opcionXPerfil;
            }
        }


        REST conecRest = new REST();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public List<OpcionXPerfilEL> ListMenu(OpcionXPerfilEL opcionPerfil)
        {
            string postdata = js.Serialize(opcionPerfil);
            return js.Deserialize<List<OpcionXPerfilEL>>(conecRest.ConectREST("ListaMenu", "POST", postdata));
            //return new OpcionXPerfilDA().ListMenu(opcionPerfil);
        }
    }
}
