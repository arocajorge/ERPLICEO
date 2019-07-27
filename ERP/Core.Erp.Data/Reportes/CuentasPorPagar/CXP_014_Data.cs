using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
  public class CXP_014_Data
    {
         public List<CXP_014_Info> GetList(int IdEmpresa,int IdSucursal, decimal IdProveedor, DateTime fecha_ini, DateTime fecha_fin, string IdTipoServicio, bool mostrar_anulados)

        {
            try
            {
                List<CXP_014_Info> Lista;
                decimal IdProveedorIni = IdProveedor;
                decimal IdProveedorFin = IdProveedor == 0 ? 9999 : IdProveedor;
                using (Entities_reportes Context = new Entities_reportes())
                {
                   if(mostrar_anulados)
                    {
                        Lista = Context.VWCXP_014.Where(q => q.IdEmpresa == IdEmpresa
                  && q.IdSucursal == IdSucursal
                  && IdProveedorIni <= q.IdProveedor
                  && q.IdProveedor <= IdProveedorFin
                  && fecha_ini <= q.co_FechaFactura
                  && q.co_FechaFactura <= fecha_fin
                   ).Select(q => new CXP_014_Info
                   {
                       IdEmpresa = q.IdEmpresa,
                       IdProveedor = q.IdProveedor,
                       IdSucursal = q.IdSucursal,                       
                       aut_doc_Modificar = q.aut_doc_Modificar,
                       BseImpNoObjDeIva = q.BseImpNoObjDeIva,
                       ConvenioTributacion = q.ConvenioTributacion,
                       co_baseImponible = q.co_baseImponible,
                       co_factura = q.co_factura,
                       co_FechaContabilizacion = q.co_FechaContabilizacion,
                       co_FechaFactura = q.co_FechaFactura,
                       co_FechaFactura_vct = q.co_FechaFactura_vct,
                       co_fechaOg = q.co_fechaOg,
                       co_observacion = q.co_observacion,
                       co_plazo = q.co_plazo,
                       co_Por_iva = q.co_Por_iva,
                       co_serie = q.co_serie,
                       co_subtotal_iva = q.co_subtotal_iva,
                       co_subtotal_siniva = q.co_subtotal_siniva,
                       co_total = q.co_total,
                       co_vaCoa = q.co_vaCoa,
                       co_valoriva = q.co_valoriva,
                       co_valorpagar = q.co_valorpagar,
                       cp_es_comprobante_electronico = q.cp_es_comprobante_electronico,
                       Descripcion = q.Descripcion,
                       estable_a_Modificar = q.estable_a_Modificar,
                       Estado = q.Estado,
                       fecha_autorizacion = q.fecha_autorizacion,
                       IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                       IdCod_101 = q.IdCod_101,
                       IdCod_ICE = q.IdCod_ICE,
                       IdTipoMovi = q.IdTipoMovi,
                       IdIden_credito = q.IdIden_credito,
                       IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                       IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                       IdTipoFlujo = q.IdTipoFlujo,
                       IdTipoServicio = q.IdTipoServicio,
                       Num_Autorizacion = q.Num_Autorizacion,
                       Num_Autorizacion_Imprenta = q.Num_Autorizacion_Imprenta,
                       num_docu_Modificar = q.num_docu_Modificar,
                       PagoLocExt = q.PagoLocExt,
                       PagoSujetoRetencion = q.PagoSujetoRetencion,
                       PaisPago = q.PaisPago,
                       pe_apellido = q.pe_apellido,
                       pe_cedulaRuc = q.pe_cedulaRuc,
                       pe_nombre = q.pe_nombre,
                       pe_nombreCompleto = q.pe_nombreCompleto,
                       pe_razonSocial = q.pe_razonSocial,
                       ptoEmi_a_Modificar = q.ptoEmi_a_Modificar,
                       Tipodoc_a_Modificar = q.Tipodoc_a_Modificar,
                       Su_Descripcion = q.Su_Descripcion
                   }).ToList();
                    }
                   else
                    {
                        Lista = Context.VWCXP_014.Where(q => q.IdEmpresa == IdEmpresa
                  && q.IdSucursal == IdSucursal
                  && IdProveedorIni <= q.IdProveedor
                  && q.IdProveedor <= IdProveedorFin
                  && fecha_ini <= q.co_FechaFactura
                  && q.co_FechaFactura <= fecha_fin
                  && q.Estado == "A"
                   ).Select(q => new CXP_014_Info
                   {
                       IdEmpresa = q.IdEmpresa,
                       IdProveedor = q.IdProveedor,
                       IdSucursal = q.IdSucursal,
                       aut_doc_Modificar = q.aut_doc_Modificar,
                       BseImpNoObjDeIva = q.BseImpNoObjDeIva,
                       ConvenioTributacion = q.ConvenioTributacion,
                       co_baseImponible = q.co_baseImponible,
                       co_factura = q.co_factura,
                       co_FechaContabilizacion = q.co_FechaContabilizacion,
                       co_FechaFactura = q.co_FechaFactura,
                       co_FechaFactura_vct = q.co_FechaFactura_vct,
                       co_fechaOg = q.co_fechaOg,
                       co_observacion = q.co_observacion,
                       co_plazo = q.co_plazo,
                       co_Por_iva = q.co_Por_iva,
                       co_serie = q.co_serie,
                       co_subtotal_iva = q.co_subtotal_iva,
                       co_subtotal_siniva = q.co_subtotal_siniva,
                       co_total = q.co_total,
                       co_vaCoa = q.co_vaCoa,
                       co_valoriva = q.co_valoriva,
                       co_valorpagar = q.co_valorpagar,
                       cp_es_comprobante_electronico = q.cp_es_comprobante_electronico,
                       Descripcion = q.Descripcion,
                       estable_a_Modificar = q.estable_a_Modificar,
                       Estado = q.Estado,
                       fecha_autorizacion = q.fecha_autorizacion,
                       IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                       IdCod_101 = q.IdCod_101,
                       IdCod_ICE = q.IdCod_ICE,
                       IdTipoMovi = q.IdTipoMovi,
                       IdIden_credito = q.IdIden_credito,
                       IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                       IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                       IdTipoFlujo = q.IdTipoFlujo,
                       IdTipoServicio = q.IdTipoServicio,
                       Num_Autorizacion = q.Num_Autorizacion,
                       Num_Autorizacion_Imprenta = q.Num_Autorizacion_Imprenta,
                       num_docu_Modificar = q.num_docu_Modificar,
                       PagoLocExt = q.PagoLocExt,
                       PagoSujetoRetencion = q.PagoSujetoRetencion,
                       PaisPago = q.PaisPago,
                       pe_apellido = q.pe_apellido,
                       pe_cedulaRuc = q.pe_cedulaRuc,
                       pe_nombre = q.pe_nombre,
                       pe_nombreCompleto = q.pe_nombreCompleto,
                       pe_razonSocial = q.pe_razonSocial,
                       ptoEmi_a_Modificar = q.ptoEmi_a_Modificar,
                       Tipodoc_a_Modificar = q.Tipodoc_a_Modificar,
                       Su_Descripcion = q.Su_Descripcion
                   }).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(IdTipoServicio))
                    Lista = Lista.Where(q => q.IdTipoServicio == IdTipoServicio).ToList();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
