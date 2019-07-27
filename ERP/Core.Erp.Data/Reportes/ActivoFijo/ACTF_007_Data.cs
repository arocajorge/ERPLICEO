using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.ActivoFijo
{
   public class ACTF_007_Data
    {
        public List<ACTF_007_Info> GetList(int IdEmpresa, int IdActivoFijo)
        {
            try
            {
                List<ACTF_007_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWACTF_007.Where(q => q.IdEmpresa == IdEmpresa && q.IdActivoFijo == IdActivoFijo).Select(q => new ACTF_007_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdActivoFijo = q.IdActivoFijo, 
                        Af_costo_compra = q.Af_costo_compra,
                        Af_Nombre = q.Af_Nombre,
                        Af_fecha_compra = q.Af_fecha_compra,
                        Af_observacion = q.Af_observacion,
                        Estado = q.Estado,
                        NomCategoria = q.NomCategoria,
                        NomCustodio = q.NomCustodio,
                        NomDepartamento = q.NomDepartamento,
                        NomEncargado = q.NomEncargado,
                        NomTipo = q.NomTipo,
                        Su_Descripcion = q.Su_Descripcion,
                        Cantidad = q.Cantidad


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
