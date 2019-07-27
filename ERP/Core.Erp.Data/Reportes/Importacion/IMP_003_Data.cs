using Core.Erp.Info.Reportes.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Importacion
{
    public class IMP_003_Data
    {
        public List<IMP_003_Info> get_list(int IdEmpresa,string IdPais_embarque, decimal IdProveedor, decimal IdProducto, int IdMarca, DateTime fecha_ini, DateTime fecha_fin )
        {
            try
            {
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                List<IMP_003_Info> Lista;
                

                decimal IdProveedorini = IdProveedor;
                decimal IdProveedorfin =  IdProveedor == 0 ? 9999 : IdProveedor;

                decimal IdProductoini = IdProducto;
                decimal IdProductorfin = IdProducto == 0 ? 9999 : IdProducto;

                int IdMarcaini = IdMarca;
                int IdMarcafin = IdMarca == 0 ? 9999 : IdMarca;

                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWIMP_003
                             where q.IdEmpresa == IdEmpresa

                             && q.IdProveedor >= IdProveedorini && q.IdProveedor <= IdProveedorfin
                             && q.IdProducto >= IdProductoini && q.IdProducto <= IdProductorfin
                             && q.IdMarca >= IdMarcaini && q.IdMarca <= IdMarcafin

                             select new IMP_003_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdProducto = q.IdProducto,
                                 IdProveedor = q.IdProveedor,
                                 IdMarca = q.IdMarca,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 IdPais_embarque = q.IdPais_embarque,
                                 IdPresentacion = q.IdPresentacion,
                                 NomMarca = q.NomMarca,
                                 NomPais = q.NomPais,
                                 nom_presentacion = q.nom_presentacion,
                                 od_cantidad_recepcion = q.od_cantidad_recepcion,
                                 od_costo = q.od_costo,
                                 od_costo_bodega = q.od_costo_bodega,
                                 od_costo_final = q.od_costo_final,
                                 od_costo_total = q.od_costo_total,
                                 od_por_descuento = q.od_por_descuento,
                                 od_total_fob = q.od_total_fob,
                                 oe_fecha = q.oe_fecha,
                                 oe_observacion = q.oe_observacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 pr_descripcion = q.pr_descripcion,
                                 Secuencia = q.Secuencia

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
