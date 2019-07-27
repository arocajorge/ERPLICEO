using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_sucursal_FormaPago_x_fa_NivelDescuento_Data
    {
        public List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> get_list(int IdEmpresa, int IdSucursal)
        {
            try
            {
                List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> Lista;

                using (Entities_general db = new Entities_general())
                {
                    Lista = db.tb_sucursal_FormaPago_x_fa_NivelDescuento.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal).Select(q => new tb_sucursal_FormaPago_x_fa_NivelDescuento_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        Secuencia = q.Secuencia,
                        IdCatalogo = q.IdCatalogo,
                        IdNivel = q.IdNivel

                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_sucursal_FormaPago_x_fa_NivelDescuento_Info get_info(int IdEmpresa, int IdSucursal, string IdCatalogo)
        {
            try
            {
                tb_sucursal_FormaPago_x_fa_NivelDescuento_Info info = new tb_sucursal_FormaPago_x_fa_NivelDescuento_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_sucursal_FormaPago_x_fa_NivelDescuento Entity = Context.tb_sucursal_FormaPago_x_fa_NivelDescuento.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdCatalogo == IdCatalogo).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new tb_sucursal_FormaPago_x_fa_NivelDescuento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        Secuencia = Entity.Secuencia,
                        IdCatalogo = Entity.IdCatalogo,
                        IdNivel = Entity.IdNivel
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
