using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_Historico_Liquidacion_Vacaciones_Det_Data
    {
        public bool Guardar_DB(ro_Historico_Liquidacion_Vacaciones_Det_Info info)
        {
            try
            {
                using (Entities_rrhh db = new Entities_rrhh())
                {
                    ro_Historico_Liquidacion_Vacaciones_Det add = new ro_Historico_Liquidacion_Vacaciones_Det();
                    add.IdEmpresa = info.IdEmpresa;
                    add.IdEmpleado = info.IdEmpleado;
                    add.IdLiquidacion = info.IdLiquidacion;
                    add.Anio = info.Anio;
                    add.Mes = info.Mes;
                    add.Total_Remuneracion = info.Total_Remuneracion;
                    add.Total_Vacaciones = info.Total_Vacaciones;
                    add.Valor_Cancelar = info.Valor_Cancelar;
                    add.Sec = info.Sec;
                    db.ro_Historico_Liquidacion_Vacaciones_Det.Add(add);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception )
            {
                throw ;
            }
        }
        public bool Eliminar(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
                using (Entities_rrhh db = new Entities_rrhh())
                {
                    db.Database.ExecuteSqlCommand(" delete ro_Historico_Liquidacion_Vacaciones_Det where IdEmpresa='" + info.IdEmpresa + "'  and IdEmpleado='" + info.IdEmpleado + "'  and IdLiquidacion ='" + info.IdLiquidacion + "'");
                    return true;
                }
            }
            catch (Exception )
            {
                throw ;
            }
        }
        public List<ro_Historico_Liquidacion_Vacaciones_Det_Info> Get_Lis(int IdEmpresa, decimal idempleado, decimal idsolicitud)
        {
            List<ro_Historico_Liquidacion_Vacaciones_Det_Info> Lst = new List<ro_Historico_Liquidacion_Vacaciones_Det_Info>();
            Entities_rrhh oEnti = new Entities_rrhh();
            try
            {
                var Query = from q in oEnti.ro_Historico_Liquidacion_Vacaciones_Det
                            where q.IdEmpresa == IdEmpresa
                            && q.IdEmpleado == idempleado
                            && q.IdLiquidacion == idsolicitud
                            select q;
                foreach (var item in Query)
                {

                    ro_Historico_Liquidacion_Vacaciones_Det_Info add = new ro_Historico_Liquidacion_Vacaciones_Det_Info();
                    add.IdEmpresa = item.IdEmpresa;
                    add.IdEmpleado = item.IdEmpleado;
                    add.IdLiquidacion = item.IdLiquidacion;
                    add.Anio = item.Anio;
                    add.Mes = item.Mes;
                    add.Total_Remuneracion = item.Total_Remuneracion;
                    add.Total_Vacaciones = item.Total_Vacaciones;
                    add.Valor_Cancelar = item.Valor_Cancelar;
                    add.Sec = item.Sec;
                    Lst.Add(add);
                }
                return Lst;
            }
            catch (Exception )
            {
               
                throw;
            }

        }

    }
}
