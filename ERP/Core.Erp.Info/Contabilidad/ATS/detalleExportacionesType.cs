using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad.ATS
{
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class detalleExportacionesType
    {

        private string tpIdClienteExField;

        private string idClienteExField;

        private parteRelType parteRelExpField;

        private bool parteRelExpFieldSpecified;

        private string tipoCliField;

        private string denoExpCliField;

        private tipoRegiType tipoRegiField;

        private bool tipoRegiFieldSpecified;

        private string paisEfecPagoGenField;

        private string paisEfecPagoParFisField;

        private string denopagoRegFisField;

        private string paisEfecExpField;

        private parteRelType pagoRegFisField;

        private bool pagoRegFisFieldSpecified;

        private string exportacionDeField;

        private string tipIngExtField;

        private parteRelType ingExtGravOtroPaisField;

        private bool ingExtGravOtroPaisFieldSpecified;

        private decimal impuestoOtroPaisField;

        private bool impuestoOtroPaisFieldSpecified;

        private string tipoComprobanteField;

        private string distAduaneroField;

        private string anioField;

        private string regimenField;

        private string correlativoField;

        private string verificadorField;

        private string docTranspField;

        private string fechaEmbarqueField;

        private string fueField;

        private decimal valorFOBField;

        private decimal valorFOBComprobanteField;

        private string establecimientoField;

        private string puntoEmisionField;

        private string secuencialField;

        private string autorizacionField;

        private string fechaEmisionField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string tpIdClienteEx
        {
            get
            {
                return this.tpIdClienteExField;
            }
            set
            {
                this.tpIdClienteExField = value;
            }
        }

        /// <comentarios/>
        public string idClienteEx
        {
            get
            {
                return this.idClienteExField;
            }
            set
            {
                this.idClienteExField = value;
            }
        }

        /// <comentarios/>
        public parteRelType parteRelExp
        {
            get
            {
                return this.parteRelExpField;
            }
            set
            {
                this.parteRelExpField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool parteRelExpSpecified
        {
            get
            {
                return this.parteRelExpFieldSpecified;
            }
            set
            {
                this.parteRelExpFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string tipoCli
        {
            get
            {
                return this.tipoCliField;
            }
            set
            {
                this.tipoCliField = value;
            }
        }

        /// <comentarios/>
        public string denoExpCli
        {
            get
            {
                return this.denoExpCliField;
            }
            set
            {
                this.denoExpCliField = value;
            }
        }

        /// <comentarios/>
        public tipoRegiType tipoRegi
        {
            get
            {
                return this.tipoRegiField;
            }
            set
            {
                this.tipoRegiField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tipoRegiSpecified
        {
            get
            {
                return this.tipoRegiFieldSpecified;
            }
            set
            {
                this.tipoRegiFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string paisEfecPagoGen
        {
            get
            {
                return this.paisEfecPagoGenField;
            }
            set
            {
                this.paisEfecPagoGenField = value;
            }
        }

        /// <comentarios/>
        public string paisEfecPagoParFis
        {
            get
            {
                return this.paisEfecPagoParFisField;
            }
            set
            {
                this.paisEfecPagoParFisField = value;
            }
        }

        /// <comentarios/>
        public string denopagoRegFis
        {
            get
            {
                return this.denopagoRegFisField;
            }
            set
            {
                this.denopagoRegFisField = value;
            }
        }

        /// <comentarios/>
        public string paisEfecExp
        {
            get
            {
                return this.paisEfecExpField;
            }
            set
            {
                this.paisEfecExpField = value;
            }
        }

        /// <comentarios/>
        public parteRelType pagoRegFis
        {
            get
            {
                return this.pagoRegFisField;
            }
            set
            {
                this.pagoRegFisField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool pagoRegFisSpecified
        {
            get
            {
                return this.pagoRegFisFieldSpecified;
            }
            set
            {
                this.pagoRegFisFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string exportacionDe
        {
            get
            {
                return this.exportacionDeField;
            }
            set
            {
                this.exportacionDeField = value;
            }
        }

        /// <comentarios/>
        public string tipIngExt
        {
            get
            {
                return this.tipIngExtField;
            }
            set
            {
                this.tipIngExtField = value;
            }
        }

        /// <comentarios/>
        public parteRelType ingExtGravOtroPais
        {
            get
            {
                return this.ingExtGravOtroPaisField;
            }
            set
            {
                this.ingExtGravOtroPaisField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ingExtGravOtroPaisSpecified
        {
            get
            {
                return this.ingExtGravOtroPaisFieldSpecified;
            }
            set
            {
                this.ingExtGravOtroPaisFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal impuestoOtroPais
        {
            get
            {
                return this.impuestoOtroPaisField;
            }
            set
            {
                this.impuestoOtroPaisField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool impuestoOtroPaisSpecified
        {
            get
            {
                return this.impuestoOtroPaisFieldSpecified;
            }
            set
            {
                this.impuestoOtroPaisFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string tipoComprobante
        {
            get
            {
                return this.tipoComprobanteField;
            }
            set
            {
                this.tipoComprobanteField = value;
            }
        }

        /// <comentarios/>
        public string distAduanero
        {
            get
            {
                return this.distAduaneroField;
            }
            set
            {
                this.distAduaneroField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string anio
        {
            get
            {
                return this.anioField;
            }
            set
            {
                this.anioField = value;
            }
        }

        /// <comentarios/>
        public string regimen
        {
            get
            {
                return this.regimenField;
            }
            set
            {
                this.regimenField = value;
            }
        }

        /// <comentarios/>
        public string correlativo
        {
            get
            {
                return this.correlativoField;
            }
            set
            {
                this.correlativoField = value;
            }
        }

        /// <comentarios/>
        public string verificador
        {
            get
            {
                return this.verificadorField;
            }
            set
            {
                this.verificadorField = value;
            }
        }

        /// <comentarios/>
        public string docTransp
        {
            get
            {
                return this.docTranspField;
            }
            set
            {
                this.docTranspField = value;
            }
        }

        /// <comentarios/>
        public string fechaEmbarque
        {
            get
            {
                return this.fechaEmbarqueField;
            }
            set
            {
                this.fechaEmbarqueField = value;
            }
        }

        /// <comentarios/>
        public string fue
        {
            get
            {
                return this.fueField;
            }
            set
            {
                this.fueField = value;
            }
        }

        /// <comentarios/>
        public decimal valorFOB
        {
            get
            {
                return this.valorFOBField;
            }
            set
            {
                this.valorFOBField = value;
            }
        }

        /// <comentarios/>
        public decimal valorFOBComprobante
        {
            get
            {
                return this.valorFOBComprobanteField;
            }
            set
            {
                this.valorFOBComprobanteField = value;
            }
        }

        /// <comentarios/>
        public string establecimiento
        {
            get
            {
                return this.establecimientoField;
            }
            set
            {
                this.establecimientoField = value;
            }
        }

        /// <comentarios/>
        public string puntoEmision
        {
            get
            {
                return this.puntoEmisionField;
            }
            set
            {
                this.puntoEmisionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secuencial
        {
            get
            {
                return this.secuencialField;
            }
            set
            {
                this.secuencialField = value;
            }
        }

        /// <comentarios/>
        public string autorizacion
        {
            get
            {
                return this.autorizacionField;
            }
            set
            {
                this.autorizacionField = value;
            }
        }

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
    }

}
