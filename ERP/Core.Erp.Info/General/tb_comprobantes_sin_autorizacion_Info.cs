using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
  public  class tb_comprobantes_sin_autorizacion_Info
    {
        public int IdEmpresa { get; set; }
        public string Tipo_documento { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_serie1 { get; set; }
        public string vt_serie2 { get; set; }
        public string DocumentoDoc { get; set; }
        public string Documento { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string vt_Observacion { get; set; }

        public bool seleccionado { get; set; }

        public int secuencia { get; set; }
    }
}
