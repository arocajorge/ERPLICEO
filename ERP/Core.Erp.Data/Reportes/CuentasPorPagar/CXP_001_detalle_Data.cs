using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_001_detalle_Data
    {
        public List<CXP_001_detalle_Info> get_list(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                List<CXP_001_detalle_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_001_detalle
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                             && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro
                             select new CXP_001_detalle_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdProducto = q.IdProducto,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 Cantidad = q.Cantidad,
                                 CostoUni = q.CostoUni,
                                 CostoUniFinal = q.CostoUniFinal,
                                 Descripcion = q.Descripcion,
                                 DescuentoUni = q.DescuentoUni,
                                 PorDescuento = q.PorDescuento,
                                 PorIva = q.PorIva,
                                 pr_codigo = q.pr_codigo,
                                 pr_codigo2 = q.pr_codigo2,
                                 Subtotal = q.Subtotal,
                                 Total = q.Total,
                                 ValorIva = q.ValorIva,
                                 pr_descripcion = q.pr_descripcion


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
