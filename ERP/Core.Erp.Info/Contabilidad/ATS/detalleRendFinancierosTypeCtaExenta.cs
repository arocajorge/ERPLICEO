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
    public partial class detalleRendFinancierosTypeCtaExenta
    {

        private decimal totalDepField;

        private decimal rendGenField;

        /// <comentarios/>
        public decimal totalDep
        {
            get
            {
                return this.totalDepField;
            }
            set
            {
                this.totalDepField = value;
            }
        }

        /// <comentarios/>
        public decimal rendGen
        {
            get
            {
                return this.rendGenField;
            }
            set
            {
                this.rendGenField = value;
            }
        }
    }

}
