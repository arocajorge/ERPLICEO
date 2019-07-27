using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_EmpleadoFoto_Data
    {

        public ro_EmpleadoFoto_Info get_foto(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                ro_EmpleadoFoto_Info info_foto = new ro_EmpleadoFoto_Info();
                using (Entities_rrhh context=new Entities_rrhh())
                {

                    ro_EmpleadoFoto entity = context.ro_EmpleadoFoto.FirstOrDefault(v => v.IdEmpresa == IdEmpresa && v.IdEmpleado == IdEmpleado);
                    if (entity == null)
                        info_foto = new ro_EmpleadoFoto_Info();
                    info_foto.IdEmpresa = entity.IdEmpresa;
                    info_foto.IdEmpleado = entity.IdEmpleado;
                    info_foto.Foto = entity.Foto;
                    return info_foto;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
