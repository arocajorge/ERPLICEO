using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Erp.Info.FacturacionElectronica.Factura_V2
{
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class facturaInfoSustitutivaGuiaRemision
    {

        private string dirPartidaField;

        private string dirDestinatarioField;

        private string fechaIniTransporteField;

        private string fechaFinTransporteField;

        private string razonSocialTransportistaField;

        private string tipoIdentificacionTransportistaField;

        private string rucTransportistaField;

        private string placaField;

        private List<destino> destinosField;

        /// <comentarios/>
        public string dirPartida
        {
            get
            {
                return this.dirPartidaField;
            }
            set
            {
                this.dirPartidaField = value;
            }
        }

        /// <comentarios/>
        public string dirDestinatario
        {
            get
            {
                return this.dirDestinatarioField;
            }
            set
            {
                this.dirDestinatarioField = value;
            }
        }

        /// <comentarios/>
        public string fechaIniTransporte
        {
            get
            {
                return this.fechaIniTransporteField;
            }
            set
            {
                this.fechaIniTransporteField = value;
            }
        }

        /// <comentarios/>
        public string fechaFinTransporte
        {
            get
            {
                return this.fechaFinTransporteField;
            }
            set
            {
                this.fechaFinTransporteField = value;
            }
        }

        /// <comentarios/>
        public string razonSocialTransportista
        {
            get
            {
                return this.razonSocialTransportistaField;
            }
            set
            {
                this.razonSocialTransportistaField = value;
            }
        }

        /// <comentarios/>
        public string tipoIdentificacionTransportista
        {
            get
            {
                return this.tipoIdentificacionTransportistaField;
            }
            set
            {
                this.tipoIdentificacionTransportistaField = value;
            }
        }

        /// <comentarios/>
        public string rucTransportista
        {
            get
            {
                return this.rucTransportistaField;
            }
            set
            {
                this.rucTransportistaField = value;
            }
        }

        /// <comentarios/>
        public string placa
        {
            get
            {
                return this.placaField;
            }
            set
            {
                this.placaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<destino> destinos
        {
            get
            {
                return this.destinosField;
            }
            set
            {
                this.destinosField = value;
            }
        }
    }
}
