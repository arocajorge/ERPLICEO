using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_Comprobantes_Contables_Data
    {
        public bool grabarDB(ro_Comprobantes_Contables_Info info)
        {
            try
            {
                using (Entities_rrhh Contex = new Entities_rrhh())
                {
                    ro_Comprobantes_Contables Entity = new ro_Comprobantes_Contables
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNomina = info.IdNomina,
                        IdNominaTipo = info.IdNominaTipo,
                        IdPeriodo = info.IdPeriodo,
                        IdTipoCbte = info.IdTipoCbte,
                        IdCbteCble = info.IdCbteCble,
                        IdRol=info.IdRol,
                        IdEmpresa_rol=info.IdEmpresa_rol
                    };
                    Contex.ro_Comprobantes_Contables.Add(Entity);
                    Contex.SaveChanges();
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
