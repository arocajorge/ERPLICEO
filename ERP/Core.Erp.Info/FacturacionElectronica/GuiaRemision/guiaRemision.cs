using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Core.Erp.Info.FacturacionElectronica.Factura_V2;

namespace Core.Erp.Info.FacturacionElectronica.GuiaRemision
{  /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class guiaRemision
    {

        private infoTributaria infoTributariaField;

        private guiaRemisionInfoGuiaRemision infoGuiaRemisionField;

        private guiaRemisionDestinatarios destinatariosField;

        private List<guiaRemisionCampoAdicional> infoAdicionalField;

        private guiaRemisionID idField;

        private string versionField;

        /// <comentarios/>
        public infoTributaria infoTributaria
        {
            get
            {
                return this.infoTributariaField;
            }
            set
            {
                this.infoTributariaField = value;
            }
        }

        /// <comentarios/>
        public guiaRemisionInfoGuiaRemision infoGuiaRemision
        {
            get
            {
                return this.infoGuiaRemisionField;
            }
            set
            {
                this.infoGuiaRemisionField = value;
            }
        }

        /// <comentarios/>
        public guiaRemisionDestinatarios destinatarios
        {
            get
            {
                return this.destinatariosField;
            }
            set
            {
                this.destinatariosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("campoAdicional", IsNullable = false)]
        public List<guiaRemisionCampoAdicional> infoAdicional
        {
            get
            {
                return this.infoAdicionalField;
            }
            set
            {
                this.infoAdicionalField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public guiaRemisionID id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <comentarios/>

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class detalle
    {

        private string codigoInternoField;

        private string codigoAdicionalField;

        private string descripcionField;

        private decimal cantidadField;

        private List<detalleDetAdicional> detallesAdicionalesField;

        /// <comentarios/>
        public string codigoInterno
        {
            get
            {
                return this.codigoInternoField;
            }
            set
            {
                this.codigoInternoField = value;
            }
        }

        /// <comentarios/>
        public string codigoAdicional
        {
            get
            {
                return this.codigoAdicionalField;
            }
            set
            {
                this.codigoAdicionalField = value;
            }
        }

        /// <comentarios/>
        public string descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <comentarios/>
        public decimal cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("detAdicional", IsNullable = false)]
        public List<detalleDetAdicional> detallesAdicionales
        {
            get
            {
                return this.detallesAdicionalesField;
            }
            set
            {
                this.detallesAdicionalesField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class detalleDetAdicional
    {

        private string nombreField;

        private string valorField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class destinatario
    {

        private string identificacionDestinatarioField;

        private string razonSocialDestinatarioField;

        private string dirDestinatarioField;

        private string motivoTrasladoField;

        private string docAduaneroUnicoField;

        private string codEstabDestinoField;

        private string rutaField;

        private string codDocSustentoField;

        private string numDocSustentoField;

        private string numAutDocSustentoField;

        private string fechaEmisionDocSustentoField;

        private destinatarioDetalles detallesField;

        /// <comentarios/>
        public string identificacionDestinatario
        {
            get
            {
                return this.identificacionDestinatarioField;
            }
            set
            {
                this.identificacionDestinatarioField = value;
            }
        }

        /// <comentarios/>
        public string razonSocialDestinatario
        {
            get
            {
                return this.razonSocialDestinatarioField;
            }
            set
            {
                this.razonSocialDestinatarioField = value;
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
        public string motivoTraslado
        {
            get
            {
                return this.motivoTrasladoField;
            }
            set
            {
                this.motivoTrasladoField = value;
            }
        }

        /// <comentarios/>
        public string docAduaneroUnico
        {
            get
            {
                return this.docAduaneroUnicoField;
            }
            set
            {
                this.docAduaneroUnicoField = value;
            }
        }

        /// <comentarios/>
        public string codEstabDestino
        {
            get
            {
                return this.codEstabDestinoField;
            }
            set
            {
                this.codEstabDestinoField = value;
            }
        }

        /// <comentarios/>
        public string ruta
        {
            get
            {
                return this.rutaField;
            }
            set
            {
                this.rutaField = value;
            }
        }

        /// <comentarios/>
        public string codDocSustento
        {
            get
            {
                return this.codDocSustentoField;
            }
            set
            {
                this.codDocSustentoField = value;
            }
        }

        /// <comentarios/>
        public string numDocSustento
        {
            get
            {
                return this.numDocSustentoField;
            }
            set
            {
                this.numDocSustentoField = value;
            }
        }

        /// <comentarios/>
        public string numAutDocSustento
        {
            get
            {
                return this.numAutDocSustentoField;
            }
            set
            {
                this.numAutDocSustentoField = value;
            }
        }

        /// <comentarios/>
        public string fechaEmisionDocSustento
        {
            get
            {
                return this.fechaEmisionDocSustentoField;
            }
            set
            {
                this.fechaEmisionDocSustentoField = value;
            }
        }

        /// <comentarios/>
        public destinatarioDetalles detalles
        {
            get
            {
                return this.detallesField;
            }
            set
            {
                this.detallesField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class destinatarioDetalles
    {

        private List<detalle> detalleField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("detalle")]
        public List<detalle> detalle
        {
            get
            {
                return this.detalleField;
            }
            set
            {
                this.detalleField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class guiaRemisionInfoGuiaRemision
    {

        private string dirEstablecimientoField;

        private string dirPartidaField;

        private string razonSocialTransportistaField;

        private string tipoIdentificacionTransportistaField;

        private string rucTransportistaField;

        private string riseField;

        private string obligadoContabilidadField;

        private string contribuyenteEspecialField;

        private string fechaIniTransporteField;

        private string fechaFinTransporteField;

        private string placaField;

        /// <comentarios/>
        public string dirEstablecimiento
        {
            get
            {
                return this.dirEstablecimientoField;
            }
            set
            {
                this.dirEstablecimientoField = value;
            }
        }

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
        public string rise
        {
            get
            {
                return this.riseField;
            }
            set
            {
                this.riseField = value;
            }
        }

        /// <comentarios/>
        public string obligadoContabilidad
        {
            get
            {
                return this.obligadoContabilidadField;
            }
            set
            {
                this.obligadoContabilidadField = value;
            }
        }

        /// <comentarios/>
        public string contribuyenteEspecial
        {
            get
            {
                return this.contribuyenteEspecialField;
            }
            set
            {
                this.contribuyenteEspecialField = value;
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
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class guiaRemisionDestinatarios
    {

        private List<destinatario> destinatarioField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("destinatario")]
        public List<destinatario> destinatario
        {
            get
            {
                return this.destinatarioField;
            }
            set
            {
                this.destinatarioField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class guiaRemisionCampoAdicional
    {

        private string nombreField;

        private string valueField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum guiaRemisionID
    {

        /// <comentarios/>
        comprobante,
    }
}
