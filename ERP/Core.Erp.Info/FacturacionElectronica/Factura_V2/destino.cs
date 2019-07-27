using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Erp.Info.FacturacionElectronica.Factura_V2
{
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class destino
    {

        private string motivoTrasladoField;

        private string docAduaneroUnicoField;

        private string codEstabDestinoField;

        private string rutaField;

        /// <comentarios/>
        public string motivoTraslado
        {
            get
            {
                return this.motivoTrasladoField;
            }
            set
            {
                this.motivoTrasladoField = value;
            }
        }

        /// <comentarios/>
        public string docAduaneroUnico
        {
            get
            {
                return this.docAduaneroUnicoField;
            }
            set
            {
                this.docAduaneroUnicoField = value;
            }
        }

        /// <comentarios/>
        public string codEstabDestino
        {
            get
            {
                return this.codEstabDestinoField;
            }
            set
            {
                this.codEstabDestinoField = value;
            }
        }

        /// <comentarios/>
        public string ruta
        {
            get
            {
                return this.rutaField;
            }
            set
            {
                this.rutaField = value;
            }
        }
    }

}
