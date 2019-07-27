using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
   public  class INV_015_Data
    {
        public List<INV_015_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, int IdCategoria, int IdLinea, int IdGrupo, int IdSubGrupo, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 999999 : IdSucursal;

                int IdBodega_ini = IdBodega;
                int IdBodega_fin = IdBodega == 0 ? 999999 : IdBodega;

                decimal IdProducto_ini = IdProducto;
                decimal IdProducto_fin = IdProducto == 0 ? 999999 : IdProducto;

                int IdCategoria_ini = IdCategoria;
                int IdCategoria_fin = IdCategoria == 0 ? 999999 : IdCategoria;


                int IdLinea_ini = IdLinea;
                int IdLinea_fin = IdLinea == 0 ? 999999 : IdLinea;


                int IdGrupo_ini = IdGrupo;
                int IdGrupo_fin = IdGrupo == 0 ? 999999 : IdGrupo;


                int IdSubgrupo_ini = IdSubGrupo;
                int IdSubgrupo_fin = IdSubGrupo == 0 ? 999999 : IdSubGrupo;
                List<INV_015_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWINV_015.Where(q => q.IdEmpresa_fa == IdEmpresa
                    && IdSucursal_ini <= q.IdSucursal_fa
                    && q.IdSucursal_fa <= IdSucursal_fin
                    && IdBodega_ini <= q.IdBodega_fa
                    && q.IdBodega_fa <= IdBodega_fin
                    && IdProducto_ini <= q.IdProducto
                    && q.IdProducto <= IdProducto_fin
                    && IdCategoria_ini <= q.IdCategoria
                    && q.IdCategoria <= IdCategoria_fin
                    && IdLinea_ini <= q.IdLinea
                    && q.IdLinea <= IdLinea_fin
                    && IdGrupo_ini <= q.IdGrupo
                    && q.IdGrupo <= IdGrupo_fin
                    && IdSubgrupo_ini <= q.IdSubGrupo
                    && q.IdSubGrupo <= IdSubgrupo_fin
                    && fecha_ini <= q.vt_fecha
                    && q.vt_fecha <= fecha_fin

                    ).Select(q => new INV_015_Info
                    {
                        IdEmpresa_eg = q.IdEmpresa_eg,
                        IdEmpresa_fa = q.IdEmpresa_fa,
                        IdBodega_fa = q.IdBodega_fa,
                        IdCategoria= q.IdCategoria,
                        CantidadFac = q.CantidadFac,
                        CantidadInv = q.CantidadInv,
                        ca_Categoria = q.ca_Categoria,
                        CostoUni = q.CostoUni,
                        IdCbteVta_fa = q.IdCbteVta_fa,
                        IdGrupo =q.IdGrupo,
                        IdLinea = q.IdLinea,
                        IdMovi_inven_tipo_eg = q.IdMovi_inven_tipo_eg,
                        IdNumMovi_eg = q.IdNumMovi_eg,
                        IdSubGrupo = q.IdSubGrupo,
                        IdSucursal_eg = q.IdSucursal_eg,
                        IdSucursal_fa = q.IdSucursal_fa,
                        nom_grupo = q.nom_grupo,
                        nom_linea = q.nom_linea,
                        nom_subgrupo = q.nom_subgrupo,
                        pr_descripcion = q.pr_descripcion,
                        Secuencia_eg = q.Secuencia_eg,
                        Secuencia_fa = q.Secuencia_fa,
                        Su_Descripcion = q.Su_Descripcion,
                        TotalCosto = q.TotalCosto,
                        TotalFac = q.TotalFac,
                        Utilidad = q.Utilidad,
                        vt_fecha = q.vt_fecha,
                        vt_NumFactura = q.vt_NumFactura,
                        vt_PrecioFinal = q.vt_PrecioFinal,
                        IdProducto = q.IdProducto

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
