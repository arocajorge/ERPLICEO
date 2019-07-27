using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Helps
{
    public class cl_filtros_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha inicio debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_ini { get; set; }
        [Required(ErrorMessage = "El campo fecha fin es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha fin debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_fin { get; set; }
        public string IdCtaCble { get; set; }
        public string IdCtaCbleFin { get; set; }
        public bool mostrarAnulados { get; set; }
        public bool mostrar_observacion_completa { get; set; }
        public int IdBodega { get; set; }
        public string CodDocumentoTipo { get; set; }

        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }

        #region Filtros activo
        public int IdActivoFijoTipo { get; set; }
        public int IdCategoriaAF { get; set; }
        public string Estado_Proceso { get; set; }
        public string IdUsuario { get; set; }
        public DateTime fecha { get; set; }
        public int IdActivoFijo { get; set; }
        public decimal IdAreaAF { get; set; }

        #endregion

        #region Filtros cuentas por pagar
        public decimal IdProveedor { get; set; }
        public bool mostrar_agrupado { get; set; }
        public bool no_mostrar_en_conciliacion { get; set; }
        public bool no_mostrar_saldo_en_0 { get; set; }
        public string IdTipoServicio { get; set; }

        public int[] IntArray { get; set; }
        public bool mostrarSaldo0 { get; set; }
        #endregion

        #region filtros de RRHH
        public int IdTipoNomina { get; set; }
        public int IdNomina { get; set; }
        public int IdSucursal { get; set; }
        public decimal? IdEmpleado { get; set; }
        public string[]  estado_novedad { get; set; }
        public string IdRubro { get; set; }
        public int IdNominaTipoLiqui { get; set; }
        public string TipoRubro { get; set; }
        public int IdArea { get; set; }
        public int IdDivision { get; set; }
        public int IdDepartamento { get; set; }
        public int IdProceso { get; set; }
        public string em_status { get; set; }
        public string Ubicacion { get; set; }
        public int IdAnio { get; set; }
        public string IdSigno { get; set; }
        public double Valor { get; set; }
        #endregion

        #region filtros de Presupuesto
        public decimal IdPeriodo { get; set; }
        public decimal IdPeriodoSet { get; set; }
        #endregion

        public cl_filtros_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }

    }

    public class cl_filtros_caja_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha inicio debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_ini { get; set; }
        [Required(ErrorMessage = "El campo fecha fin es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha fin debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_fin { get; set; }
        public int IdCaja { get; set; }
        public cl_filtros_caja_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }
    }

    public class cl_filtros_importacion_Info
    {
        public DateTime fecha_fin { get; set; }
        public DateTime fecha_ini { get; set; }
        public int IdEmpresa { get; set; }
        public int IdMarca { get; set; }
        public string IdPais_embarque { get; set; }
        public decimal? IdProductoPadre { get; set; }
        public decimal? IdProveedor { get; set; }

        public cl_filtros_importacion_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }
    }

    public class cl_filtros_inventario_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal? IdProducto { get; set; }
        public int IdMarca { get; set; }
        public string IdCategoria { get; set; }
        public int IdLinea { get; set; }
        public int IdGrupo { get; set; }
        public int IdSubGrupo { get; set; }
        public DateTime fecha_ini { get; set; }
        public DateTime fecha_fin { get; set; }
        public bool mostrar_detallado { get; set; }
        public decimal? IdProductoPadre { get; set; }
        public bool mostrarSinMovimiento { get; set; }
        public bool no_mostrar_valores_en_0 { get; set; }
        public bool mostrar_saldos_en_0 { get; set; }
        public bool mostrar_agrupado { get; set; }
        public string IdUsuario { get; set; }
        public int dIAS { get; set; }
        public cl_filtros_inventario_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }
    }
    public class cl_filtros_banco_Info
    {
        public int[] IntArray { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        public decimal? IdPersona { get; set; }
        public string Estado { get; set; }
        public DateTime fecha_ini { get; set; }
        public DateTime fecha_fin { get; set; }
        public int IdSucursal { get; set; }
        public string IdUsuario { get; set; }
        public cl_filtros_banco_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }
        public int NumDesde { get; set; }
        public int NumHasta { get; set; }
        public bool mostrar_agrupado { get; set; }
        public bool mostrarSaldo0 { get; set; }
    }

    public class cl_filtros_contabilidad_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdNivel { get; set; }
        public string balance { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha inicio debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_ini { get; set; }
        [Required(ErrorMessage = "El campo fecha fin es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha fin debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_fin { get; set; }
        public bool mostrar_saldos_en_0 { get; set; }
        public bool MostrarSaldoAcumulado { get; set; }
        public int IdSucursal { get; set; }
        public int IdPunto_cargo_grupo { get; set; }
        public string IdUsuario { get; set; }
        public int IdPeriodoIni { get; set; }
        public int IdPeriodoFin { get; set; }
        public cl_filtros_contabilidad_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }

        public int[] IntArray { get; set; }
    }

    public class cl_filtros_facturacion_Info
    {
        public int IdEmpresa { get; set; }
        public DateTime fecha_fin { get; set; }
        public decimal? IdProducto { get; set; }
        public decimal? IdCliente { get; set; }
        public int IdClienteContacto { get; set; }
        public int IdVendedor { get; set; }
        public decimal? IdProductoPadre { get; set; }        
        public decimal? IdEntidad { get; set; }
        public DateTime fecha_ini { get; set; }
        public int IdSucursal { get; set; }
        public bool Check1 { get; set; }
        public bool Check2 { get; set; }
        public DateTime fecha_corte { get; set; }
        public int IdContacto { get; set; }

        public decimal IdProforma { get; set; }
        public bool formato_hoja_membretada { get; set; }

        public int IdMarca { get; set; }
        public string IdCategoria { get; set; }
        public int IdLinea { get; set; }
        public int IdGrupo { get; set; }
        public int IdSubGrupo { get; set; }
        public bool mostrarSaldo0 { get; set; }
        public bool mostrarSoloVencido { get; set; }

        public decimal IdLiquidacion { get; set; }
        public bool mostrarAnulados { get; set; }
        public bool mostrar_observacion_completa { get; set; }
        public int Idtipo_cliente { get; set; }
        public string IdCatalogo_FormaPago { get; set; }

        public int IdAnio { get; set; }
        public string IdCobro_tipo { get; set; }
        public int[] IntArray { get; set; }
        public int IdTipoNota { get; set; }
        public string CreDeb { get; set; }
        public cl_filtros_facturacion_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
            fecha_corte = DateTime.Now.Date;
            IdCatalogo_FormaPago = "";
        }
    }

}
