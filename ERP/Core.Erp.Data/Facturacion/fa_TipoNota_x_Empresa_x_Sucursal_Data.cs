using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_TipoNota_x_Empresa_x_Sucursal_Data
    {
        public List<fa_TipoNota_x_Empresa_x_Sucursal_Info> get_list(int IdEmpresa, int IdTipoNota)
        {
            try
            {
                List<fa_TipoNota_x_Empresa_x_Sucursal_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.fa_TipoNota_x_Empresa_x_Sucursal
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipoNota == IdTipoNota
                             select new fa_TipoNota_x_Empresa_x_Sucursal_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdTipoNota = q.IdTipoNota,
                                 IdCtaCble = q.IdCtaCble
                             }).ToList();
                }
                Lista.ForEach(v => v.secuencia = v.secuencia + 1);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_TipoNota_x_Empresa_x_Sucursal_Info get_info(int IdEmpresa, int IdTipoNota, int IdSucursal)
        {
            try
            {
                fa_TipoNota_x_Empresa_x_Sucursal_Info info;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var Entity = Context.fa_TipoNota_x_Empresa_x_Sucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoNota == IdTipoNota && q.IdSucursal == IdSucursal).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_TipoNota_x_Empresa_x_Sucursal_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdTipoNota = Entity.IdTipoNota,
                        IdCtaCble = Entity.IdCtaCble
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
