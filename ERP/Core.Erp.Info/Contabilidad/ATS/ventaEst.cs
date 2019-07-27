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
    public partial class ventaEst
    {

        private string codEstabField;

        private decimal ventasEstabField;

        private decimal ivaCompField;

        private bool ivaCompFieldSpecified;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string codEstab
        {
            get
            {
                return this.codEstabField;
            }
            set
            {
                this.codEstabField = value;
            }
        }

        /// <comentarios/>
        public decimal ventasEstab
        {
            get
            {
                return this.ventasEstabField;
            }
            set
            {
                this.ventasEstabField = value;
            }
        }

        /// <comentarios/>
        public decimal ivaComp
        {
            get
            {
                return this.ivaCompField;
            }
            set
            {
                this.ivaCompField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ivaCompSpecified
        {
            get
            {
                return this.ivaCompFieldSpecified;
            }
            set
            {
                this.ivaCompFieldSpecified = value;
            }
        }
    }

}
