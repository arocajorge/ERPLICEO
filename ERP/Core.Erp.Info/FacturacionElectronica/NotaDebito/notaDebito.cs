using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;
using Core.Erp.Info.FacturacionElectronica.Factura_V2;

namespace Core.Erp.Info.FacturacionElectronica.NotaDebito
{
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class notaDebito
    {

        private infoTributaria infoTributariaField;

        private notaDebitoInfoNotaDebito infoNotaDebitoField;

        private notaDebitoMotivos motivosField;

        private List<notaDebitoCampoAdicional> infoAdicionalField;

        private notaDebitoID idField;

        private bool idFieldSpecified;

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
        public notaDebitoInfoNotaDebito infoNotaDebito
        {
            get
            {
                return this.infoNotaDebitoField;
            }
            set
            {
                this.infoNotaDebitoField = value;
            }
        }

        /// <comentarios/>
        public notaDebitoMotivos motivos
        {
            get
            {
                return this.motivosField;
            }
            set
            {
                this.motivosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("campoAdicional", IsNullable = false)]
        public List<notaDebitoCampoAdicional> infoAdicional
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
        public notaDebitoID id
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool idSpecified
        {
            get
            {
                return this.idFieldSpecified;
            }
            set
            {
                this.idFieldSpecified = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class notaDebitoInfoNotaDebito
    {

        private string fechaEmisionField;

        private string dirEstablecimientoField;

        private string tipoIdentificacionCompradorField;

        private string razonSocialCompradorField;

        private string identificacionCompradorField;

        private string contribuyenteEspecialField;

        private string obligadoContabilidadField;

        private string riseField;

        private string codDocModificadoField;

        private string numDocModificadoField;

        private string fechaEmisionDocSustentoField;

        private decimal totalSinImpuestosField;

        private List<impuesto> impuestosField;

        private decimal valorTotalField;

        /// <comentarios/>
        public string fechaEmision
        {
            get
            {
                return this.fechaEmisionField;
            }
            set
            {
                this.fechaEmisionField = value;
            }
        }

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
        public string tipoIdentificacionComprador
        {
            get
            {
                return this.tipoIdentificacionCompradorField;
            }
            set
            {
                this.tipoIdentificacionCompradorField = value;
            }
        }

        /// <comentarios/>
        public string razonSocialComprador
        {
            get
            {
                return this.razonSocialCompradorField;
            }
            set
            {
                this.razonSocialCompradorField = value;
            }
        }

        /// <comentarios/>
        public string identificacionComprador
        {
            get
            {
                return this.identificacionCompradorField;
            }
            set
            {
                this.identificacionCompradorField = value;
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
        public string codDocModificado
        {
            get
            {
                return this.codDocModificadoField;
            }
            set
            {
                this.codDocModificadoField = value;
            }
        }

        /// <comentarios/>
        public string numDocModificado
        {
            get
            {
                return this.numDocModificadoField;
            }
            set
            {
                this.numDocModificadoField = value;
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
        public decimal totalSinImpuestos
        {
            get
            {
                return this.totalSinImpuestosField;
            }
            set
            {
                this.totalSinImpuestosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<impuesto> impuestos
        {
            get
            {
                return this.impuestosField;
            }
            set
            {
                this.impuestosField = value;
            }
        }

        /// <comentarios/>
        public decimal valorTotal
        {
            get
            {
                return this.valorTotalField;
            }
            set
            {
                this.valorTotalField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class notaDebitoMotivos
    {

        private List<notaDebitoMotivosMotivo> motivoField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("motivo")]
        public List<notaDebitoMotivosMotivo> motivo
        {
            get
            {
                return this.motivoField;
            }
            set
            {
                this.motivoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class notaDebitoMotivosMotivo
    {

        private string razonField;

        private decimal valorField;

        /// <comentarios/>
        public string razon
        {
            get
            {
                return this.razonField;
            }
            set
            {
                this.razonField = value;
            }
        }

        /// <comentarios/>
        public decimal valor
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class notaDebitoCampoAdicional
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
    public enum notaDebitoID
    {

        /// <comentarios/>
        comprobante,
    }

}
