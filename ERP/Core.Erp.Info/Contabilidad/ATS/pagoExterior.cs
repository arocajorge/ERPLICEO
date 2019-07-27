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
    public enum parteRelType
    {

        /// <comentarios/>
        SI,

        /// <comentarios/>
        NO,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class pagoExterior
    {

        private pagoLocExtType pagoLocExtField;

        private tipoRegiType tipoRegiField;

        private bool tipoRegiFieldSpecified;

        private string paisEfecPagoGenField;

        private string paisEfecPagoParFisField;

        private string denopagoRegFisField;

        private string paisEfecPagoField;

        private aplicConvDobTribType aplicConvDobTribField;

        private aplicConvDobTribType pagExtSujRetNorLegField;

        private aplicConvDobTribType pagoRegFisField;

        private bool pagoRegFisFieldSpecified;

        /// <comentarios/>
        public pagoLocExtType pagoLocExt
        {
            get
            {
                return this.pagoLocExtField;
            }
            set
            {
                this.pagoLocExtField = value;
            }
        }

        /// <comentarios/>
        public tipoRegiType tipoRegi
        {
            get
            {
                return this.tipoRegiField;
            }
            set
            {
                this.tipoRegiField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tipoRegiSpecified
        {
            get
            {
                return this.tipoRegiFieldSpecified;
            }
            set
            {
                this.tipoRegiFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string paisEfecPagoGen
        {
            get
            {
                return this.paisEfecPagoGenField;
            }
            set
            {
                this.paisEfecPagoGenField = value;
            }
        }

        /// <comentarios/>
        public string paisEfecPagoParFis
        {
            get
            {
                return this.paisEfecPagoParFisField;
            }
            set
            {
                this.paisEfecPagoParFisField = value;
            }
        }

        /// <comentarios/>
        public string denopagoRegFis
        {
            get
            {
                return this.denopagoRegFisField;
            }
            set
            {
                this.denopagoRegFisField = value;
            }
        }

        /// <comentarios/>
        public string paisEfecPago
        {
            get
            {
                return this.paisEfecPagoField;
            }
            set
            {
                this.paisEfecPagoField = value;
            }
        }

        /// <comentarios/>
        public aplicConvDobTribType aplicConvDobTrib
        {
            get
            {
                return this.aplicConvDobTribField;
            }
            set
            {
                this.aplicConvDobTribField = value;
            }
        }

        /// <comentarios/>
        public aplicConvDobTribType pagExtSujRetNorLeg
        {
            get
            {
                return this.pagExtSujRetNorLegField;
            }
            set
            {
                this.pagExtSujRetNorLegField = value;
            }
        }

        /// <comentarios/>
        public aplicConvDobTribType pagoRegFis
        {
            get
            {
                return this.pagoRegFisField;
            }
            set
            {
                this.pagoRegFisField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool pagoRegFisSpecified
        {
            get
            {
                return this.pagoRegFisFieldSpecified;
            }
            set
            {
                this.pagoRegFisFieldSpecified = value;
            }
        }
    }


    
}
