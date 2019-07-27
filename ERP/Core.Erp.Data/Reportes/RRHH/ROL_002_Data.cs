using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Data.RRHH;

namespace Core.Erp.Data.Reportes.RRHH
{
   public class ROL_002_Data
    {

        public List<ROL_002_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, int IdSucursal, decimal Idempleado)
        {
            try
            {
                ro_rubros_calculados_Data calculados_data = new ro_rubros_calculados_Data();
                var info_rubros_calcu = calculados_data.get_info(IdEmpresa);
                List<ROL_002_Info> Lista;
                string mes_nom_ = mes(IdPeriodo);

                using (Entities_reportes Context = new Entities_reportes())
                {

                  //  Context.SPROL_002(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo);
                    Lista = (from q in Context.VWROL_002
                             where (q.IdSucursal == 0 ? 1==1 : q.IdSucursal == IdSucursal
                             && q.IdEmpresa == IdEmpresa
                             && q.IdNominaTipo == IdNomina
                             && q.IdNominaTipoLiqui == IdNominaTipo
                             && q.IdPeriodo == IdPeriodo
                             && q.IdEmpleado==Idempleado
                             && q.IdRubro!=info_rubros_calcu.IdRubro_tot_pagar)                             
                             orderby q.NombreCompleto
                             select new ROL_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                 IdPeriodo = q.IdPeriodo,
                                 Ruc = q.Ruc,
                                 em_ruc = "RUC #"+q.em_ruc,
                                 ca_orden = q.ca_orden,
                                 ru_orden = q.ru_orden,
                                 NombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                 RubroDescripcion = q.RubroDescripcion,
                                 Cargo = q.Cargo,
                                 Valor = q.Valor,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_FechaFin = q.pe_FechaFin,
                                 IdNominaTipo = q.IdNominaTipo,
                                 mes_nom = mes_nom_,
                                 Area = q.Area,
                                 de_descripcion = q.de_descripcion,
                                 em_codigo = q.em_codigo,
                                 em_status = q.em_status,
                                 Grupo = q.Grupo,
                                 IdSucursal = q.IdSucursal,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 ru_tipo = q.ru_tipo   ,
                                 IdRubro=q.IdRubro,
                                 NomNomina = q.NomNomina,
                                 NomNominaTipo = q.NomNominaTipo

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ROL_002_Info> get_list_empleados(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, int IdSucursal)
        {
            try
            {
                List<ROL_002_Info> Lista;
                string mes_nom_ = mes(IdPeriodo);
                ro_rubros_calculados_Data calculados_data = new ro_rubros_calculados_Data();
                var info_rubros_calcu = calculados_data.get_info(IdEmpresa);
                using (Entities_reportes Context = new Entities_reportes())
                {

                    Context.SPROL_002(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo);
                    Lista = (from q in Context.VWROL_002
                             where (q.IdSucursal == 0 ? 1 == 1 : q.IdSucursal == IdSucursal
                             && q.IdEmpresa == IdEmpresa
                             && q.IdNominaTipo == IdNomina
                             && q.IdNominaTipoLiqui == IdNominaTipo
                             && q.IdPeriodo == IdPeriodo
                             && q.IdRubro==info_rubros_calcu.IdRubro_tot_pagar
                             )
                             orderby q.NombreCompleto
                             select new ROL_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 IdNominaTipoLiqui = q.IdNominaTipoLiqui,
                                 IdPeriodo = q.IdPeriodo,
                                 Ruc = q.Ruc,
                                 em_ruc = "RUC #" + q.em_ruc,
                                 ru_orden = q.ru_orden,
                                 NombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                 RubroDescripcion = q.RubroDescripcion,
                                 Cargo = q.Cargo,
                                 Valor = q.Valor,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_FechaFin = q.pe_FechaFin,
                                 IdNominaTipo = q.IdNominaTipo,
                                 mes_nom = mes_nom_,
                                 Area = q.Area,
                                 de_descripcion = q.de_descripcion,
                                 em_codigo = q.em_codigo,
                                 em_status = q.em_status,
                                 Grupo = q.Grupo,
                                 IdSucursal = q.IdSucursal,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 ru_tipo = q.ru_tipo,
                                 IdRubro = q.IdRubro,
                                 NomNomina = q.NomNomina,
                                 NomNominaTipo = q.NomNominaTipo

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ROL_002_Info> get_list_detalle_prestamos(int IdEmpresa, int IdNominaTipo, int IdPeriodo, int IdEmpleado)
        {
            try
            {
                List<ROL_002_Info> ListaPrestamos;
                ro_periodo_Data periodo_data = new ro_periodo_Data();
                var info_periodo = periodo_data.get_info(IdEmpresa, IdPeriodo);
                using (Entities_reportes Context = new Entities_reportes())
                {
                    ListaPrestamos = (from q in Context.VWROL_002_detallle_prestamos
                                      where (q.IdEmpresa == IdEmpresa
                                     && q.IdEmpleado == IdEmpleado
                                     && q.FechaPago>= info_periodo.pe_FechaIni
                                     && q.FechaPago <= info_periodo.pe_FechaFin
                                     )
                             orderby q.ru_descripcion, q.IdPrestamo
                                      select new ROL_002_Info
                             {
                                 IdPrestamo = q.IdPrestamo,
                                 ru_descripcion = q.ru_descripcion,
                                 NumCuota = q.NumCuota,
                                 NumCuotas = q.NumCuotas,
                                 TotalCuota = q.TotalCuota
                             }).ToList();
                }

                return ListaPrestamos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string mes(int idperiodo)
        {
            try
            {

                string anio = idperiodo.ToString().Substring(0, 4);
                string mes_nom = "";
                if (idperiodo.ToString().Substring(4, 2) == "01")
                    mes_nom = "COMPROBANTE DE PAGO ENERO "+anio;
                if (idperiodo.ToString().Substring(4, 2) == "02")
                    mes_nom = "COMPROBANTE DE PAGO FEBRERO" + anio;
                if (idperiodo.ToString().Substring(4, 2) == "03")
                    mes_nom = "COMPROBANTE DE PAGO MARZO " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "04")
                    mes_nom = "COMPROBANTE DE PAGO ABRIL " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "05")
                    mes_nom = "COMPROBANTE DE PAGO MAYO " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "06")
                    mes_nom = "COMPROBANTE DE PAGO JUNIO " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "07")
                    mes_nom = "COMPROBANTE DE PAGO JULIO " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "08")
                    mes_nom = "COMPROBANTE DE PAGO AGOSTO " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "09")
                    mes_nom = "COMPROBANTE DE PAGO SEPTIEMBRE " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "10")
                    mes_nom = "COMPROBANTE DE PAGO OCTUBRE " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "11")
                    mes_nom = "COMPROBANTE DE PAGO NOVIEMBRE " + anio;
                if (idperiodo.ToString().Substring(4, 2) == "12")
                    mes_nom = "COMPROBANTE PAGO DICIEMBRE " + anio;

                return mes_nom;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
