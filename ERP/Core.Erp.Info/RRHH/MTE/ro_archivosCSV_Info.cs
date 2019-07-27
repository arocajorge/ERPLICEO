using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH.MTE
{
   public class ro_archivosCSV_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string CodigoSectorial { get; set; }
        public string ca_descripcion { get; set; }
        public Nullable<double> Valor { get; set; }
        public string pe_sexo { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> em_fechaIngaRol { get; set; }
        public Nullable<int> Dias_Decimo { get; set; }
        public Nullable<int> DiasA_considerar_Decimo { get; set; }
    }
}
