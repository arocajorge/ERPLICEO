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
    public partial class detalleRendFinancierosType
    {

        private string retenidoField;

        private string idRetenidoField;

        private aplicConvDobTribType parteRelFidField;

        private bool parteRelFidFieldSpecified;

        private string tipoReteField;

        private string denoBenefiField;

        private detalleRendFinancierosTypeAhorroPN ahorroPNField;

        private detalleRendFinancierosTypeCtaExenta ctaExentaField;

        private List<detalleRendFinancierosTypeDetRet> retencionesField;

        /// <comentarios/>
        public string retenido
        {
            get
            {
                return this.retenidoField;
            }
            set
            {
                this.retenidoField = value;
            }
        }

        /// <comentarios/>
        public string idRetenido
        {
            get
            {
                return this.idRetenidoField;
            }
            set
            {
                this.idRetenidoField = value;
            }
        }

        /// <comentarios/>
        public aplicConvDobTribType parteRelFid
        {
            get
            {
                return this.parteRelFidField;
            }
            set
            {
                this.parteRelFidField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool parteRelFidSpecified
        {
            get
            {
                return this.parteRelFidFieldSpecified;
            }
            set
            {
                this.parteRelFidFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string tipoRete
        {
            get
            {
                return this.tipoReteField;
            }
            set
            {
                this.tipoReteField = value;
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
        public detalleRendFinancierosTypeAhorroPN ahorroPN
        {
            get
            {
                return this.ahorroPNField;
            }
            set
            {
                this.ahorroPNField = value;
            }
        }

        /// <comentarios/>
        public detalleRendFinancierosTypeCtaExenta ctaExenta
        {
            get
            {
                return this.ctaExentaField;
            }
            set
            {
                this.ctaExentaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("detRet", IsNullable = false)]
        public List<detalleRendFinancierosTypeDetRet> retenciones
        {
            get
            {
                return this.retencionesField;
            }
            set
            {
                this.retencionesField = value;
            }
        }
    }

}
