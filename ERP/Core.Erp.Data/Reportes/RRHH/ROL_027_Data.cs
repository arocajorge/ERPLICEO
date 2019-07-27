using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_027_Data
    {
        public List<ROL_027_Info> GetList(int IdEmpresa, int IdSucursal, int IdNomina_Tipo, int IdDivision, int IdArea, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 999999 : IdSucursal;


                int IdDivisionIni = IdDivision;
                int IdDivisionFin = IdDivision == 0 ? 999999 : IdDivision;

                int IdAreaIni = IdArea;
                int IdAreaFin = IdArea == 0 ? 999999 : IdArea;


                fecha_fin = fecha_fin.Date;
                fecha_ini = fecha_ini.Date;
                List<ROL_027_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPROL_027(IdEmpresa, IdSucursalIni, IdSucursalFin, IdNomina_Tipo, IdDivisionIni, IdDivisionFin, IdAreaIni, IdAreaFin, fecha_ini, fecha_fin).Select(q => new ROL_027_Info
                    {
                        IdEmpleado = q.IdEmpleado,
                        IdSucursal = q.IdSucursal,
                        Area = q.Area,
                        Descuento = q.Descuento,
                        em_codigo = q.em_codigo,
                        IdEmpresa = q.IdEmpresa,
                        Nomina = q.Nomina,
                        pe_apellido = q.pe_apellido,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        pe_nombre = q.pe_nombre,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Total = q.Total,
                        Valor = q.Valor,
                        de_descripcion = q.de_descripcion,
                        IdArea = q.IdArea,
                        IdDepartamento = q.IdDepartamento,
                        IdDivision = q.IdDivision,
                        Su_Descripcion = q.Su_Descripcion

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
