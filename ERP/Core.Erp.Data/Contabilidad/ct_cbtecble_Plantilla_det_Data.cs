using Core.Erp.Info;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_cbtecble_Plantilla_det_Data
    {
        public List<ct_cbtecble_Plantilla_det_Info> get_list(int IdEmpresa, decimal IdPlantilla)
        {
            try
            {
                List<ct_cbtecble_Plantilla_det_Info> Lista = new List<ct_cbtecble_Plantilla_det_Info>();

                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    Lista = db.vwct_cbtecble_Plantilla_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdPlantilla == IdPlantilla).Select(q => new ct_cbtecble_Plantilla_det_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPlantilla = q.IdPlantilla,
                        secuencia = q.secuencia,
                        pc_Cuenta = q.pc_Cuenta,
                        IdCtaCble = q.IdCtaCble,
                        dc_Valor = q.dc_Valor,
                        dc_Valor_debe = q.dc_Valor >0 ? q.dc_Valor : 0,
                        dc_Valor_haber = q.dc_Valor > 0 ? 0 : (q.dc_Valor) * -1,
                        dc_Observacion = q.dc_Observacion,
                        IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                        IdPunto_cargo = q.IdPunto_cargo,
                        IdCentroCosto = q.IdCentroCosto
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
