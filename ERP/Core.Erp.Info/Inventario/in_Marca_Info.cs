using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Marca_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdMarca { get; set; }
        [Required(ErrorMessage = ("El campo descripción es obligatorio"))]
        [StringLength(100, MinimumLength =1, ErrorMessage =("El campo descripción debe tener mínimo 1 caracter máximo 100"))]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }

        #region Campos auditoria
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        #endregion

        #region Campos que no existen en la tabla
        public bool Seleccionado { get; set; }
        #endregion
    }
}
