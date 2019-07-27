using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Core.Erp.Info.FacturacionElectronica.Factura_V2;

namespace Core.Erp.Info.FacturacionElectronica.Retencion
{

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class comprobanteRetencion
    {

        private infoTributaria infoTributariaField;

        private comprobanteRetencionInfoCompRetencion infoCompRetencionField;

        private List<impuesto> impuestosField;

        private List<comprobanteRetencionCampoAdicional> infoAdicionalField;

        private comprobanteRetencionID idField;

        private bool idFieldSpecified;

        private string versionField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public comprobanteRetencionInfoCompRetencion infoCompRetencion
        {
            get
            {
                return this.infoCompRetencionField;
            }
            set
            {
                this.infoCompRetencionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
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
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("campoAdicional", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<comprobanteRetencionCampoAdicional> infoAdicional
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
        public comprobanteRetencionID id
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class comprobanteRetencionInfoCompRetencion
    {

        private string fechaEmisionField;

        private string dirEstablecimientoField;

        private string contribuyenteEspecialField;

        private string obligadoContabilidadField;

        private string tipoIdentificacionSujetoRetenidoField;

        private string razonSocialSujetoRetenidoField;

        private string identificacionSujetoRetenidoField;

        private string periodoFiscalField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string tipoIdentificacionSujetoRetenido
        {
            get
            {
                return this.tipoIdentificacionSujetoRetenidoField;
            }
            set
            {
                this.tipoIdentificacionSujetoRetenidoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string razonSocialSujetoRetenido
        {
            get
            {
                return this.razonSocialSujetoRetenidoField;
            }
            set
            {
                this.razonSocialSujetoRetenidoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string identificacionSujetoRetenido
        {
            get
            {
                return this.identificacionSujetoRetenidoField;
            }
            set
            {
                this.identificacionSujetoRetenidoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string periodoFiscal
        {
            get
            {
                return this.periodoFiscalField;
            }
            set
            {
                this.periodoFiscalField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class comprobanteRetencionCampoAdicional
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
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class impuesto
    {

        private string codigoField;

        private string codigoRetencionField;

        private decimal baseImponibleField;

        private decimal porcentajeRetenerField;

        private decimal valorRetenidoField;

        private string codDocSustentoField;

        private string numDocSustentoField;

        private string fechaEmisionDocSustentoField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string codigoRetencion
        {
            get
            {
                return this.codigoRetencionField;
            }
            set
            {
                this.codigoRetencionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal baseImponible
        {
            get
            {
                return this.baseImponibleField;
            }
            set
            {
                this.baseImponibleField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal porcentajeRetener
        {
            get
            {
                return this.porcentajeRetenerField;
            }
            set
            {
                this.porcentajeRetenerField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal valorRetenido
        {
            get
            {
                return this.valorRetenidoField;
            }
            set
            {
                this.valorRetenidoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    }




    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum comprobanteRetencionID
    {

        /// <comentarios/>
        comprobante = 1,
    }

}