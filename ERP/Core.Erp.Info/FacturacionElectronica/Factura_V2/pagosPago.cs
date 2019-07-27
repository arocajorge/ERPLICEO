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
    public partial class pagosPago
    {

        private string formaPagoField;

        private decimal totalField;

        private decimal plazoField;

        private bool plazoFieldSpecified;

        private string unidadTiempoField;

        /// <comentarios/>
        public string formaPago
        {
            get
            {
                return this.formaPagoField;
            }
            set
            {
                this.formaPagoField = value;
            }
        }

        /// <comentarios/>
        public decimal total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <comentarios/>
        public decimal plazo
        {
            get
            {
                return this.plazoField;
            }
            set
            {
                this.plazoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool plazoSpecified
        {
            get
            {
                return this.plazoFieldSpecified;
            }
            set
            {
                this.plazoFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string unidadTiempo
        {
            get
            {
                return this.unidadTiempoField;
            }
            set
            {
                this.unidadTiempoField = value;
            }
        }
    }
}
