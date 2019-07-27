using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
    public class Af_Depreciacion_Det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdDepreciacion { get; set; }
        public int Secuencia { get; set; }
        public int IdActivoFijo { get; set; }
        public string Concepto { get; set; }
        public double Valor_Compra { get; set; }
        public double Valor_Salvamento { get; set; }
        public int Vida_Util { get; set; }
        public double Porc_Depreciacion { get; set; }
        public double Valor_Depreciacion { get; set; }
        public double Valor_Depre_Acum { get; set; }

        //Campos que no existen en la table
        public string CodActivoFijo { get; set; }
        public string Af_Nombre { get; set; }
        public Nullable<int> IdActijoFijoTipo { get; set; }
        public string nom_tipo { get; set; }
        public Nullable<int> IdCategoriaAF { get; set; }
        public string nom_categoria { get; set; }
        public string IdCtaCble_Activo { get; set; }
        public string IdCtaCble_Dep_Acum { get; set; }
        public string IdCtaCble_Gastos_Depre { get; set; }
    }
}
