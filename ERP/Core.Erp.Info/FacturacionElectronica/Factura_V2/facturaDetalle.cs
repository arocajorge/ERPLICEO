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
    public partial class facturaDetalle
    {

        private string codigoPrincipalField;

        private string codigoAuxiliarField;

        private string descripcionField;

        private string unidadMedidaField;

        private decimal cantidadField;

        private decimal precioUnitarioField;

        private decimal precioSinSubsidioField;

        private bool precioSinSubsidioFieldSpecified;

        private decimal descuentoField;

        private decimal precioTotalSinImpuestoField;

        private List<facturaDetalleDetAdicional> detallesAdicionalesField;

        private List<impuesto> impuestosField;

        /// <comentarios/>
        public string codigoPrincipal
        {
            get
            {
                return this.codigoPrincipalField;
            }
            set
            {
                this.codigoPrincipalField = value;
            }
        }

        /// <comentarios/>
        public string codigoAuxiliar
        {
            get
            {
                return this.codigoAuxiliarField;
            }
            set
            {
                this.codigoAuxiliarField = value;
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
        public string unidadMedida
        {
            get
            {
                return this.unidadMedidaField;
            }
            set
            {
                this.unidadMedidaField = value;
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
        public decimal precioUnitario
        {
            get
            {
                return this.precioUnitarioField;
            }
            set
            {
                this.precioUnitarioField = value;
            }
        }

        /// <comentarios/>
        public decimal precioSinSubsidio
        {
            get
            {
                return this.precioSinSubsidioField;
            }
            set
            {
                this.precioSinSubsidioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool precioSinSubsidioSpecified
        {
            get
            {
                return this.precioSinSubsidioFieldSpecified;
            }
            set
            {
                this.precioSinSubsidioFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal descuento
        {
            get
            {
                return this.descuentoField;
            }
            set
            {
                this.descuentoField = value;
            }
        }

        /// <comentarios/>
        public decimal precioTotalSinImpuesto
        {
            get
            {
                return this.precioTotalSinImpuestoField;
            }
            set
            {
                this.precioTotalSinImpuestoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("detAdicional", IsNullable = false)]
        public List<facturaDetalleDetAdicional> detallesAdicionales
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
    }
}
