using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_008_Data
    {
        public List<CXP_008_Info> get_list(int IdEmpresa, DateTime fecha, int IdSucursal, decimal IdProveedor, bool no_mostrar_en_conciliacion, bool no_mostrar_saldo_0)
        {
            try
            {
               decimal IdProveedor_ini = IdProveedor;
                decimal IdProveedor_fin = IdProveedor == 0 ? 9999 : IdProveedor;

                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                fecha = fecha.Date;
                List<CXP_008_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    if (no_mostrar_en_conciliacion && no_mostrar_saldo_0)
                    {
                        Lista = (from q in Context.SPCXP_008(IdEmpresa, fecha, IdSucursalIni, IdSucursalFin, IdProveedor_ini, IdProveedor_fin)
                                 where q.Saldo > 0 && q.en_conciliacion == 0
                                 select new CXP_008_Info
                                 {
                                     IdRow = q.IdRow,
                                     IdEmpresa = q.IdEmpresa,
                                     IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                     IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                     IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                     Documento = q.Documento,
                                     nom_tipo_doc = q.nom_tipo_doc,
                                     cod_tipo_doc = q.cod_tipo_doc,
                                     IdProveedor = q.IdProveedor,
                                     nom_proveedor = q.nom_proveedor,
                                     Valor_a_pagar = q.Valor_a_pagar,
                                     MontoAplicado = q.MontoAplicado,
                                     Saldo = q.Saldo,
                                     Observacion = q.Observacion,
                                     Ruc_Proveedor = q.Ruc_Proveedor,
                                     representante_legal = q.representante_legal,
                                     Tipo_cbte = q.Tipo_cbte,
                                     Plazo_fact = q.Plazo_fact,
                                     co_FechaFactura_vct = q.co_FechaFactura_vct,
                                     co_fechaOg = q.co_fechaOg,
                                     Dias_Vcto = q.Dias_Vcto,
                                     Fecha_corte = q.Fecha_corte,
                                     x_Vencer = q.x_Vencer,
                                     Vencido = q.Vencido,
                                     Vencido_1_30 = q.Vencido_1_30,
                                     Vencido_31_60 = q.Vencido_31_60,
                                     Vencido_60_90 = q.Vencido_60_90,
                                     Vencido_mayor_90 = q.Vencido_mayor_90,
                                     en_conciliacion = q.en_conciliacion
                                 }).ToList();
                    }
                    else
                        if (!no_mostrar_en_conciliacion && no_mostrar_saldo_0)
                    {
                        Lista = (from q in Context.SPCXP_008(IdEmpresa, fecha, IdSucursalIni, IdSucursalFin, IdProveedor_ini, IdProveedor_fin)
                                 where q.Saldo > 0
                                 select new CXP_008_Info
                                 {
                                     IdRow = q.IdRow,
                                     IdEmpresa = q.IdEmpresa,
                                     IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                     IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                     IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                     Documento = q.Documento,
                                     nom_tipo_doc = q.nom_tipo_doc,
                                     cod_tipo_doc = q.cod_tipo_doc,
                                     IdProveedor = q.IdProveedor,
                                     nom_proveedor = q.nom_proveedor,
                                     Valor_a_pagar = q.Valor_a_pagar,
                                     MontoAplicado = q.MontoAplicado,
                                     Saldo = q.Saldo,
                                     Observacion = q.Observacion,
                                     Ruc_Proveedor = q.Ruc_Proveedor,
                                     representante_legal = q.representante_legal,
                                     Tipo_cbte = q.Tipo_cbte,
                                     Plazo_fact = q.Plazo_fact,
                                     co_FechaFactura_vct = q.co_FechaFactura_vct,
                                     co_fechaOg = q.co_fechaOg,
                                     Dias_Vcto = q.Dias_Vcto,
                                     Fecha_corte = q.Fecha_corte,
                                     x_Vencer = q.x_Vencer,
                                     Vencido = q.Vencido,
                                     Vencido_1_30 = q.Vencido_1_30,
                                     Vencido_31_60 = q.Vencido_31_60,
                                     Vencido_60_90 = q.Vencido_60_90,
                                     Vencido_mayor_90 = q.Vencido_mayor_90,
                                     en_conciliacion = q.en_conciliacion
                                 }).ToList();
                    }
                    else
                        if (no_mostrar_en_conciliacion && !no_mostrar_saldo_0)
                    {
                        Lista = (from q in Context.SPCXP_008(IdEmpresa, fecha, IdSucursalIni, IdSucursalFin, IdProveedor_ini, IdProveedor_fin)
                                 where q.en_conciliacion == 0
                                 select new CXP_008_Info
                                 {
                                     IdRow = q.IdRow,
                                     IdEmpresa = q.IdEmpresa,
                                     IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                     IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                     IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                     Documento = q.Documento,
                                     nom_tipo_doc = q.nom_tipo_doc,
                                     cod_tipo_doc = q.cod_tipo_doc,
                                     IdProveedor = q.IdProveedor,
                                     nom_proveedor = q.nom_proveedor,
                                     Valor_a_pagar = q.Valor_a_pagar,
                                     MontoAplicado = q.MontoAplicado,
                                     Saldo = q.Saldo,
                                     Observacion = q.Observacion,
                                     Ruc_Proveedor = q.Ruc_Proveedor,
                                     representante_legal = q.representante_legal,
                                     Tipo_cbte = q.Tipo_cbte,
                                     Plazo_fact = q.Plazo_fact,
                                     co_FechaFactura_vct = q.co_FechaFactura_vct,
                                     co_fechaOg = q.co_fechaOg,
                                     Dias_Vcto = q.Dias_Vcto,
                                     Fecha_corte = q.Fecha_corte,
                                     x_Vencer = q.x_Vencer,
                                     Vencido = q.Vencido,
                                     Vencido_1_30 = q.Vencido_1_30,
                                     Vencido_31_60 = q.Vencido_31_60,
                                     Vencido_60_90 = q.Vencido_60_90,
                                     Vencido_mayor_90 = q.Vencido_mayor_90,
                                     en_conciliacion = q.en_conciliacion
                                 }).ToList();
                    }
                    else
                    Lista = (from q in Context.SPCXP_008(IdEmpresa, fecha, IdSucursalIni, IdSucursalFin, IdProveedor_ini, IdProveedor_fin)
                             select new CXP_008_Info
                             {
                                 IdRow = q.IdRow,
                                 IdEmpresa = q.IdEmpresa,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                 Documento = q.Documento,
                                 nom_tipo_doc = q.nom_tipo_doc,
                                 cod_tipo_doc = q.cod_tipo_doc,
                                 IdProveedor = q.IdProveedor,
                                 nom_proveedor = q.nom_proveedor,
                                 Valor_a_pagar = q.Valor_a_pagar,
                                 MontoAplicado = q.MontoAplicado,
                                 Saldo = q.Saldo,
                                 Observacion = q.Observacion,
                                 Ruc_Proveedor = q.Ruc_Proveedor,
                                 representante_legal = q.representante_legal,
                                 Tipo_cbte = q.Tipo_cbte,
                                 Plazo_fact = q.Plazo_fact,
                                 co_FechaFactura_vct = q.co_FechaFactura_vct,
                                 co_fechaOg = q.co_fechaOg,
                                 Dias_Vcto = q.Dias_Vcto,
                                 Fecha_corte = q.Fecha_corte,
                                 x_Vencer = q.x_Vencer,
                                 Vencido = q.Vencido,
                                 Vencido_1_30 = q.Vencido_1_30,
                                 Vencido_31_60 = q.Vencido_31_60,
                                 Vencido_60_90 = q.Vencido_60_90,
                                 Vencido_mayor_90 = q.Vencido_mayor_90,
                                 en_conciliacion = q.en_conciliacion
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
