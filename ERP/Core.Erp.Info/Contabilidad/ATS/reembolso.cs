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
    public enum tipoEmisionType
    {

        /// <comentarios/>
        E,

        /// <comentarios/>
        F,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class reembolso
    {

        private string tipoComprobanteReembField;

        private string tpIdProvReembField;

        private string idProvReembField;

        private string establecimientoReembField;

        private string puntoEmisionReembField;

        private string secuencialReembField;

        private string fechaEmisionReembField;

        private string autorizacionReembField;

        private decimal baseImponibleReembField;

        private decimal baseImpGravReembField;

        private decimal baseNoGraIvaReembField;

        private decimal baseImpExeReembField;

        private decimal montoIceRembField;

        private decimal montoIvaRembField;

        /// <comentarios/>
        public string tipoComprobanteReemb
        {
            get
            {
                return this.tipoComprobanteReembField;
            }
            set
            {
                this.tipoComprobanteReembField = value;
            }
        }

        /// <comentarios/>
        public string tpIdProvReemb
        {
            get
            {
                return this.tpIdProvReembField;
            }
            set
            {
                this.tpIdProvReembField = value;
            }
        }

        /// <comentarios/>
        public string idProvReemb
        {
            get
            {
                return this.idProvReembField;
            }
            set
            {
                this.idProvReembField = value;
            }
        }

        /// <comentarios/>
        public string establecimientoReemb
        {
            get
            {
                return this.establecimientoReembField;
            }
            set
            {
                this.establecimientoReembField = value;
            }
        }

        /// <comentarios/>
        public string puntoEmisionReemb
        {
            get
            {
                return this.puntoEmisionReembField;
            }
            set
            {
                this.puntoEmisionReembField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secuencialReemb
        {
            get
            {
                return this.secuencialReembField;
            }
            set
            {
                this.secuencialReembField = value;
            }
        }

        /// <comentarios/>
        public string fechaEmisionReemb
        {
            get
            {
                return this.fechaEmisionReembField;
            }
            set
            {
                this.fechaEmisionReembField = value;
            }
        }

        /// <comentarios/>
        public string autorizacionReemb
        {
            get
            {
                return this.autorizacionReembField;
            }
            set
            {
                this.autorizacionReembField = value;
            }
        }

        /// <comentarios/>
        public decimal baseImponibleReemb
        {
            get
            {
                return this.baseImponibleReembField;
            }
            set
            {
                this.baseImponibleReembField = value;
            }
        }

        /// <comentarios/>
        public decimal baseImpGravReemb
        {
            get
            {
                return this.baseImpGravReembField;
            }
            set
            {
                this.baseImpGravReembField = value;
            }
        }

        /// <comentarios/>
        public decimal baseNoGraIvaReemb
        {
            get
            {
                return this.baseNoGraIvaReembField;
            }
            set
            {
                this.baseNoGraIvaReembField = value;
            }
        }

        /// <comentarios/>
        public decimal baseImpExeReemb
        {
            get
            {
                return this.baseImpExeReembField;
            }
            set
            {
                this.baseImpExeReembField = value;
            }
        }

        /// <comentarios/>
        public decimal montoIceRemb
        {
            get
            {
                return this.montoIceRembField;
            }
            set
            {
                this.montoIceRembField = value;
            }
        }

        /// <comentarios/>
        public decimal montoIvaRemb
        {
            get
            {
                return this.montoIvaRembField;
            }
            set
            {
                this.montoIvaRembField = value;
            }
        }
    }

}
