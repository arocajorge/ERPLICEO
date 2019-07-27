using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
   public class CXC_008_Data
    {
        public List<CXC_008_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdCliente, String IdCobro_tipo, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_anulados)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                decimal IdClienteIni = IdCliente;
                decimal IdClienteFin = IdCliente == 0 ? 9999999 : IdCliente;
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                List<CXC_008_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    if(mostrar_anulados==true)
                    {
                        Lista = Context.VWCXC_008.Where(q => q.IdEmpresa == IdEmpresa
                    && IdSucursalIni <= q.IdSucursal
                    && q.IdSucursal <= IdSucursalFin
                    && IdClienteIni <= q.IdCliente
                    && q.IdCliente <= IdClienteFin
                    && fecha_ini <= q.cr_fecha
                    && q.cr_fecha <= fecha_fin).Select(q => new CXC_008_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdBodega = q.IdBodega,
                        cr_estado = q.cr_estado,
                        cr_fecha = q.cr_fecha,
                        cr_observacion = q.cr_observacion,
                        dc_ValorPago = q.dc_ValorPago,
                        IdCbteVta = q.IdCbteVta,
                        IdCliente = q.IdCliente,
                        IdCobro = q.IdCobro,
                        IdCobro_tipo = q.IdCobro_tipo,
                        IdSucursal = q.IdSucursal,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Su_Descripcion = q.Su_Descripcion,
                        tc_descripcion = q.tc_descripcion,
                        vt_fecha = q.vt_fecha,
                        vt_NumFactura = q.vt_NumFactura,
                        vt_tipoDoc = q.vt_tipoDoc,
                        FechaLiquidacion = q.FechaLiquidacion
                    }).ToList();
                    }
                    else
                    {
                        Lista = Context.VWCXC_008.Where(q => q.IdEmpresa == IdEmpresa
                    && IdSucursalIni <= q.IdSucursal
                    && q.IdSucursal <= IdSucursalFin
                    && IdClienteIni <= q.IdCliente
                    && q.IdCliente <= IdClienteFin
                    && fecha_ini <= q.cr_fecha
                    && q.cr_fecha <= fecha_fin
                    && q.cr_estado == "A").Select(q => new CXC_008_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdBodega = q.IdBodega,
                        cr_estado = q.cr_estado,
                        cr_fecha = q.cr_fecha,
                        cr_observacion = q.cr_observacion,
                        dc_ValorPago = q.dc_ValorPago,
                        IdCbteVta = q.IdCbteVta,
                        IdCliente = q.IdCliente,
                        IdCobro = q.IdCobro,
                        IdCobro_tipo = q.IdCobro_tipo,
                        IdSucursal = q.IdSucursal,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Su_Descripcion = q.Su_Descripcion,
                        tc_descripcion = q.tc_descripcion,
                        vt_fecha = q.vt_fecha,
                        vt_NumFactura = q.vt_NumFactura,
                        vt_tipoDoc = q.vt_tipoDoc,
                        FechaLiquidacion = q.FechaLiquidacion

                    }).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(IdCobro_tipo))
                    Lista = Lista.Where(q => q.IdCobro_tipo == IdCobro_tipo).ToList();
                return Lista;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
