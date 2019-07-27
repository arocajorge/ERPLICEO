using Core.Erp.Data.General;
using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Contabilidad
{
   public class CONTA_006_Data
    {

        tb_sucursal_Data data_sucursal = new tb_sucursal_Data();
        string Su_Descripcion = "";
        public List<CONTA_006_Info> GetList(int IdEmpresa, int IdAnio, bool mostrarSaldo0, string IdUsuario, int IdNivel, bool mostrarAcumulado, string balance)
        {
            try
            {
                List<CONTA_006_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPCONTA_006(IdEmpresa, IdAnio, mostrarSaldo0, IdUsuario, IdNivel, mostrarAcumulado, balance).Select(q => new CONTA_006_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdCtaCble = q.IdCtaCble,
                        IdCtaCblePadre = q.IdCtaCblePadre,
                        IdGrupoCble = q.IdGrupoCble,
                        IdNivelCta = q.IdNivelCta,
                        IdUsuario = q.IdUsuario,
                        Abril = q.Abril,
                        Agosto = q.Agosto,
                        Diciembre = q.Diciembre,
                        Enero = q.Enero,
                        EsCtaUtilidad = q.EsCtaUtilidad,
                        EsCuentaMovimiento = q.EsCuentaMovimiento,
                        Febrero = q.Febrero,
                        gc_estado_financiero = q.gc_estado_financiero,
                        gc_GrupoCble = q.gc_GrupoCble,
                        gc_Orden = q.gc_Orden,
                        Julio = q.Julio,
                        Junio = q.Junio,
                        Marzo = q.Marzo,
                        Mayo = q.Mayo,
                        Naturaleza = q.Naturaleza,
                        Noviembre = q.Noviembre,
                        Octubre = q.Octubre,
                        pc_Cuenta = q.pc_Cuenta,
                        Septiembre = q.Septiembre,
                        Total = q.Total,
                        Su_Descripcion = Su_Descripcion
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
