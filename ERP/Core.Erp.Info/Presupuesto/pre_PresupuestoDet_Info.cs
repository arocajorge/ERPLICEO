using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Presupuesto
{
    public class pre_PresupuestoDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdPresupuesto { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = ("El campo rubro es obligatorio"))]
        public int IdRubro { get; set; }        
        public string IdCtaCble { get; set; }
        [Required(ErrorMessage = ("El campo monto es obligatorio"))]
        public double Monto { get; set; }


        public string Descripcion { get; set; }
        public decimal IdPeriodo { get; set; }
        public bool EstadoCierre { get; set; }
    }
}
