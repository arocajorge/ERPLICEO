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
    public partial class detalleAirCompras : detalleAir
    {

        private string fechaPagoDivField;

        private decimal imRentaSocField;

        private bool imRentaSocFieldSpecified;

        private string anioUtDivField;

        private decimal numCajBanField;

        private bool numCajBanFieldSpecified;

        private decimal precCajBanField;

        private bool precCajBanFieldSpecified;

        /// <comentarios/>
        public string fechaPagoDiv
        {
            get
            {
                return this.fechaPagoDivField;
            }
            set
            {
                this.fechaPagoDivField = value;
            }
        }

        /// <comentarios/>
        public decimal imRentaSoc
        {
            get
            {
                return this.imRentaSocField;
            }
            set
            {
                this.imRentaSocField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool imRentaSocSpecified
        {
            get
            {
                return this.imRentaSocFieldSpecified;
            }
            set
            {
                this.imRentaSocFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string anioUtDiv
        {
            get
            {
                return this.anioUtDivField;
            }
            set
            {
                this.anioUtDivField = value;
            }
        }

        /// <comentarios/>
        public decimal numCajBan
        {
            get
            {
                return this.numCajBanField;
            }
            set
            {
                this.numCajBanField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool numCajBanSpecified
        {
            get
            {
                return this.numCajBanFieldSpecified;
            }
            set
            {
                this.numCajBanFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal precCajBan
        {
            get
            {
                return this.precCajBanField;
            }
            set
            {
                this.precCajBanField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool precCajBanSpecified
        {
            get
            {
                return this.precCajBanFieldSpecified;
            }
            set
            {
                this.precCajBanFieldSpecified = value;
            }
        }
    }

}
