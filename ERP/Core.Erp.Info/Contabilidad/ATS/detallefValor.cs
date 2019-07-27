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
    public partial class detallefValor
    {

        private string tipoFideicomisoField;

        private decimal totalFField;

        private decimal individualFField;

        private decimal porRetFField;

        private decimal valorRetFField;

        private string fechaPagoDivField;

        private decimal imRentaSocField;

        private bool imRentaSocFieldSpecified;

        private string anioUtDivField;

        private pagoExterior pagoExteriorField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string tipoFideicomiso
        {
            get
            {
                return this.tipoFideicomisoField;
            }
            set
            {
                this.tipoFideicomisoField = value;
            }
        }

        /// <comentarios/>
        public decimal totalF
        {
            get
            {
                return this.totalFField;
            }
            set
            {
                this.totalFField = value;
            }
        }

        /// <comentarios/>
        public decimal individualF
        {
            get
            {
                return this.individualFField;
            }
            set
            {
                this.individualFField = value;
            }
        }

        /// <comentarios/>
        public decimal porRetF
        {
            get
            {
                return this.porRetFField;
            }
            set
            {
                this.porRetFField = value;
            }
        }

        /// <comentarios/>
        public decimal valorRetF
        {
            get
            {
                return this.valorRetFField;
            }
            set
            {
                this.valorRetFField = value;
            }
        }

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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
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
    }

}
