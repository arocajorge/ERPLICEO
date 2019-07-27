using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
   public class CXC_003_Data
    {
        public List<CXC_003_Info> get_list (int IdEmpresa, decimal IdCliente, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                List<CXC_003_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;

                decimal IdClienteIni = IdCliente;
                decimal IdClienteFin = IdCliente == 0 ? 999999 : IdCliente;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXC_003
                             where q.IdEmpresa == IdEmpresa
                             && q.IdCliente >= IdClienteIni
                             && q.IdCliente <= IdClienteFin
                             && q.vt_fecha >= Fecha_ini
                             && q.vt_fecha <= Fecha_fin
                             select new CXC_003_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 vt_fecha = q.vt_fecha,
                                 vt_NumFactura = q.vt_NumFactura,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdCliente = q.IdCliente,
                                 ValorRteFTE = q.ValorRteFTE,
                                 ValorRteIVA = q.ValorRteIVA,
                                 PorcentajeRetIVA = q.PorcentajeRetIVA,
                                 cr_fecha = q.cr_fecha,
                                 PorcentajeRetFTE = q.PorcentajeRetFTE,
                                 TotalRTE = q.TotalRTE
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}
