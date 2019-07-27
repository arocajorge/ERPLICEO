using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Caja
{
    public class cp_conciliacion_Caja_det_Data
    {
        public List<cp_conciliacion_Caja_det_Info> get_list(int IdEmpresa, decimal IdConciliacion_caja)
        {
            try
            {
                List<cp_conciliacion_Caja_det_Info> Lista;

                using (Entities_caja Context = new Entities_caja())
                {
                    Lista = (from q in Context.vwcp_conciliacion_Caja_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion_Caja == IdConciliacion_caja
                             select new cp_conciliacion_Caja_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion_Caja = q.IdConciliacion_Caja,
                                 Secuencia = q.Secuencia,
                                 IdEmpresa_OGiro = q.IdEmpresa_OGiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 Valor_a_aplicar = q.Valor_a_aplicar,
                                 Tipo_documento = q.Tipo_documento,
                                 IdEmpresa_OP = q.IdEmpresa_OP,
                                 IdOrdenPago_OP = q.IdOrdenPago_OP,
                                 IdPersona = q.IdPersona,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 idEntidad = q.IdProveedor,
                                 idSucursal = q.IdSucursal,
                                 por_iva = q.co_Por_iva,
                                 Observacion = q.co_observacion,
                                 co_factura = q.co_factura,
                                 co_baseImponible = q.co_baseImponible,
                                 co_FechaFactura = q.co_FechaFactura,
                                 co_valoriva = q.co_valoriva,
                                 co_total = q.co_total,
                                 co_valorpagar = q.co_valorpagar,
                                 Saldo_OG = q.SaldoOG
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }        

        public List<cp_conciliacion_Caja_det_Info> get_list_x_pagar(int IdEmpresa, int IdSucursal)
        {
            try
            {
                List<cp_conciliacion_Caja_det_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_x_pagar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.cod_Documento != "N/D"
                             && q.Saldo_OG > 0
                             select new cp_conciliacion_Caja_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpresa_OGiro = q.IdEmpresa,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdPersona = q.IdPersona,
                                 pe_nombreCompleto = q.nom_proveedor,
                                 idEntidad = q.IdProveedor,
                                 idSucursal = q.IdSucursal,
                                 por_iva = q.co_Por_iva,
                                 Observacion = q.co_observacion,
                                 co_factura = q.co_factura,
                                 co_baseImponible = q.co_baseImponible,
                                 co_FechaFactura = q.co_FechaFactura,
                                 co_valoriva = q.co_valoriva,
                                 co_total = q.co_total,
                                 co_valorpagar = q.co_valorpagar,
                                 Saldo_OG = q.Saldo_OG,
                                 Tipo_documento = q.cod_Documento
                             }).ToList();

                    Lista.ForEach(q => q.Valor_a_aplicar = Convert.ToDecimal(q.Saldo_OG));
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_conciliacion_Caja_det_x_ValeCaja_Info> get_list_x_movimientos_caja(int IdEmpresa, int IdCaja)
        {
            try
            {
                List<cp_conciliacion_Caja_det_x_ValeCaja_Info> Lista;

                using (Entities_caja Context = new Entities_caja())
                {
                    Lista = (from q in Context.vwcaj_Caja_Movimiento_por_conciliar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdCaja == IdCaja
                             select new cp_conciliacion_Caja_det_x_ValeCaja_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpresa_movcaja = q.IdEmpresa,
                                 IdTipocbte_movcaja = q.IdTipocbte,
                                 IdCbteCble_movcaja = q.IdCbteCble,
                                 IdConciliacion_Caja = q.IdCaja,
                                 idTipoMovi = q.IdTipoMovi,
                                 tm_descripcion = q.tm_descripcion,
                                 valor = q.cm_valor,
                                 Observacion = q.cm_observacion,
                                 fecha = q.cm_fecha,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdTipo_Persona = q.IdTipo_Persona,
                                 IdEntidad = q.IdEntidad,
                                 IdPersona = q.IdPersona
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
