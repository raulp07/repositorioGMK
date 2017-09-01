using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio 
{
    [DataContract]
    public class LocalEL : EncuestaEL
    {
        ///

        /// Gets or Sets id
        ///
        [DataMember]
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _id;

        ///

        /// Gets or Sets franquiciaId
        ///
        [DataMember]
        public int franquiciaId
        {
            get { return _franquiciaId; }
            set { _franquiciaId = value; }
        }
        private int _franquiciaId;

        ///

        /// Gets or Sets nombre
        ///
        [DataMember]
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _nombre;

        ///

        /// Gets or Sets fechaApertura
        ///
        [DataMember]
        public DateTime fechaApertura
        {
            get { return _fechaApertura; }
            set { _fechaApertura = value; }
        }
        private DateTime _fechaApertura;

        ///

        /// Gets or Sets responsable
        ///
        [DataMember]
        public string responsable
        {
            get { return _responsable; }
            set { _responsable = value; }
        }
        private string _responsable;

        ///

        /// Gets or Sets distrito
        ///
        [DataMember]
        public string distrito
        {
            get { return _distrito; }
            set { _distrito = value; }
        }
        private string _distrito;

        ///

        /// Gets or Sets direccion
        ///
        [DataMember]
        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        private string _direccion;

        ///

        /// Gets or Sets latitud
        ///
        [DataMember]
        public string latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }
        private string _latitud;

        ///

        /// Gets or Sets longitud
        ///
        [DataMember]
        public string longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }
        private string _longitud;

        ///

        /// Gets or Sets auditoriaUC
        ///
        [DataMember]
        public int auditoriaUC
        {
            get { return _auditoriaUC; }
            set { _auditoriaUC = value; }
        }
        private int _auditoriaUC;

        ///

        /// Gets or Sets auditoriaUM
        ///
        [DataMember]
        public int auditoriaUM
        {
            get { return _auditoriaUM; }
            set { _auditoriaUM = value; }
        }
        private int _auditoriaUM;

        ///

        /// Gets or Sets auditoriaFC
        ///
        [DataMember]
        public DateTime auditoriaFC
        {
            get { return _auditoriaFC; }
            set { _auditoriaFC = value; }
        }
        private DateTime _auditoriaFC;

        ///

        /// Gets or Sets auditoriaFM
        ///
        [DataMember]
        public DateTime auditoriaFM
        {
            get { return _auditoriaFM; }
            set { _auditoriaFM = value; }
        }
        private DateTime _auditoriaFM;
       

    }
}