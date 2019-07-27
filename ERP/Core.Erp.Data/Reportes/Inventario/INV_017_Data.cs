using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
    public class INV_017_Data
    {
        public List<INV_017_Info> get_list(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                List<INV_017_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWINV_017
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdMovi_inven_tipo == IdMovi_inven_tipo
                             && q.IdNumMovi == IdNumMovi
                             select new INV_017_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                 IdNumMovi = q.IdNumMovi,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 pr_descripcion = q.pr_descripcion,
                                 pr_codigo = q.pr_codigo,
                                 Su_Descripcion = q.Su_Descripcion,
                                 bo_Descripcion = q.bo_Descripcion,
                                 IdUnidadMedida_sinConversion = q.IdUnidadMedida_sinConversion,
                                 Descripcion = q.Descripcion,
                                 dm_cantidad_sinConversion = q.dm_cantidad_sinConversion,
                                 mv_costo_sinConversion = q.mv_costo_sinConversion,
                                 cm_observacion = q.cm_observacion,
                                 CodMoviInven = q.CodMoviInven,
                                 cm_fecha = q.cm_fecha,
                                 Estado = q.Estado,
                                 IdMotivo_Inv = q.IdMotivo_Inv,
                                 Desc_mov_inv = q.Desc_mov_inv,
                                 lote_num_lote = q.lote_num_lote,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 nom_presentacion = q.nom_presentacion,
                                 signo = q.signo,
                                 tm_descripcion = q.tm_descripcion,
                                 NomUsuario = q.NomUsuario,
                                 IdOrdenCompra = q.IdOrdenCompra,
                                 pe_nombreCompleto = q.pe_nombreCompleto
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
