using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad.ATS
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(detalleAirCompras))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class detalleAir
    {

        private string codRetAirField;

        private decimal baseImpAirField;

        private decimal porcentajeAirField;

        private decimal valRetAirField;

        /// <comentarios/>
        public string codRetAir
        {
            get
            {
                return this.codRetAirField;
            }
            set
            {
                this.codRetAirField = value;
            }
        }

        /// <comentarios/>
        public string baseImpAir
        {
            get
            {
                return this.baseImpAirField.ToString("0.00");
            }
            set
            {
                this.baseImpAirField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string porcentajeAir
        {
            get
            {
                return this.porcentajeAirField.ToString("0.00");
            }
            set
            {
                this.porcentajeAirField = Convert.ToDecimal(value);
            }
        }

        /// <comentarios/>
        public string valRetAir
        {
            get
            {
                return this.valRetAirField.ToString("0.00");
            }
            set
            {
                this.valRetAirField = Convert.ToDecimal(value);
            }
        }
    }
 
}
