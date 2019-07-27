using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_028_Data
    {
        public List<ROL_028_Info> get_list(int IdEmpresa, int Id_Rdep)
        {
            try
            {

                List<ROL_028_Info> Lista = new List<ROL_028_Info>();
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWROL_028.Where(q => q.IdEmpresa == IdEmpresa
                    && q.Id_Rdep >= Id_Rdep).OrderBy(q => q.pe_apellido).ThenBy(q => q.pe_nombre).Select(q => new ROL_028_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        Id_Rdep = q.Id_Rdep,
                        Estado = q.Estado,
                        pe_anio = q.pe_anio,
                        IdEmpleado = q.IdEmpleado,
                        IdSucursal = q.IdSucursal,
                        IdNomina_Tipo = q.IdNomina_Tipo,
                        Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        Empleado = q.pe_apellido + " " + q.pe_nombre,
                        pe_nombre = q.pe_nombre,
                        pe_apellido = q.pe_apellido,
                        Sueldo = q.Sueldo,
                        FondosReserva = q.FondosReserva,
                        DecimoTercerSueldo = q.DecimoTercerSueldo,
                        DecimoCuartoSueldo = q.DecimoCuartoSueldo,
                        Vacaciones = q.Vacaciones,
                        AportePErsonal = q.AportePErsonal,
                        GastoAlimentacion = q.GastoAlimentacion,
                        GastoEucacion = q.GastoEucacion,
                        GastoSalud = q.GastoSalud,
                        GastoVestimenta = q.GastoVestimenta,
                        GastoVivienda = q.GastoVivienda,
                        Utilidades = q.Utilidades,
                        IngresoVarios = q.IngresoVarios,
                        IngresoPorOtrosEmpleaodres = q.IngresoPorOtrosEmpleaodres,
                        IessPorOtrosEmpleadores = q.IessPorOtrosEmpleadores,
                        ValorImpuestoPorEsteEmplador = q.ValorImpuestoPorEsteEmplador,
                        ValorImpuestoPorOtroEmplador = q.ValorImpuestoPorOtroEmplador,
                        ExoneraionPorDiscapacidad = q.ExoneraionPorDiscapacidad,
                        ExoneracionPorTerceraEdad = q.ExoneracionPorTerceraEdad,
                        OtrosIngresosRelacionDependencia = q.OtrosIngresosRelacionDependencia,
                        ImpuestoRentaCausado = q.ImpuestoRentaCausado,
                        ValorImpuestoRetenidoTrabajador = q.ValorImpuestoRetenidoTrabajador,
                        ImpuestoRentaAsumidoPorEsteEmpleador = q.ImpuestoRentaAsumidoPorEsteEmpleador,
                        BaseImponibleGravada = q.BaseImponibleGravada,
                        IngresosGravadorPorEsteEmpleador = q.IngresosGravadorPorEsteEmpleador
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
