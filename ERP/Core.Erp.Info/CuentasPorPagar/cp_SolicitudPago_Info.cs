using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_SolicitudPago_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdSolicitud { get; set; }
        [Required(ErrorMessage = ("el campo sucursal es obligatorio"))]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = ("el campo fecha es obligatorio"))]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = ("el campo proveedor es obligatorio"))]
        [Range(1, int.MaxValue, ErrorMessage = "El campo proveedor es obligatorio")]
        public decimal IdProveedor { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 1000")]
        public string Concepto { get; set; }
        public bool Estado { get; set; }
        [Required(ErrorMessage = ("el campo valor es obligatorio"))]

        [Range(1, int.MaxValue, ErrorMessage = "El campo valor debe ser mayor a 0")]
        public double Valor { get; set; }
        [Required(ErrorMessage = ("el campo solicitante es obligatorio"))]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "el campo solicitante debe tener mínimo 1 caracter y máximo 1000")]
        public string Solicitante { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        [Required(ErrorMessage = ("el campo girado a es obligatorio"))]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "el campo girado debe tener mínimo 3 caracter y máximo 1000")]
        public string GiradoA { get; set; }
        //campos que no existen en la tabla
        public List<cp_SolicitudPagoDet_Info> lst_det { get; set; }
        public string pe_nombreCompleto { get; set; }

    }
}
