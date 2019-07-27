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
    public partial class reembolsosReembolsoDetalle
    {

        private string tipoIdentificacionProveedorReembolsoField;

        private string identificacionProveedorReembolsoField;

        private string codPaisPagoProveedorReembolsoField;

        private string tipoProveedorReembolsoField;

        private string codDocReembolsoField;

        private string estabDocReembolsoField;

        private string ptoEmiDocReembolsoField;

        private string secuencialDocReembolsoField;

        private string fechaEmisionDocReembolsoField;

        private string numeroautorizacionDocReembField;

        private List<detalleImpuestosDetalleImpuesto> detalleImpuestosField;

        /// <comentarios/>
        public string tipoIdentificacionProveedorReembolso
        {
            get
            {
                return this.tipoIdentificacionProveedorReembolsoField;
            }
            set
            {
                this.tipoIdentificacionProveedorReembolsoField = value;
            }
        }

        /// <comentarios/>
        public string identificacionProveedorReembolso
        {
            get
            {
                return this.identificacionProveedorReembolsoField;
            }
            set
            {
                this.identificacionProveedorReembolsoField = value;
            }
        }

        /// <comentarios/>
        public string codPaisPagoProveedorReembolso
        {
            get
            {
                return this.codPaisPagoProveedorReembolsoField;
            }
            set
            {
                this.codPaisPagoProveedorReembolsoField = value;
            }
        }

        /// <comentarios/>
        public string tipoProveedorReembolso
        {
            get
            {
                return this.tipoProveedorReembolsoField;
            }
            set
            {
                this.tipoProveedorReembolsoField = value;
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
        public string estabDocReembolso
        {
            get
            {
                return this.estabDocReembolsoField;
            }
            set
            {
                this.estabDocReembolsoField = value;
            }
        }

        /// <comentarios/>
        public string ptoEmiDocReembolso
        {
            get
            {
                return this.ptoEmiDocReembolsoField;
            }
            set
            {
                this.ptoEmiDocReembolsoField = value;
            }
        }

        /// <comentarios/>
        public string secuencialDocReembolso
        {
            get
            {
                return this.secuencialDocReembolsoField;
            }
            set
            {
                this.secuencialDocReembolsoField = value;
            }
        }

        /// <comentarios/>
        public string fechaEmisionDocReembolso
        {
            get
            {
                return this.fechaEmisionDocReembolsoField;
            }
            set
            {
                this.fechaEmisionDocReembolsoField = value;
            }
        }

        /// <comentarios/>
        public string numeroautorizacionDocReemb
        {
            get
            {
                return this.numeroautorizacionDocReembField;
            }
            set
            {
                this.numeroautorizacionDocReembField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("detalleImpuesto", IsNullable = false)]
        public List<detalleImpuestosDetalleImpuesto> detalleImpuestos
        {
            get
            {
                return this.detalleImpuestosField;
            }
            set
            {
                this.detalleImpuestosField = value;
            }
        }
    }


}
