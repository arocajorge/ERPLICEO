using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_empleado_x_division_x_area_Data
    {


        public List<ro_empleado_x_division_x_area_Info> get_lis(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                List<ro_empleado_x_division_x_area_Info> lista;
                using (Entities_rrhh content=new Entities_rrhh())
                {
                    lista = (from q in content.vwro_empleado_x_division_x_area

                             where q.IdEmpresa ==IdEmpresa
                             && q.IdEmpleado == IdEmpleado

                             select new ro_empleado_x_division_x_area_Info
                             {
                                 IdEmpresa=q.IdEmpresa,
                                 IdEmpleado=q.IdEmpleado,
                                 IDividion=q.IDividion,
                                 IdArea=q.IdArea,
                                 Secuencia=q.Secuencia,
                                 Porcentaje=q.Porcentaje,
                                 Observacion=q.Observacion,
                                 IdArea_det = q.IdArea,
                                 IDividion_det = q.IDividion,
                                 Descripcion_Division = q.DivisionDescripcion,
                                 Descripcion = q.AreaDescripcion,     
                                 CargaGasto = q.CargaGasto                            
                             }).ToList();

                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
