using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_005_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdActaFiniquito { get; set; }
        public decimal IdEmpleado { get; set; }
        public string IdCausaTerminacion { get; set; }
        public decimal IdContrato { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public System.DateTime FechaSalida { get; set; }
        public double UltimaRemuneracion { get; set; }
        public string Observacion { get; set; }
        public Nullable<double> Ingresos { get; set; }
        public Nullable<double> Egresos { get; set; }
        public bool EsMujerEmbarazada { get; set; }
        public bool EsDirigenteSindical { get; set; }
        public bool EsPorDiscapacidad { get; set; }
        public bool EsPorEnfermedadNoProfesional { get; set; }
        public int IdSecuencia { get; set; }
        public string DescripcionDetalle { get; set; }
        public double Valor { get; set; }
        public Nullable<int> IdCargo { get; set; }
        public string NumDocumento { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string ca_descripcion { get; set; }
        public string ru_descripcion { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoTerminacion { get; set; }
        public string Su_Descripcion { get; set; }
        public double liquido { get; set; }
    }
}
