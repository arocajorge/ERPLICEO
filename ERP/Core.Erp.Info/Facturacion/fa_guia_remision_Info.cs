using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
   public class fa_guia_remision_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdPuntoVta { get; set; }
        public int IdBodega { get; set; }
        public decimal IdGuiaRemision { get; set; }
        public string CodGuiaRemision { get; set; }
        public string CodDocumentoTipo { get; set; }
      
        [RegularExpression(@"\d{3}", ErrorMessage = "El formato debe ser 000")]
        [Required(ErrorMessage = "El campo establecimiento es obligatorio")]
        public string Serie1 { get; set; }

        [RegularExpression(@"\d{3}", ErrorMessage = "El formato debe ser 000")]
        [Required(ErrorMessage = "El campo punto de emisión es obligatorio")]
        public string Serie2 { get; set; }
        [RegularExpression(@"\d{9}", ErrorMessage = "El formato debe ser 000000000")]
        [Required(ErrorMessage = "El campo número de documento es obligatorio")]
        public string NumGuia_Preimpresa { get; set; }
        public string NUAutorizacion { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizacion { get; set; }

        [Required(ErrorMessage = "El campo cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo transportista es obligatorio")]
        public decimal IdCliente { get; set; }
        [Required(ErrorMessage = "El campo contacto es obligatorio")]

        public decimal IdTransportista { get; set; }
        public System.DateTime gi_fecha { get; set; }
        public Nullable<decimal> gi_plazo { get; set; }
        public Nullable<System.DateTime> gi_fech_venc { get; set; }

        public string gi_Observacion { get; set; }
        public System.DateTime gi_FechaFinTraslado { get; set; }
        public System.DateTime gi_FechaInicioTraslado { get; set; }
        [Required(ErrorMessage = "El campo placa es obligatorio")]
        public string placa { get; set; }
        [Required(ErrorMessage = "El campo dirección origen es obligatorio")]
        public string Direccion_Origen { get; set; }
        [Required(ErrorMessage = "El campo dirección destino es obligatorio")]
        public string Direccion_Destino { get; set; }
        public bool Estado { get; set; }
        public bool aprobada_enviar_sri { get; set; }
        public Nullable<bool> Generado { get; set; }
        public int IdMotivoTraslado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }

        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotiAnula { get; set; }

        #region Campos que no estan en la tabla
        public string IdCatalogo_traslado { get; set; }

        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public List<fa_guia_remision_det_Info> lst_detalle { get; set; }
        public List<fa_factura_x_fa_guia_remision_Info> lst_detalle_x_factura { get; set; }
        #endregion


    }
}
