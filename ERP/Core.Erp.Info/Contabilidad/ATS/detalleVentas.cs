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
    public partial class detalleVentas
    {

        private string tpIdClienteField;

        private string idClienteField;

        private parteRelType parteRelVtasField;

        private bool parteRelVtasFieldSpecified;

        private string tipoClienteField;

        private string denoCliField;

        private string tipoComprobanteField;

        private tipoEmisionType tipoEmisionField;

        private string numeroComprobantesField;

        private decimal baseNoGraIvaField;

        private decimal baseImponibleField;

        private decimal baseImpGravField;

        private decimal montoIvaField;

        private List<compensacion> compensacionesField;

        private decimal montoIceField;

        private bool montoIceFieldSpecified;

        private decimal valorRetIvaField;

        private decimal valorRetRentaField;

        private string[] formasDePagoField;

        /// <comentarios/>
        public string tpIdCliente
        {
            get
            {
                return this.tpIdClienteField;
            }
            set
            {
                this.tpIdClienteField = value;
            }
        }

        /// <comentarios/>
        public string idCliente
        {
            get
            {
                return this.idClienteField;
            }
            set
            {
                this.idClienteField = value;
            }
        }

        /// <comentarios/>
        public parteRelType parteRelVtas
        {
            get
            {
                return this.parteRelVtasField;
            }
            set
            {
                this.parteRelVtasField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool parteRelVtasSpecified
        {
            get
            {
                return this.parteRelVtasFieldSpecified;
            }
            set
            {
                this.parteRelVtasFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string tipoCliente
        {
            get
            {
                return this.tipoClienteField;
            }
            set
            {
                this.tipoClienteField = value;
            }
        }

        /// <comentarios/>
        public string denoCli
        {
            get
            {
                return this.denoCliField;
            }
            set
            {
                this.denoCliField = value;
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
        public tipoEmisionType tipoEmision
        {
            get
            {
                return this.tipoEmisionField;
            }
            set
            {
                this.tipoEmisionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string numeroComprobantes
        {
            get
            {
                return this.numeroComprobantesField;
            }
            set
            {
                this.numeroComprobantesField = value;
            }
        }

        /// <comentarios/>
        public decimal baseNoGraIva
        {
            get
            {
                return this.baseNoGraIvaField;
            }
            set
            {
                this.baseNoGraIvaField = value;
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
        public decimal baseImpGrav
        {
            get
            {
                return this.baseImpGravField;
            }
            set
            {
                this.baseImpGravField = value;
            }
        }

        /// <comentarios/>
        public decimal montoIva
        {
            get
            {
                return this.montoIvaField;
            }
            set
            {
                this.montoIvaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("compensacion", IsNullable = false)]
        public List<compensacion> compensaciones
        {
            get
            {
                return this.compensacionesField;
            }
            set
            {
                this.compensacionesField = value;
            }
        }

        /// <comentarios/>
        public decimal montoIce
        {
            get
            {
                return this.montoIceField;
            }
            set
            {
                this.montoIceField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool montoIceSpecified
        {
            get
            {
                return this.montoIceFieldSpecified;
            }
            set
            {
                this.montoIceFieldSpecified = value;
            }
        }

        /// <comentarios/>
        /// 
        

        public string valorRetIva
        {
            get
            {
                return this.valorRetIvaField.ToString("0.00");
            }
            set
            {
                this.valorRetIvaField =   Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string valorRetRenta
        {
            get
            {
                return this.valorRetRentaField.ToString("0.00");
            }
            set
            {
                this.valorRetRentaField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("formaPago", IsNullable = false)]
        public string[] formasDePago
        {
            get
            {
                return this.formasDePagoField;
            }
            set
            {
                this.formasDePagoField = value;
            }
        }
    }

}
