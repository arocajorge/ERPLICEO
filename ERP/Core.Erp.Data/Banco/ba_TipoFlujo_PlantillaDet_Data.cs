using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_TipoFlujo_PlantillaDet_Data
    {
        public List<ba_TipoFlujo_PlantillaDet_Info> get_list(int IdEmpresa, decimal IdPlantilla)
        {
            try
            {
                List<ba_TipoFlujo_PlantillaDet_Info> Lista = new List<ba_TipoFlujo_PlantillaDet_Info>();

                using (Entities_banco db = new Entities_banco())
                {
                    Lista = db.vwba_TipoFlujoPlantillaDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdPlantilla == IdPlantilla).Select(q => new ba_TipoFlujo_PlantillaDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPlantilla = q.IdPlantilla,
                        Secuencia = q.Secuencia,
                        IdTipoFlujo = q.IdTipoFlujo,
                        Porcentaje = q.Porcentaje,
                        Descricion = q.Descricion
                    }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
