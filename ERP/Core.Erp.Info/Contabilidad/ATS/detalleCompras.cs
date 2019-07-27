using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Erp.Info.Contabilidad.ATS
{
   

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum ivaTypeTipoIDInformante
    {

        /// <comentarios/>
        R,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    public enum codigoOperativoType
    {

        /// <comentarios/>
        IVA,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class detalleCompras
    {

        private string codSustentoField;

        private string tpIdProvField;

        private string idProvField;

        private string tipoComprobanteField;

        private string tipoProvField;

        private string denoProvField;

        private parteRelType parteRelField;

        //private bool parteRelFieldSpecified;

        private string fechaRegistroField;

        private string establecimientoField;

        private string puntoEmisionField;

        private string secuencialField;

        private string fechaEmisionField;

        private string autorizacionField;

        private decimal baseNoGraIvaField;

        private decimal baseImponibleField;

        private decimal baseImpGravField;

        private decimal baseImpExeField;

        private decimal montoIceField;

        private decimal montoIvaField;

        private decimal valRetBien10Field;

        private bool valRetBien10FieldSpecified;

        private decimal valRetServ20Field;

        private bool valRetServ20FieldSpecified;

        private decimal valorRetBienesField;

        private decimal valRetServ50Field;

        private bool valRetServ50FieldSpecified;

        private decimal valorRetServiciosField;

        private decimal valRetServ100Field;

        private decimal totbasesImpReembField;

        private bool totbasesImpReembFieldSpecified;

        private pagoExterior pagoExteriorField;

        private string[] formasDePagoField;

        private List<detalleAir> airField;

        private string estabRetencion1Field;

        private string ptoEmiRetencion1Field;

        private string secRetencion1Field;

        private string autRetencion1Field;

        private string fechaEmiRet1Field;

        private string estabRetencion2Field;

        private string ptoEmiRetencion2Field;

        private string secRetencion2Field;

        private string autRetencion2Field;

        private string fechaEmiRet2Field;

        private string docModificadoField;

        private string estabModificadoField;

        private string ptoEmiModificadoField;

        private string secModificadoField;

        private string autModificadoField;

        private List<reembolso> reembolsosField;

        /// <comentarios/>
        public string codSustento
        {
            get
            {
                return this.codSustentoField;
            }
            set
            {
                this.codSustentoField = value;
            }
        }

        /// <comentarios/>
        public string tpIdProv
        {
            get
            {
                return this.tpIdProvField;
            }
            set
            {
                this.tpIdProvField = value;
            }
        }

        /// <comentarios/>
        public string idProv
        {
            get
            {
                return this.idProvField;
            }
            set
            {
                this.idProvField = value;
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
        public string tipoProv
        {
            get
            {
                return this.tipoProvField;
            }
            set
            {
                this.tipoProvField = value;
            }
        }

        /// <comentarios/>
        public string denoProv
        {
            get
            {
                return this.denoProvField;
            }
            set
            {
                this.denoProvField = value;
            }
        }

        /// <comentarios/>
        public parteRelType parteRel
        {
            get
            {
                return this.parteRelField;
            }
            set
            {
                this.parteRelField = value;
            }
        }

        /// <comentarios/>
        
        //public bool parteRelSpecified
        //{
        //    get
        //    {
        //        return this.parteRelFieldSpecified;
        //    }
        //    set
        //    {
        //        this.parteRelFieldSpecified = value;
        //    }
        //}

        /// <comentarios/>
        public string fechaRegistro
        {
            get
            {
                return this.fechaRegistroField;
            }
            set
            {
                this.fechaRegistroField = value;
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
        public string baseNoGraIva
        {
            get
            {
                return this.baseNoGraIvaField.ToString("0.00");
            }
            set
            {
                this.baseNoGraIvaField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string baseImponible
        {
            get
            {
                return this.baseImponibleField.ToString("0.00");
            }
            set
            {
                this.baseImponibleField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string baseImpGrav
        {
            get
            {
                return this.baseImpGravField.ToString("0.00");
            }
            set
            {
                this.baseImpGravField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string baseImpExe
        {
            get
            {
                return this.baseImpExeField.ToString("0.00");
            }
            set
            {
                this.baseImpExeField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string montoIce
        {
            get
            {
                return this.montoIceField.ToString("0.00");
            }
            set
            {
                this.montoIceField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string montoIva
        {
            get
            {
                return this.montoIvaField.ToString("0.00");
            }
            set
            {
                this.montoIvaField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string valRetBien10
        {
            get
            {
                return this.valRetBien10Field.ToString("0.00");
            }
            set
            {
                this.valRetBien10Field = Convert.ToDecimal(value);
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
        public string valRetServ20
        {
            get
            {
                return this.valRetServ20Field.ToString("0.00");
            }
            set
            {
                this.valRetServ20Field = Convert.ToDecimal( value);
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
        public string valorRetBienes
        {
            get
            {
                return this.valorRetBienesField.ToString("0.00");
            }
            set
            {
                this.valorRetBienesField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string valRetServ50
        {
            get
            {
                return this.valRetServ50Field.ToString("0.00");
            }
            set
            {
                this.valRetServ50Field = Convert.ToDecimal(value);
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
        public string valorRetServicios
        {
            get
            {
                return this.valorRetServiciosField.ToString("0.00");
            }
            set
            {
                this.valorRetServiciosField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string valRetServ100
        {
            get
            {
                return this.valRetServ100Field.ToString("0.00");
            }
            set
            {
                this.valRetServ100Field = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string totbasesImpReemb
        {
            get
            {
                return this.totbasesImpReembField.ToString("0.00");
            }
            set
            {
                this.totbasesImpReembField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totbasesImpReembSpecified
        {
            get
            {
                return this.totbasesImpReembFieldSpecified;
            }
            set
            {
                this.totbasesImpReembFieldSpecified = value;
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
        public string estabRetencion1
        {
            get
            {
                return this.estabRetencion1Field;
            }
            set
            {
                this.estabRetencion1Field = value;
            }
        }

        /// <comentarios/>
        public string ptoEmiRetencion1
        {
            get
            {
                return this.ptoEmiRetencion1Field;
            }
            set
            {
                this.ptoEmiRetencion1Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secRetencion1
        {
            get
            {
                return this.secRetencion1Field;
            }
            set
            {
                this.secRetencion1Field = value;
            }
        }

        /// <comentarios/>
        public string autRetencion1
        {
            get
            {
                return this.autRetencion1Field;
            }
            set
            {
                this.autRetencion1Field = value;
            }
        }

        /// <comentarios/>
        public string fechaEmiRet1
        {
            get
            {
                return this.fechaEmiRet1Field;
            }
            set
            {
                this.fechaEmiRet1Field = value;
            }
        }

        /// <comentarios/>
        public string estabRetencion2
        {
            get
            {
                return this.estabRetencion2Field;
            }
            set
            {
                this.estabRetencion2Field = value;
            }
        }

        /// <comentarios/>
        public string ptoEmiRetencion2
        {
            get
            {
                return this.ptoEmiRetencion2Field;
            }
            set
            {
                this.ptoEmiRetencion2Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secRetencion2
        {
            get
            {
                return this.secRetencion2Field;
            }
            set
            {
                this.secRetencion2Field = value;
            }
        }

        /// <comentarios/>
        public string autRetencion2
        {
            get
            {
                return this.autRetencion2Field;
            }
            set
            {
                this.autRetencion2Field = value;
            }
        }

        /// <comentarios/>
        public string fechaEmiRet2
        {
            get
            {
                return this.fechaEmiRet2Field;
            }
            set
            {
                this.fechaEmiRet2Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string docModificado
        {
            get
            {
                return this.docModificadoField;
            }
            set
            {
                this.docModificadoField = value;
            }
        }

        /// <comentarios/>
        public string estabModificado
        {
            get
            {
                return this.estabModificadoField;
            }
            set
            {
                this.estabModificadoField = value;
            }
        }

        /// <comentarios/>
        public string ptoEmiModificado
        {
            get
            {
                return this.ptoEmiModificadoField;
            }
            set
            {
                this.ptoEmiModificadoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secModificado
        {
            get
            {
                return this.secModificadoField;
            }
            set
            {
                this.secModificadoField = value;
            }
        }

        /// <comentarios/>
        public string autModificado
        {
            get
            {
                return this.autModificadoField;
            }
            set
            {
                this.autModificadoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("reembolso", IsNullable = false)]
        public List<reembolso> reembolsos
        {
            get
            {
                return this.reembolsosField;
            }
            set
            {
                this.reembolsosField = value;
            }
        }
    }


}
