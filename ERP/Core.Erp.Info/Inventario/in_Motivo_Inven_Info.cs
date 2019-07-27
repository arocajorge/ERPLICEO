using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Motivo_Inven_Info
    {
        public int IdEmpresa { get; set; }
        public int IdMotivo_Inv { get; set; }
        [StringLength(20, MinimumLength = 1, ErrorMessage = ("El campo código debe tener máximo 20 caracteres"))]
        public string Cod_Motivo_Inv { get; set; }

        [Required(ErrorMessage = ("El campo descripción es obligatorio"))]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ("El campo descripción debe tener mínimo 1 caracter máximo 50"))]
        public string Desc_mov_inv { get; set; }
        public string Genera_Movi_Inven { get; set; }
        public string estado { get; set; }
        public bool EstadoBool { get; set; }
        public string Tipo_Ing_Egr { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaHoraAnul { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public string IdCtaCble { get; set; }
        //Campos que no existen en la tabla

        public bool Genera_Movi_Inven_bool { get; set; }

    }
}
