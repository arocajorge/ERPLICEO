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
    public enum pagoLocExtType
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    public enum tipoRegiType
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    public enum aplicConvDobTribType
    {

        /// <comentarios/>
        SI,

        /// <comentarios/>
        NO,

        /// <comentarios/>
        NA,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class detalleAirRen
    {

        private string codRetAirField;

        private decimal depositoField;

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
        public decimal deposito
        {
            get
            {
                return this.depositoField;
            }
            set
            {
                this.depositoField = value;
            }
        }

        /// <comentarios/>
        public decimal baseImpAir
        {
            get
            {
                return this.baseImpAirField;
            }
            set
            {
                this.baseImpAirField = value;
            }
        }

        /// <comentarios/>
        public decimal porcentajeAir
        {
            get
            {
                return this.porcentajeAirField;
            }
            set
            {
                this.porcentajeAirField = value;
            }
        }

        /// <comentarios/>
        public decimal valRetAir
        {
            get
            {
                return this.valRetAirField;
            }
            set
            {
                this.valRetAirField = value;
            }
        }
    }



}
