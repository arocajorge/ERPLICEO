using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
  public  class fa_factura_x_fa_guia_remision_Data
    {
        public List<fa_factura_x_fa_guia_remision_Info> get_list(int IdEmpresa,decimal IdGuiaRemision)
        {
            try
            {
                List<fa_factura_x_fa_guia_remision_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_factura_x_fa_guia_remision
                             where q.IdEmpresa == IdEmpresa
                             && q.gi_IdGuiaRemision==IdGuiaRemision
                             select new fa_factura_x_fa_guia_remision_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 CodCbteVta = q.CodCbteVta,
                                 vt_serie1 = q.vt_serie1,
                                 vt_serie2 = q.vt_serie2,
                                 vt_NumFactura = q.vt_NumFactura,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 gi_IdGuiaRemision=q.gi_IdGuiaRemision

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminar(int IdEmpresa, decimal IdGuiaRemision)
        {
            try
            {
                using (Entities_importacion context = new Entities_importacion())
                {
                    string sql = "delete fa_factura_x_fa_guia_remision where fa_IdEmpresa='" + IdEmpresa + "' and gi_IdGuiaRemision='" + IdGuiaRemision + "'";
                    context.Database.ExecuteSqlCommand(sql);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
