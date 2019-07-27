using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Banco
{
    public class ba_TipoFlujo_PlantillaDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdPlantilla { get; set; }
        public int Secuencia { get; set; }
        public decimal IdTipoFlujo { get; set; }
        public double Porcentaje { get; set; }


        public string Descricion { get; set; }
    }
}
