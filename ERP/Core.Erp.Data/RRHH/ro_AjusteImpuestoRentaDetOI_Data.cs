using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_AjusteImpuestoRentaDetOI_Data
    {
        public List<ro_AjusteImpuestoRentaDetOI_Info> get_list(int IdEmpresa, decimal IdAjuste, decimal IdEmpleado)
        {
            try
            {
                List<ro_AjusteImpuestoRentaDetOI_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_AjusteImpuestoRentaDetOI
                             where q.IdEmpresa == IdEmpresa
                             && q.IdAjuste == IdAjuste
                             && q.IdEmpleado == IdEmpleado
                             select new ro_AjusteImpuestoRentaDetOI_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdAjuste = q.IdAjuste,
                                 Secuencia = q.Secuencia,
                                 IdEmpleado = q.IdEmpleado,
                                 DescripcionOI = q.DescripcionOI,
                                 Valor = q.Valor
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarBD(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                using (Entities_rrhh db = new Entities_rrhh())
                {
                    ro_AjusteImpuestoRenta entity = db.ro_AjusteImpuestoRenta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAjuste == info.IdAjuste).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    var lst_oi = db.ro_AjusteImpuestoRentaDetOI.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAjuste == info.IdAjuste && q.IdEmpleado ==info.IdEmpleado).ToList();
                    db.ro_AjusteImpuestoRentaDetOI.RemoveRange(lst_oi);
                    decimal TotalOtrosIngresos = 0;

                    if (info.lst_det_oi.Count>0)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.lst_det_oi)
                        {
                            db.ro_AjusteImpuestoRentaDetOI.Add(new ro_AjusteImpuestoRentaDetOI
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAjuste = info.IdAjuste,
                                IdEmpleado = info.IdEmpleado,
                                Secuencia = Secuencia++,
                                DescripcionOI = item.DescripcionOI,
                                Valor = item.Valor
                            });
                        }
                        TotalOtrosIngresos = info.lst_det_oi.Sum(q => q.Valor);
                    }

                    ro_AjusteImpuestoRentaDet entity_det = db.ro_AjusteImpuestoRentaDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAjuste == info.IdAjuste && q.IdEmpleado == info.IdEmpleado).FirstOrDefault();
                    entity_det.OtrosIngresos = TotalOtrosIngresos;

                    

                    db.SaveChanges();

                    db.spRo_procesa_AjusteIR(info.IdEmpresa, info.IdAnio, info.IdAjuste, info.IdEmpleado, info.IdSucursal ?? 0, info.IdUsuario, info.Fecha.Date, info.FechaCorte.Date, info.Observacion);

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
