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
    public partial class compensacion
    {
        
        private string tipoCompeField;

        private decimal montoField;

        /// <comentarios/>
        public string tipoCompe
        {
            get
            {
                return this.tipoCompeField;
            }
            set
            {
                this.tipoCompeField = value;
            }
        }

        /// <comentarios/>
        public decimal monto
        {
            get
            {
                return this.montoField;
            }
            set
            {
                this.montoField = value;
            }
        }
    }

    
}
