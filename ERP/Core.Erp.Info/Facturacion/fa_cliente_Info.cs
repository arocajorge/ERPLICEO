using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
   public class fa_cliente_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdCliente { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 50")]
        public string Codigo { get; set; }
        public decimal IdPersona { get; set; }
        public int Idtipo_cliente { get; set; }
        public string IdTipoCredito { get; set; }

        [Required(ErrorMessage = "El campo plazo es obligatorio")]
        public int cl_plazo { get; set; }
        [Required(ErrorMessage = "El campo cupo es obligatorio")]
        public double cl_Cupo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdCtaCble_cxc_Credito { get; set; }
        public bool es_empresa_relacionada { get; set; }
        public string FormaPago { get; set; }
        public bool EsClienteExportador { get; set; }
        public int IdNivel { get; set; }

        //Campos que no existen en la tabla
        public tb_persona_Info info_persona { get; set; }
        public string Descripcion_tip_cliente { get; set; }
        public List<fa_cliente_contactos_Info> lst_fa_cliente_contactos { get; set; }
        public List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> Lst_fa_cliente_x_fa_Vendedor_x_sucursal { get; set; }
        public int IdVendedor { get; set; }

        //datos de contacto
        public int IdContacto { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string IdCiudad { get; set; }
        public string Descripcion_Ciudad { get; set; }
        public string IdParroquia { get; set; }
    }
}
