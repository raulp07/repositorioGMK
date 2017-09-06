using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class UsuarioEL : BaseEL
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public PerfilEL Perfil { get; set; }

        [DataMember]
        public string CtaUsuario { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Nombres { get; set; }

        [DataMember]
        public string Apellidos { get; set; }

        [DataMember]
        public string Cargo { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public bool CambiarContrasenia { get; set; }

        [DataMember]
        public DateTime FechaVenceCuenta { get; set; }

        [DataMember]
        public DateTime FechaVencePass { get; set; }

        public string NombreCompleto { get { return string.Concat(Apellidos, ", ", Nombres); } }

    }
}