using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_009_Info
    {
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        public string ba_descripcion { get; set; }
        public decimal IdTipoFlujo { get; set; }
        public string NomFlujo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<double> ValorFlujo { get; set; }
    }
}
