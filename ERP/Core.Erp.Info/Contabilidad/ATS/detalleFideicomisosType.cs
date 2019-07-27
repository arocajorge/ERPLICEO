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
    public partial class detalleFideicomisosType
    {

        private string tipoBeneficiarioField;

        private string idBeneficiarioField;

        private parteRelType parteRelExpField;

        private bool parteRelExpFieldSpecified;

        private string tipoBeneficiarioCliField;

        private string denoBenefiField;

        private string rucFideicomisoField;

        private List<detallefValor> fValorField;

        /// <comentarios/>
        public string tipoBeneficiario
        {
            get
            {
                return this.tipoBeneficiarioField;
            }
            set
            {
                this.tipoBeneficiarioField = value;
            }
        }

        /// <comentarios/>
        public string idBeneficiario
        {
            get
            {
                return this.idBeneficiarioField;
            }
            set
            {
                this.idBeneficiarioField = value;
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
        public string tipoBeneficiarioCli
        {
            get
            {
                return this.tipoBeneficiarioCliField;
            }
            set
            {
                this.tipoBeneficiarioCliField = value;
            }
        }

        /// <comentarios/>
        public string denoBenefi
        {
            get
            {
                return this.denoBenefiField;
            }
            set
            {
                this.denoBenefiField = value;
            }
        }

        /// <comentarios/>
        public string rucFideicomiso
        {
            get
            {
                return this.rucFideicomisoField;
            }
            set
            {
                this.rucFideicomisoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("detallefValor", IsNullable = false)]
        public List<detallefValor> fValor
        {
            get
            {
                return this.fValorField;
            }
            set
            {
                this.fValorField = value;
            }
        }
    }

}
