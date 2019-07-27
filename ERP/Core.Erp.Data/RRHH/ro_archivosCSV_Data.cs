using Core.Erp.Info.RRHH.MTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
  public  class ro_archivosCSV_Data
    {

        public List<ro_archivosCSV_Info> get_lis(int idEmpresa, int IdRol, int IdRubro)
        {

            try
            {
                List<ro_archivosCSV_Info> oListado = new List<ro_archivosCSV_Info>();

                using (Entities_rrhh db = new Entities_rrhh())
                {
                    oListado =( from q in db.spROL_DecimosCSV(idEmpresa, IdRol, IdRubro)
                               select new ro_archivosCSV_Info
                               {
                                   IdEmpresa = q.IdEmpresa,
                                   IdEmpleado = q.IdEmpleado,
                                   pe_apellido = q.pe_apellido,
                                   pe_nombre = q.pe_nombre,
                                   pe_cedulaRuc = q.pe_cedulaRuc,
                                   CodigoSectorial = q.CodigoSectorial,
                                   ca_descripcion = q.ca_descripcion,
                                   Valor = q.Valor,
                                   pe_sexo = q.pe_sexo,
                                   Estado = q.Estado,
                                   em_fechaIngaRol = q.em_fechaIngaRol,
                                   DiasA_considerar_Decimo = q.DiasA_considerar_Decimo,
                                   Dias_Decimo = q.Dias_Decimo

                               }).ToList();
                }
                return oListado;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
