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
    public partial class detalleAnulados
    {

        private string tipoComprobanteField;

        private string establecimientoField;

        private string puntoEmisionField;

        private string secuencialInicioField;

        private string secuencialFinField;

        private string autorizacionField;

        /// <comentarios/>
        public string tipoComprobante
        {
            get
            {
                return this.tipoComprobanteField;
            }
            set
            {
                this.tipoComprobanteField = value;
            }
        }

        /// <comentarios/>
        public string establecimiento
        {
            get
            {
                return this.establecimientoField;
            }
            set
            {
                this.establecimientoField = value;
            }
        }

        /// <comentarios/>
        public string puntoEmision
        {
            get
            {
                return this.puntoEmisionField;
            }
            set
            {
                this.puntoEmisionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secuencialInicio
        {
            get
            {
                return this.secuencialInicioField;
            }
            set
            {
                this.secuencialInicioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string secuencialFin
        {
            get
            {
                return this.secuencialFinField;
            }
            set
            {
                this.secuencialFinField = value;
            }
        }

        /// <comentarios/>
        public string autorizacion
        {
            get
            {
                return this.autorizacionField;
            }
            set
            {
                this.autorizacionField = value;
            }
        }
    }

}
