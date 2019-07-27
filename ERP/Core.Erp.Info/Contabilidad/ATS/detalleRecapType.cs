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
    public partial class detalleRecapType
    {

        private string establecimientoRecapField;

        private string identificacionRecapField;

        private parteRelType parteRelRecField;

        private bool parteRelRecFieldSpecified;

        private string tipoEstField;

        private string denoCliRecapsField;

        private string tipoComprobanteField;

        private string numeroRecapField;

        private string fechaPagoField;

        private string tarjetaCreditoField;

        private string fechaEmisionRecapField;

        private decimal consumoCeroField;

        private decimal consumoGravadoField;

        private decimal totalConsumoField;

        private decimal montoIvaField;

        private List<compensacion> compensacionesField;

        private decimal comisionField;

        private string numeroVouchersField;

        private decimal valRetBien10Field;

        private bool valRetBien10FieldSpecified;

        private decimal valRetServ20Field;

        private bool valRetServ20FieldSpecified;

        private decimal valorRetBienesField;

        private decimal valRetServ50Field;

        private bool valRetServ50FieldSpecified;

        private decimal valorRetServiciosField;

        private decimal valRetServ100Field;

        private pagoExterior pagoExteriorField;

        private List<detalleAir> airField;

        private string establecimientoField;

        private string puntoEmisionField;

        private string secuencialField;

        private string autorizacionField;

        private string fechaEmisionField;

        /// <comentarios/>
        public string establecimientoRecap
        {
            get
            {
                return this.establecimientoRecapField;
            }
            set
            {
                this.establecimientoRecapField = value;
            }
        }

        /// <comentarios/>
        public string identificacionRecap
        {
            get
            {
                return this.identificacionRecapField;
            }
            set
            {
                this.identificacionRecapField = value;
            }
        }

        /// <comentarios/>
        public parteRelType parteRelRec
        {
            get
            {
                return this.parteRelRecField;
            }
            set
            {
                this.parteRelRecField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool parteRelRecSpecified
        {
            get
            {
                return this.parteRelRecFieldSpecified;
            }
            set
            {
                this.parteRelRecFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string tipoEst
        {
            get
            {
                return this.tipoEstField;
            }
            set
            {
                this.tipoEstField = value;
            }
        }

        /// <comentarios/>
        public string denoCliRecaps
        {
            get
            {
                return this.denoCliRecapsField;
            }
            set
            {
                this.denoCliRecapsField = value;
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
        public string numeroRecap
        {
            get
            {
                return this.numeroRecapField;
            }
            set
            {
                this.numeroRecapField = value;
            }
        }

        /// <comentarios/>
        public string fechaPago
        {
            get
            {
                return this.fechaPagoField;
            }
            set
            {
                this.fechaPagoField = value;
            }
        }

        /// <comentarios/>
        public string tarjetaCredito
        {
            get
            {
                return this.tarjetaCreditoField;
            }
            set
            {
                this.tarjetaCreditoField = value;
            }
        }

        /// <comentarios/>
        public string fechaEmisionRecap
        {
            get
            {
                return this.fechaEmisionRecapField;
            }
            set
            {
                this.fechaEmisionRecapField = value;
            }
        }

        /// <comentarios/>
        public decimal consumoCero
        {
            get
            {
                return this.consumoCeroField;
            }
            set
            {
                this.consumoCeroField = value;
            }
        }

        /// <comentarios/>
        public decimal consumoGravado
        {
            get
            {
                return this.consumoGravadoField;
            }
            set
            {
                this.consumoGravadoField = value;
            }
        }

        /// <comentarios/>
        public decimal totalConsumo
        {
            get
            {
                return this.totalConsumoField;
            }
            set
            {
                this.totalConsumoField = value;
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
        public decimal comision
        {
            get
            {
                return this.comisionField;
            }
            set
            {
                this.comisionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string numeroVouchers
        {
            get
            {
                return this.numeroVouchersField;
            }
            set
            {
                this.numeroVouchersField = value;
            }
        }

        /// <comentarios/>
        public decimal valRetBien10
        {
            get
            {
                return this.valRetBien10Field;
            }
            set
            {
                this.valRetBien10Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valRetBien10Specified
        {
            get
            {
                return this.valRetBien10FieldSpecified;
            }
            set
            {
                this.valRetBien10FieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal valRetServ20
        {
            get
            {
                return this.valRetServ20Field;
            }
            set
            {
                this.valRetServ20Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valRetServ20Specified
        {
            get
            {
                return this.valRetServ20FieldSpecified;
            }
            set
            {
                this.valRetServ20FieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal valorRetBienes
        {
            get
            {
                return this.valorRetBienesField;
            }
            set
            {
                this.valorRetBienesField = value;
            }
        }

        /// <comentarios/>
        public decimal valRetServ50
        {
            get
            {
                return this.valRetServ50Field;
            }
            set
            {
                this.valRetServ50Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valRetServ50Specified
        {
            get
            {
                return this.valRetServ50FieldSpecified;
            }
            set
            {
                this.valRetServ50FieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal valorRetServicios
        {
            get
            {
                return this.valorRetServiciosField;
            }
            set
            {
                this.valorRetServiciosField = value;
            }
        }

        /// <comentarios/>
        public decimal valRetServ100
        {
            get
            {
                return this.valRetServ100Field;
            }
            set
            {
                this.valRetServ100Field = value;
            }
        }

        /// <comentarios/>
        public pagoExterior pagoExterior
        {
            get
            {
                return this.pagoExteriorField;
            }
            set
            {
                this.pagoExteriorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("detalleAir", IsNullable = false)]
        public List<detalleAir> air
        {
            get
            {
                return this.airField;
            }
            set
            {
                this.airField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
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
