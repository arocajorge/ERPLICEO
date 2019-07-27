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

    public partial class facturaInfoFacturaTotalImpuesto
    {

        private string codigoField;

        private string codigoPorcentajeField;

        private decimal descuentoAdicionalField;

        private bool descuentoAdicionalFieldSpecified;

        private decimal baseImponibleField;

        private decimal tarifaField;

        private bool tarifaFieldSpecified;

        private decimal valorField;

        private decimal valorDevolucionIvaField;

        private bool valorDevolucionIvaFieldSpecified;

        /// <comentarios/>
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
        public string codigoPorcentaje
        {
            get
            {
                return this.codigoPorcentajeField;
            }
            set
            {
                this.codigoPorcentajeField = value;
            }
        }

        /// <comentarios/>
        public decimal descuentoAdicional
        {
            get
            {
                return this.descuentoAdicionalField;
            }
            set
            {
                this.descuentoAdicionalField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool descuentoAdicionalSpecified
        {
            get
            {
                return this.descuentoAdicionalFieldSpecified;
            }
            set
            {
                this.descuentoAdicionalFieldSpecified = value;
            }
        }

        /// <comentarios/>
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
        public decimal tarifa
        {
            get
            {
                return this.tarifaField;
            }
            set
            {
                this.tarifaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tarifaSpecified
        {
            get
            {
                return this.tarifaFieldSpecified;
            }
            set
            {
                this.tarifaFieldSpecified = value;
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

        /// <comentarios/>
        public decimal valorDevolucionIva
        {
            get
            {
                return this.valorDevolucionIvaField;
            }
            set
            {
                this.valorDevolucionIvaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valorDevolucionIvaSpecified
        {
            get
            {
                return this.valorDevolucionIvaFieldSpecified;
            }
            set
            {
                this.valorDevolucionIvaFieldSpecified = value;
            }
        }
    }
}
