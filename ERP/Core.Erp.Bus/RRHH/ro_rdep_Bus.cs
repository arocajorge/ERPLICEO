using Core.Erp.Bus.General;
using Core.Erp.Data.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.RRHH;
using Core.Erp.Info.RRHH.RDEP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_rdep_Bus
    {
        ro_rdep_Data oData = new ro_rdep_Data();
        tb_empresa_Info info_empresa = new tb_empresa_Info();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();

        public List<ro_rdep_Info> GetList(int IdEmpresa, int IdSucursal, int IdNomina_Tipo, int IdAnio)
        {
            try
            {
                return oData.GetList(IdEmpresa, IdSucursal, IdNomina_Tipo, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ro_rdep_Info GetInfo(int IdEmpresa, int Id_Rdep )
        {
            try
            {
                return oData.GetInfo(IdEmpresa, Id_Rdep);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ro_rdep_Info GetInfo_X_Anio(int IdEmpresa, int IdSucursal, int Anio)
        {
            try
            {
                return oData.GetInfo_X_Anio(IdEmpresa, IdSucursal, Anio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ro_rdep_det_Info GetInfo_x_Empleado(int IdEmpresa, int Id_Rdep, int Secuencia)
        {
            try
            {
                return oData.GetInfo_x_Empleado(IdEmpresa, Id_Rdep, Secuencia);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GenerarRDEP(int IdEmpresa, int IdSucursal, int Id_Rdep, int IdAnio, int IdNomina_Tipo, int IdEmpleado, string Observacion, string IdUsuario)
        {
            try
            {
                return oData.GenerarRDEP(IdEmpresa, IdSucursal, Id_Rdep, IdAnio, IdNomina_Tipo, IdEmpleado, Observacion, IdUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarBD(ro_rdep_det_Info info)
        {
            try
            {
                return oData.ModificarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(ro_rdep_Info info)
        {
            try
            {
                return oData.AnularBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /********* XML ***************/
        rdep rdep = new rdep();
        List<ro_tabla_Impu_Renta_Info> list_base_calculo = new List<ro_tabla_Impu_Renta_Info>();
        ro_tabla_Impu_Renta_Data odata_base_Calculo = new ro_tabla_Impu_Renta_Data();
        
        public rdep get_info_xml(int IdEmpresa, int IdRdep)
        {
            try
            {
                info_empresa = bus_empresa.get_info(IdEmpresa);
                rdep.numRuc = info_empresa.em_ruc;
                
                datRetRelDepTyp detalle = new datRetRelDepTyp();
                rdep.retRelDep = new List<datRetRelDepTyp>();

                List<ro_rdep_det_Info> Lista_RDEP = new List<ro_rdep_det_Info>();

                Lista_RDEP = oData.get_info_xml(IdEmpresa, IdRdep);

                Lista_RDEP.ForEach(item =>
                {
                    rdep.anio = item.pe_anio;

                    datRetRelDepTyp info_det = new datRetRelDepTyp();
                    info_det.empleado = new datEmpTyp();
                    info_det.empleado.benGalpg = benGalpgType.NO;
                    info_det.empleado.tipIdRet = datEmpTypTipIdRet.C;
                    info_det.empleado.idRet = item.pe_cedulaRuc;
                    info_det.empleado.apellidoTrab = item.pe_apellido.Replace("Ñ", "N").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Trim();
                    info_det.empleado.nombreTrab = item.pe_nombre.Replace("Ñ", "N").Replace("Á","A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Trim();
                    info_det.empleado.apellidoTrab = info_det.empleado.apellidoTrab.Replace("Ñ", "N").Replace("  ", " ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Trim();
                    info_det.empleado.nombreTrab = info_det.empleado.nombreTrab.Replace("Ñ", "N").Replace("  ", " ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Trim();
                    info_det.empleado.estab = item.Su_CodigoEstablecimiento;
                    info_det.empleado.residenciaTrab = resciTyp.Item01;
                    info_det.empleado.paisResidencia = "593";
                    info_det.empleado.aplicaConvenio = convImposTyp.NA;
                    info_det.empleado.tipoTrabajDiscap = discapTyp.Item01;
                    info_det.empleado.porcentajeDiscap = "0";
                    info_det.empleado.tipIdDiscap = tipIdDiscapTyp.N;
                    info_det.empleado.idDiscap = "999";
                    //info_det.deducAliementSpecified = true;
                    //info_det.deducVestimSpecified = true;
                    //info_det.deducSaludSpecified = true;
                    info_det.deducEducaSpecified = false;
                    //info_det.deducViviendaSpecified = true;
                    info_det.deducArtycultSpecified = false;
                    info_det.suelSal = (item.Sueldo) == null ? Convert.ToDecimal(0.00) : Math.Round(Convert.ToDecimal(item.Sueldo), 2, MidpointRounding.AwayFromZero);
                    info_det.sobSuelComRemu = (item.IngresoVarios) == null ? Convert.ToDecimal(0.00) : Math.Round(Convert.ToDecimal(item.IngresoVarios), 2, MidpointRounding.AwayFromZero);
                    info_det.partUtil = Math.Round(Convert.ToDecimal(item.Utilidades), 2, MidpointRounding.AwayFromZero);
                    info_det.intGrabGen = Math.Round(Convert.ToDecimal(item.IngresoPorOtrosEmpleaodres), 2, MidpointRounding.AwayFromZero);
                    info_det.impRentEmpl = Math.Round(Convert.ToDecimal(item.ImpuestoRentaAsumidoPorEsteEmpleador), 2, MidpointRounding.AwayFromZero);
                    info_det.decimTer = (item.DecimoTercerSueldo) == null ? Convert.ToDecimal(0.00) : Math.Round(Convert.ToDecimal(item.DecimoTercerSueldo), 2, MidpointRounding.AwayFromZero);
                    info_det.decimCuar = (item.DecimoCuartoSueldo) == null ? Convert.ToDecimal(0.00) : Math.Round(Convert.ToDecimal(item.DecimoCuartoSueldo), 2, MidpointRounding.AwayFromZero);
                    info_det.fondoReserva = (item.FondosReserva) == null ? Convert.ToDecimal(0.00) : Math.Round(Convert.ToDecimal(item.FondosReserva), 2, MidpointRounding.AwayFromZero);
                    info_det.salarioDigno = 0;
                    info_det.otrosIngRenGrav = Math.Round(Convert.ToDecimal(item.OtrosIngresosRelacionDependencia), 2, MidpointRounding.AwayFromZero);
                    info_det.ingGravConEsteEmpl = Math.Round(Convert.ToDecimal(item.IngresosGravadorPorEsteEmpleador), 2, MidpointRounding.AwayFromZero);
                    info_det.sisSalNet = "1";
                    info_det.apoPerIess = Math.Round(Convert.ToDecimal(item.AportePErsonal), 2, MidpointRounding.AwayFromZero);
                    info_det.aporPerIessConOtrosEmpls = Math.Round(Convert.ToDecimal(item.IessPorOtrosEmpleadores), 2, MidpointRounding.AwayFromZero);
                    info_det.deducVivienda = Math.Round(Convert.ToDecimal(item.GastoVivienda), 2, MidpointRounding.AwayFromZero);
                    info_det.deducSalud = Math.Round(Convert.ToDecimal(item.GastoSalud), 2, MidpointRounding.AwayFromZero);
                    //info_det.deducEduca = Math.Round(Convert.ToDecimal(item.GastoEucacion), 2, MidpointRounding.AwayFromZero);
                    info_det.deducAliement = Math.Round(Convert.ToDecimal(item.GastoAlimentacion), 2, MidpointRounding.AwayFromZero);
                    info_det.deducVestim = Math.Round(Convert.ToDecimal(item.GastoVestimenta), 2, MidpointRounding.AwayFromZero);
                    info_det.deducArtycult = Math.Round(Convert.ToDecimal(item.GastoEucacion), 2, MidpointRounding.AwayFromZero);
                    info_det.basImp = Math.Round(Convert.ToDecimal((item.BaseImponibleGravada)), 2, MidpointRounding.AwayFromZero);
                    info_det.impRentCaus = Math.Round(Convert.ToDecimal((item.ImpuestoRentaCausado)), 2, MidpointRounding.AwayFromZero);
                    info_det.valRetAsuOtrosEmpls = Math.Round(Convert.ToDecimal((item.ValorImpuestoPorOtroEmplador)), 2, MidpointRounding.AwayFromZero);
                    info_det.valImpAsuEsteEmpl = Math.Round(Convert.ToDecimal((item.ValorImpuestoPorEsteEmplador)), 2, MidpointRounding.AwayFromZero);
                    info_det.valRet = Math.Round(Convert.ToDecimal((item.ValorImpuestoRetenidoTrabajador)), 2, MidpointRounding.AwayFromZero);
                    info_det.exoDiscap = Math.Round(Convert.ToDecimal(item.ExoneraionPorDiscapacidad), 2, MidpointRounding.AwayFromZero);
                    info_det.exoTerEd = Math.Round(Convert.ToDecimal(item.ExoneracionPorTerceraEdad), 2, MidpointRounding.AwayFromZero);

                    rdep.retRelDep.Add(info_det);
                });
                return rdep;
            }
            catch (Exception)
            {

                throw;
            }
        }        
    }
}
