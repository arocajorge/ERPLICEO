using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
    public class Af_Mej_Baj_Activo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal Id_Mejora_Baja_Activo { get; set; }
        public string Id_Tipo { get; set; }
        [Required(ErrorMessage = ("el campo activo es obligatorio"))]
        public int IdActivoFijo { get; set; }
        public string Cod_Mej_Baj_Activo { get; set; }
        [Required(ErrorMessage = ("el campo valor activo es obligatorio"))]
        public double ValorActivo { get; set; }
        [Required(ErrorMessage = ("el campo valor baja es obligatorio"))]
        public double Valor_Tot_Bajas { get; set; }
        [Required(ErrorMessage = ("el campo valor total es obligatorio"))]
        public double Valor_Tot_Mejora { get; set; }
        [Required(ErrorMessage = ("el campo valor depreciacion es obligatorio"))]
        public double Valor_Depre_Acu { get; set; }
        [Required(ErrorMessage = ("el campo valor neto  es obligatorio"))]
        public double Valor_Neto { get; set; }
        [Required(ErrorMessage = ("el campo valor es obligatorio"))]
        public double Valor_Mej_Baj_Activo { get; set; }
        public System.DateTime Fecha_MejBaj { get; set; }
        public string Compr_Mej_Baj { get; set; }
        public string DescripcionTecnica { get; set; }
        public string Motivo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnula { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<int> IdEmpresa_ct { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }

        //Campos que no existen en la tabla
        public List<ct_cbtecble_det_Info> lst_ct_cbtecble_det { get; set; }
    }
}
