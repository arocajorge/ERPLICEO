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
    public partial class detalleImpuestosDetalleImpuesto
    {

        private string codigoField;

        private string codigoPorcentajeField;

        private string tarifaField;

        private decimal baseImponibleReembolsoField;

        private decimal impuestoReembolsoField;

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
        public string tarifa
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
        public decimal baseImponibleReembolso
        {
            get
            {
                return this.baseImponibleReembolsoField;
            }
            set
            {
                this.baseImponibleReembolsoField = value;
            }
        }

        /// <comentarios/>
        public decimal impuestoReembolso
        {
            get
            {
                return this.impuestoReembolsoField;
            }
            set
            {
                this.impuestoReembolsoField = value;
            }
        }
    }
}
