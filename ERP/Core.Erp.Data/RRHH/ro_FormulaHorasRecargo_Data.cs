using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
  public  class ro_FormulaHorasRecargo_Data
    {

        public ro_FormulaHorasRecargo_Info get_info(int IdEmpresa)
        {
            try
            {
                ro_FormulaHorasRecargo_Info info = new ro_FormulaHorasRecargo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_FormulaHorasRecargo Entity = Context.ro_FormulaHorasRecargo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa );
                    if (Entity == null) return null;

                    info = new ro_FormulaHorasRecargo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Dividendo=Entity.Dividendo,
                        Divisor=Entity.Divisor,
                        PorcentajeRecargo=Entity.PorcentajeRecargo
                        
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
