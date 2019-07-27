using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_parametros_Info
    {
        public int IdEmpresa { get; set; }
        public string CiudadDefaultParaCrearCheques { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public Nullable<int> CantidadChequesAlerta { get; set; }
        public bool PermitirSobreGiro { get; set; }
        public bool ValidarSoloCuentasArchivo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaTransac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
    }
}
