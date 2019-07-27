using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.ActivoFijo
{
    public class ACTF_001_Data
    {
        public List<ACTF_001_Info> get_list(int IdEmpresa, decimal Id_Mejora_Baja_Activo, string Id_Tipo)
        {
            try
            {
                List<ACTF_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWACTF_001
                             where q.IdEmpresa == IdEmpresa
                             && q.Id_Mejora_Baja_Activo == Id_Mejora_Baja_Activo
                             && q.Id_Tipo == Id_Tipo
                             select new ACTF_001_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 Id_Mejora_Baja_Activo = q.Id_Mejora_Baja_Activo,
                                 Id_Tipo = q.Id_Tipo,
                                 IdActivoFijo = q.IdActivoFijo,
                                 Af_Nombre = q.Af_Nombre,
                                 ValorActivo = q.ValorActivo,
                                 Valor_Tot_Bajas = q.Valor_Tot_Bajas,
                                 Valor_Tot_Mejora = q.Valor_Tot_Mejora,
                                 Valor_Depre_Acu = q.Valor_Depre_Acu,
                                 Valor_Neto =q.Valor_Neto,
                                 Valor_Mej_Baj_Activo = q.Valor_Mej_Baj_Activo,
                                 Fecha_MejBaj = q.Fecha_MejBaj,
                                 Estado = q.Estado,
                                 Motivo = q.Motivo,
                                 IdCtaCble = q.IdCtaCble,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 pc_Cuenta = q.pc_Cuenta
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
