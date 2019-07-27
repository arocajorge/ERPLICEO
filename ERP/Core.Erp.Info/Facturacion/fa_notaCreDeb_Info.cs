using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Facturacion
{
    public class fa_notaCreDeb_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public int IdPuntoVta { get; set; }
        public Nullable<int> dev_IdEmpresa { get; set; }
        public Nullable<decimal> dev_IdDev_Inven { get; set; }
        public string CodNota { get; set; }
        public string CreDeb { get; set; }
        public string CodDocumentoTipo { get; set; }
        public string Serie1 { get; set; }
        public string Serie2 { get; set; }
        public string NumNota_Impresa { get; set; }
        public string NumAutorizacion { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizacion { get; set; }
        public decimal IdCliente { get; set; }
        public System.DateTime no_fecha { get; set; }
        public System.DateTime no_fecha_venc { get; set; }
        public int IdTipoNota { get; set; }
        public string sc_observacion { get; set; }
        public string Estado { get; set; }
        public string NaturalezaNota { get; set; }
        public string IdCtaCble_TipoNota { get; set; }

        // campos que no existen en la tabla

        public string Ruc { get; set; }
        public string Nombres { get; set; }
        public Nullable<double> sc_total { get; set; }
        public Nullable<double> sc_saldo { get; set; }

        #region Campos de auditoria
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        [Required(ErrorMessage = "El campo motivo anulación es obligatorio")]
        public string MotiAnula { get; set; }
        public bool aprobada_enviar_sri { get; set; }

        #endregion

        #region Campos auditoria
        public List<fa_notaCreDeb_det_Info> lst_det { get; set; }
        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> lst_cruce { get; set; }
        #endregion
    }

    public class fa_notaCreDeb_consulta_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public string CreDeb { get; set; }
        public string NumNota_Impresa { get; set; }
        public System.DateTime no_fecha { get; set; }
        public string Nombres { get; set; }
        public Nullable<double> sc_subtotal { get; set; }
        public Nullable<double> sc_iva { get; set; }
        public Nullable<double> sc_total { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }

    }
}
