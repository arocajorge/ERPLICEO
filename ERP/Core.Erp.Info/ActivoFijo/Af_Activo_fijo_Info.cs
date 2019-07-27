using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.ActivoFijo
{
    public class Af_Activo_fijo_Info

    {
        public decimal IdTransaccionSession { get; set; }        
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public string CodActivoFijo { get; set; }
        [Required(ErrorMessage = ("el campo nombre es obligatorio"))]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracter y máximo 500")]
        public string Af_Nombre { get; set; }
        public int IdActivoFijoTipo { get; set; }
        [Required(ErrorMessage = ("el campo catégoria es obligatorio"))]
        public int IdCategoriaAF { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdDepartamento { get; set; }
        public decimal IdArea { get; set; }
        public string IdCatalogo_Marca { get; set; }
        public string IdCatalogo_Modelo { get; set; }
        public string Af_NumSerie { get; set; }
        public string IdCatalogo_Color { get; set; }
        public string IdTipoCatalogo_Ubicacion { get; set; }
        public System.DateTime Af_fecha_compra { get; set; }
        public System.DateTime Af_fecha_ini_depre { get; set; }
        public System.DateTime Af_fecha_fin_depre { get; set; }
        public double Af_costo_compra { get; set; }
        public double Af_Depreciacion_acum { get; set; }
        public int Af_Vida_Util { get; set; }
        public int Af_Meses_depreciar { get; set; }
        public double Af_porcentaje_deprec { get; set; }
        public string Af_observacion { get; set; }
        public string Af_NumPlaca { get; set; }
        public string Estado { get; set; }
        [Required(ErrorMessage = ("el campo empleado encargado es obligatorio"))]
        public decimal IdEmpleadoEncargado { get; set; }
        [Required(ErrorMessage = ("el campo empleado custodio es obligatorio"))]
        public decimal IdEmpleadoCustodio { get; set; }
        public string Af_Codigo_Barra { get; set; }
        public string Estado_Proceso { get; set; }
        public double Af_ValorSalvamento { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = ("el campo cantidad es obligatorio"))]
        public int Cantidad { get; set; }

        //Campos que no existen en la tabla
        public string Estado_Proceso_nombre { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public byte[] imagen_af { get; set; }

    }

    public class Af_Activo_fijo_valores_Info
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public double v_activo { get; set; }
        public double v_depr_acum { get; set; }
        public double v_mejora { get; set; }
        public double v_baja { get; set; }
        public double v_neto { get; set; }



    }
}
