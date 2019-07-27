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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class detalleRendFinancierosTypeDetRet
    {

        private pagoExterior pagoExteriorField;

        private string estabRetencionField;

        private string ptoEmiRetencionField;

        private string secRetencionField;

        private string autRetencionField;

        private string fechaEmiRetField;

        private List<detalleAirRen> airRendField;

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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string estabRetencion
        {
            get
            {
                return this.estabRetencionField;
            }
            set
            {
                this.estabRetencionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string ptoEmiRetencion
        {
            get
            {
                return this.ptoEmiRetencionField;
            }
            set
            {
                this.ptoEmiRetencionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secRetencion
        {
            get
            {
                return this.secRetencionField;
            }
            set
            {
                this.secRetencionField = value;
            }
        }

        /// <comentarios/>
        public string autRetencion
        {
            get
            {
                return this.autRetencionField;
            }
            set
            {
                this.autRetencionField = value;
            }
        }

        /// <comentarios/>
        public string fechaEmiRet
        {
            get
            {
                return this.fechaEmiRetField;
            }
            set
            {
                this.fechaEmiRetField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("detalleAirRen", IsNullable = false)]
        public List<detalleAirRen> airRend
        {
            get
            {
                return this.airRendField;
            }
            set
            {
                this.airRendField = value;
            }
        }
    }

}
