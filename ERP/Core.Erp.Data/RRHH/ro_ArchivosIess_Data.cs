using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_ArchivosIess_Data
    {

        public List<ro_ArchivosIess_Info> get_list(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiqui, int IdPeriodo)
        {
            try
            {
                List<ro_ArchivosIess_Info> lista;
                using (Entities_rrhh Contex=new Entities_rrhh())
                {

                    lista = (from q in Contex.vwro_ArchivosIess
                             where q.IdEmpresa==IdEmpresa
                             && q.IdNominaTipo==IdNominaTipo
                             && q.IdNominaTipoLiqui==IdNominaTipoLiqui
                            && q.IdPeriodo==IdPeriodo
                             select new ro_ArchivosIess_Info
                             {
                                 IdEmpresa=q.IdEmpresa,
                                 IdNominaTipo=q.IdNominaTipo,
                                 IdNominaTipoLiqui=q.IdNominaTipoLiqui,
                                 IdPeriodo=q.IdPeriodo,
                                 IdRubro=q.IdRubro,
                                 IdEmpleado=q.IdEmpleado,
                                 pe_cedulaRuc=q.pe_cedulaRuc,
                                 pe_nombre=q.pe_nombre,
                                 pe_apellido=q.pe_apellido,
                                 pe_nombreCompleto=q.pe_nombreCompleto,
                                 Valor=q.Valor,
                                 ru_descripcion=q.ru_descripcion
                             }
                           ).ToList();

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
