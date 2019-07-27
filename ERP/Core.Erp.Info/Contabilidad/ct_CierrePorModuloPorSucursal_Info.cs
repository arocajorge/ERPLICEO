using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_CierrePorModuloPorSucursal_Info
    {
        public int IdEmpresa { get; set; }
        public int IdCierre { get; set; }
        [Required(ErrorMessage = ("El campo sucursal obligatorio"))]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = ("El campo módulo obligatorio"))]
        public string CodModulo { get; set; }
        [Required(ErrorMessage = ("El campo fecha inicio obligatorio"))]
        public System.DateTime FechaIni { get; set; }
        [Required(ErrorMessage = ("El campo fecha fin obligatorio"))]
        public System.DateTime FechaFin { get; set; }
        public bool Cerrado { get; set; }

        #region Campos que no existen en la tabla
        public string Descripcion { get; set; }
        public string Su_Descripcion { get; set; }
        #endregion
    }
}
