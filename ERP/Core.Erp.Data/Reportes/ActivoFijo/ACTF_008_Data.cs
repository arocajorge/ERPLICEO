using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.ActivoFijo
{
   public class ACTF_008_Data
    {
        public List<ACTF_008_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdDepartamento, decimal IdEmpleadoCustodio, decimal IdEmpleadoEncargado)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;



                decimal IdDepartamentoIni = IdDepartamento;
                decimal IdDepartamentoFin = IdDepartamento == 0 ? 9999 : IdDepartamento;


                decimal IdEmpleadoCustodioIni = IdEmpleadoCustodio;
                decimal IdEmpleadoCustodioFin = IdEmpleadoCustodio == 0 ? 999999999 : IdEmpleadoCustodio;


                decimal IdEmpleadoEncargadoIni = IdEmpleadoEncargado;
                decimal IdEmpleadoEncargadoFin = IdEmpleadoEncargado == 0 ? 999999999 : IdEmpleadoEncargado;

                List<ACTF_008_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWACTF_008.Where(q=> q.IdEmpresa == IdEmpresa
                    && IdSucursalIni <= q.IdSucursal
                    && q.IdSucursal <= IdSucursalFin
                    && IdDepartamentoIni <= q.IdDepartamento
                    && q.IdDepartamento <= IdDepartamentoFin 
                    && IdEmpleadoCustodioIni <= q.IdEmpleadoCustodio
                    && q.IdEmpleadoCustodio <= IdEmpleadoCustodioFin
                    && IdEmpleadoEncargadoIni <= q.IdEmpleadoEncargado
                    && q.IdEmpleadoEncargado <= IdEmpleadoEncargadoFin
                    
                    ).Select(q => new ACTF_008_Info
                    {
                        IdEmpleadoCustodio = q.IdEmpleadoCustodio,
                        Af_costo_compra = q.Af_costo_compra,
                        Af_fecha_compra = q.Af_fecha_compra,
                        Af_Nombre = q.Af_Nombre,
                        Af_observacion = q.Af_observacion,
                        Af_Vida_Util = q.Af_Vida_Util,
                        Cantidad = q.Cantidad,
                        EmpleadoCustodio = q.EmpleadoCustodio, 
                        EmpleadoEncargado = q.EmpleadoEncargado,
                        Estado = q.Estado,
                        IdActivoFijo = q.IdActivoFijo,
                        IdDepartamento = q.IdDepartamento,
                        IdEmpleadoEncargado = q.IdEmpleadoEncargado,
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        NomCategoria = q.NomCategoria,
                        NomDepartamento = q.NomDepartamento,
                        NomTipo = q.NomTipo,
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
