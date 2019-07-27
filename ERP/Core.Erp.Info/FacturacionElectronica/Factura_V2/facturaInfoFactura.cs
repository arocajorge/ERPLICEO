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

    public partial class facturaInfoFactura
    {

        private string fechaEmisionField;

        private string dirEstablecimientoField;

        private string contribuyenteEspecialField;

        private obligadoContabilidad obligadoContabilidadField;

        private bool obligadoContabilidadFieldSpecified;

        private string comercioExteriorField;

        private string incoTermFacturaField;

        private string lugarIncoTermField;

        private string paisOrigenField;

        private string puertoEmbarqueField;

        private string puertoDestinoField;

        private string paisDestinoField;

        private string paisAdquisicionField;

        private string tipoIdentificacionCompradorField;

        private string guiaRemisionField;

        private string razonSocialCompradorField;

        private string identificacionCompradorField;

        private string direccionCompradorField;

        private decimal totalSinImpuestosField;

        private decimal totalSubsidioField;

        private bool totalSubsidioFieldSpecified;

        private string incoTermTotalSinImpuestosField;

        private decimal totalDescuentoField;

        private string codDocReembolsoField;

        private decimal totalComprobantesReembolsoField;

        private bool totalComprobantesReembolsoFieldSpecified;

        private decimal totalBaseImponibleReembolsoField;

        private bool totalBaseImponibleReembolsoFieldSpecified;

        private decimal totalImpuestoReembolsoField;

        private bool totalImpuestoReembolsoFieldSpecified;

        private List<facturaInfoFacturaTotalImpuesto> totalConImpuestosField;

        private decimal propinaField;

        private bool propinaFieldSpecified;

        private decimal fleteInternacionalField;

        private bool fleteInternacionalFieldSpecified;

        private decimal seguroInternacionalField;

        private bool seguroInternacionalFieldSpecified;

        private decimal gastosAduanerosField;

        private bool gastosAduanerosFieldSpecified;

        private decimal gastosTransporteOtrosField;

        private bool gastosTransporteOtrosFieldSpecified;

        private decimal importeTotalField;

        private string monedaField;

        private List<pagosPago> pagosField;

        private decimal valorRetIvaField;

        private bool valorRetIvaFieldSpecified;

        private decimal valorRetRentaField;

        private bool valorRetRentaFieldSpecified;

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
        public obligadoContabilidad obligadoContabilidad
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool obligadoContabilidadSpecified
        {
            get
            {
                return this.obligadoContabilidadFieldSpecified;
            }
            set
            {
                this.obligadoContabilidadFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string comercioExterior
        {
            get
            {
                return this.comercioExteriorField;
            }
            set
            {
                this.comercioExteriorField = value;
            }
        }

        /// <comentarios/>
        public string incoTermFactura
        {
            get
            {
                return this.incoTermFacturaField;
            }
            set
            {
                this.incoTermFacturaField = value;
            }
        }

        /// <comentarios/>
        public string lugarIncoTerm
        {
            get
            {
                return this.lugarIncoTermField;
            }
            set
            {
                this.lugarIncoTermField = value;
            }
        }

        /// <comentarios/>
        public string paisOrigen
        {
            get
            {
                return this.paisOrigenField;
            }
            set
            {
                this.paisOrigenField = value;
            }
        }

        /// <comentarios/>
        public string puertoEmbarque
        {
            get
            {
                return this.puertoEmbarqueField;
            }
            set
            {
                this.puertoEmbarqueField = value;
            }
        }

        /// <comentarios/>
        public string puertoDestino
        {
            get
            {
                return this.puertoDestinoField;
            }
            set
            {
                this.puertoDestinoField = value;
            }
        }

        /// <comentarios/>
        public string paisDestino
        {
            get
            {
                return this.paisDestinoField;
            }
            set
            {
                this.paisDestinoField = value;
            }
        }

        /// <comentarios/>
        public string paisAdquisicion
        {
            get
            {
                return this.paisAdquisicionField;
            }
            set
            {
                this.paisAdquisicionField = value;
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
        public string guiaRemision
        {
            get
            {
                return this.guiaRemisionField;
            }
            set
            {
                this.guiaRemisionField = value;
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
        public string direccionComprador
        {
            get
            {
                return this.direccionCompradorField;
            }
            set
            {
                this.direccionCompradorField = value;
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
        public decimal totalSubsidio
        {
            get
            {
                return this.totalSubsidioField;
            }
            set
            {
                this.totalSubsidioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalSubsidioSpecified
        {
            get
            {
                return this.totalSubsidioFieldSpecified;
            }
            set
            {
                this.totalSubsidioFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string incoTermTotalSinImpuestos
        {
            get
            {
                return this.incoTermTotalSinImpuestosField;
            }
            set
            {
                this.incoTermTotalSinImpuestosField = value;
            }
        }

        /// <comentarios/>
        public decimal totalDescuento
        {
            get
            {
                return this.totalDescuentoField;
            }
            set
            {
                this.totalDescuentoField = value;
            }
        }

        /// <comentarios/>
        public string codDocReembolso
        {
            get
            {
                return this.codDocReembolsoField;
            }
            set
            {
                this.codDocReembolsoField = value;
            }
        }

        /// <comentarios/>
        public decimal totalComprobantesReembolso
        {
            get
            {
                return this.totalComprobantesReembolsoField;
            }
            set
            {
                this.totalComprobantesReembolsoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalComprobantesReembolsoSpecified
        {
            get
            {
                return this.totalComprobantesReembolsoFieldSpecified;
            }
            set
            {
                this.totalComprobantesReembolsoFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal totalBaseImponibleReembolso
        {
            get
            {
                return this.totalBaseImponibleReembolsoField;
            }
            set
            {
                this.totalBaseImponibleReembolsoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalBaseImponibleReembolsoSpecified
        {
            get
            {
                return this.totalBaseImponibleReembolsoFieldSpecified;
            }
            set
            {
                this.totalBaseImponibleReembolsoFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal totalImpuestoReembolso
        {
            get
            {
                return this.totalImpuestoReembolsoField;
            }
            set
            {
                this.totalImpuestoReembolsoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalImpuestoReembolsoSpecified
        {
            get
            {
                return this.totalImpuestoReembolsoFieldSpecified;
            }
            set
            {
                this.totalImpuestoReembolsoFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("totalImpuesto", IsNullable = false)]
        public List<facturaInfoFacturaTotalImpuesto> totalConImpuestos
        {
            get
            {
                return this.totalConImpuestosField;
            }
            set
            {
                this.totalConImpuestosField = value;
            }
        }

        /// <comentarios/>
        public decimal propina
        {
            get
            {
                return this.propinaField;
            }
            set
            {
                this.propinaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool propinaSpecified
        {
            get
            {
                return this.propinaFieldSpecified;
            }
            set
            {
                this.propinaFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal fleteInternacional
        {
            get
            {
                return this.fleteInternacionalField;
            }
            set
            {
                this.fleteInternacionalField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fleteInternacionalSpecified
        {
            get
            {
                return this.fleteInternacionalFieldSpecified;
            }
            set
            {
                this.fleteInternacionalFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal seguroInternacional
        {
            get
            {
                return this.seguroInternacionalField;
            }
            set
            {
                this.seguroInternacionalField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool seguroInternacionalSpecified
        {
            get
            {
                return this.seguroInternacionalFieldSpecified;
            }
            set
            {
                this.seguroInternacionalFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal gastosAduaneros
        {
            get
            {
                return this.gastosAduanerosField;
            }
            set
            {
                this.gastosAduanerosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool gastosAduanerosSpecified
        {
            get
            {
                return this.gastosAduanerosFieldSpecified;
            }
            set
            {
                this.gastosAduanerosFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal gastosTransporteOtros
        {
            get
            {
                return this.gastosTransporteOtrosField;
            }
            set
            {
                this.gastosTransporteOtrosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool gastosTransporteOtrosSpecified
        {
            get
            {
                return this.gastosTransporteOtrosFieldSpecified;
            }
            set
            {
                this.gastosTransporteOtrosFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal importeTotal
        {
            get
            {
                return this.importeTotalField;
            }
            set
            {
                this.importeTotalField = value;
            }
        }

        /// <comentarios/>
        public string moneda
        {
            get
            {
                return this.monedaField;
            }
            set
            {
                this.monedaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("pago", IsNullable = false)]
        public List<pagosPago> pagos
        {
            get
            {
                return this.pagosField;
            }
            set
            {
                this.pagosField = value;
            }
        }

        /// <comentarios/>
        public decimal valorRetIva
        {
            get
            {
                return this.valorRetIvaField;
            }
            set
            {
                this.valorRetIvaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valorRetIvaSpecified
        {
            get
            {
                return this.valorRetIvaFieldSpecified;
            }
            set
            {
                this.valorRetIvaFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal valorRetRenta
        {
            get
            {
                return this.valorRetRentaField;
            }
            set
            {
                this.valorRetRentaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valorRetRentaSpecified
        {
            get
            {
                return this.valorRetRentaFieldSpecified;
            }
            set
            {
                this.valorRetRentaFieldSpecified = value;
            }
        }
    }



    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    public enum obligadoContabilidad
    {

        /// <comentarios/>
        SI,

        /// <comentarios/>
        NO,
    }
}
