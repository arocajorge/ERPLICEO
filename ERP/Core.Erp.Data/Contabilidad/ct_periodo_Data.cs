using Core.Erp.Data.Compras.Base;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_periodo_Data
    {
        public List<ct_periodo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {

            try
            {

                List<ct_periodo_Info> Lista;

                Entities_general Context_g = new Entities_general();
                Entities_contabilidad Context = new Entities_contabilidad();
                var lst_mes = (from q in Context_g.tb_mes
                               select new tb_mes_Info
                               {
                                   idMes = q.idMes,
                                   smes = q.smes
                               }).ToList();
                Context_g.Dispose();

                var lst_periodo = (from q in Context.vwct_periodo
                                   where q.IdEmpresa == IdEmpresa
                                   select new ct_periodo_Info
                                   {
                                       IdPeriodo = q.IdPeriodo,
                                       IdEmpresa = q.IdEmpresa,
                                       IdanioFiscal = q.IdanioFiscal,
                                       pe_FechaIni = q.pe_FechaIni,
                                       pe_FechaFin = q.pe_FechaFin,
                                       pe_mes = q.pe_mes,
                                       pe_cerrado = q.pe_cerrado,
                                       pe_estado = q.pe_estado,
                                       AnioMes = q.AnioMes,
                                       EstadoBool = q.pe_estado == "A" ? true : false
                                   }).ToList();


                if (mostrar_anulados == true)
                    Lista = (from q in lst_periodo
                             join m in lst_mes
                             on q.pe_mes equals m.idMes
                             where q.IdEmpresa == IdEmpresa
                             select new ct_periodo_Info
                             {
                                 IdPeriodo = q.IdPeriodo,
                                 IdEmpresa = q.IdEmpresa,
                                 IdanioFiscal = q.IdanioFiscal,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_mes = q.pe_mes,
                                 pe_cerrado = q.pe_cerrado,
                                 pe_estado = q.pe_estado,
                                 smes = m.smes,
                                 AnioMes = q.AnioMes,

                                 EstadoBool = q.pe_estado == "A" ? true : false
                             }).ToList();
                else
                    Lista = (from q in lst_periodo
                             join m in lst_mes
                              on q.pe_mes equals m.idMes
                             where q.IdEmpresa == IdEmpresa
                              && q.pe_estado == "A"
                             select new ct_periodo_Info
                             {
                                 IdPeriodo = q.IdPeriodo,
                                 IdEmpresa = q.IdEmpresa,
                                 IdanioFiscal = q.IdanioFiscal,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_mes = q.pe_mes,
                                 pe_cerrado = q.pe_cerrado,
                                 pe_estado = q.pe_estado,
                                 smes = m.smes,
                                 AnioMes = q.AnioMes,

                                 EstadoBool = q.pe_estado == "A" ? true : false
                             }).ToList();

                Lista.ForEach(q => q.nom_periodo_combo = q.IdanioFiscal + " " + q.smes);

                Context.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_periodo_Info get_info(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                ct_periodo_Info info = new ct_periodo_Info();

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_periodo Entity = Context.ct_periodo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPeriodo == IdPeriodo);
                    if (Entity == null) return null;
                    info = new ct_periodo_Info
                    {
                        IdPeriodo = Entity.IdPeriodo,
                        IdEmpresa = Entity.IdEmpresa,
                        IdanioFiscal = Entity.IdanioFiscal,
                        pe_FechaIni = Entity.pe_FechaIni,
                        pe_FechaFin = Entity.pe_FechaFin,
                        pe_mes = Entity.pe_mes,
                        pe_cerrado_bool = Entity.pe_cerrado == "S" ? true : false,
                        pe_estado = Entity.pe_estado
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_periodo_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_periodo Entity = new ct_periodo
                    {
                        IdPeriodo = info.IdPeriodo,
                        IdEmpresa = info.IdEmpresa,
                        IdanioFiscal = info.IdanioFiscal,
                        pe_FechaIni = info.pe_FechaIni.Date,
                        pe_FechaFin = info.pe_FechaFin.Date,
                        pe_mes = info.pe_mes,
                        pe_cerrado = info.pe_cerrado_bool == true ? "S" : "N",
                        pe_estado = info.pe_estado = "A"
                    };
                    Context.ct_periodo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(ct_periodo_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_periodo Entity = Context.ct_periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == info.IdPeriodo);
                    if (Entity == null)
                        return false;

                    Entity.pe_FechaFin = info.pe_FechaFin.Date;
                    Entity.pe_FechaIni = info.pe_FechaIni.Date;
                    Entity.pe_cerrado = info.pe_cerrado_bool == true ? "S" : "N";
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ct_periodo_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_periodo Entity = Context.ct_periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == info.IdPeriodo);
                    if (Entity == null)
                        return false;
                    Entity.pe_estado = info.pe_estado = "I";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_periodo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPeriodo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarFechaTransaccion(int IdEmpresa, DateTime Fecha, cl_enumeradores.eModulo Modulo, int IdSucursal, ref string mensaje)
        {
            Entities_contabilidad db_conta = new Entities_contabilidad();
            Entities_general db_general = new Entities_general();
            try
            {
                Fecha = Fecha.Date;
                int Periodo = Convert.ToInt32(Fecha.ToString("yyyyMM"));
                string sModulo = Modulo.ToString();
                ct_CierrePorModuloPorSucursal CierreModulo = new ct_CierrePorModuloPorSucursal();
                var empresa = db_general.tb_empresa.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                if(empresa != null)
                {
                    if(empresa.em_fechaInicioActividad > Fecha.Date)
                    {
                        mensaje = "La fecha de la transacción es menor al inicio de actividades de la empresa en el sistema FIXED ERP: " + Fecha.Date.ToString("dd/MM/yyyy") ;
                        return false;
                    }
                }

                ct_periodo per = db_conta.ct_periodo.Where(q => q.IdEmpresa == IdEmpresa && q.IdPeriodo == Periodo).FirstOrDefault();

                if(per == null)
                {
                    mensaje = "El periodo "+Periodo+" de la transacción no se encuentra registrado.";
                    return false;
                }

                if (per.pe_cerrado == "S")
                {
                    mensaje = "El periodo " + Periodo + " se encuentra cerrado.";
                    return false;
                }

                switch (Modulo)
                {
                    case cl_enumeradores.eModulo.INV:
                        using (Entities_inventario db = new Entities_inventario())
                        {
                            var param = db.in_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de inventario";
                                return false;
                            }
                            /*
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de importación";
                                return false;
                            }
                            */

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "INV" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if(CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de inventario";
                                    return false;
                                }
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.FAC:
                        using (Entities_facturacion db = new Entities_facturacion())
                        {
                            var param = db.fa_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de facturación";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de facturación";
                                return false;
                            }
                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "FAC" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de facturación";
                                    return false;
                                }
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.COM:
                        using (Entities_compras db = new Entities_compras())
                        {
                            var param = db.com_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de compras";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de compras";
                                return false;
                            }
                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "COMP" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de compras";
                                return false;
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.ACF:
                        using (Entities_activo_fijo db = new Entities_activo_fijo())
                        {
                            var param = db.Af_Parametros.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de activo fijo";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de activo fijo";
                                return false;
                            }
                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "ACTF" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de activo fijo";
                                return false;
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.RRHH:
                        using (Entities_rrhh db = new Entities_rrhh())
                        {

                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "ROL" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de recursos humanos";
                                return false;
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.IMP:
                        using (Entities_importacion db = new Entities_importacion())
                        {
                            var param = db.imp_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de importación";
                                return false;
                            }
                            /*
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de importación";
                                return false;
                            }
                            */
                        }
                        break;
                    case cl_enumeradores.eModulo.CONTA:
                        using (Entities_contabilidad db = new Entities_contabilidad())
                        {
                            var param = db.ct_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de contabilidad";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de contabilidad";
                                return false;
                            }
                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CONTA" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de contabilidad";
                                return false;
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.CAJA:
                        using (Entities_caja db = new Entities_caja())
                        {
                            var param = db.caj_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de caja";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de caja";
                                return false;
                            }
                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CAJ" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de caja";
                                return false;
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.BANCO:
                        using (Entities_banco db = new Entities_banco())
                        {
                            var param = db.ba_parametros.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de bancos";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de bancos";
                                return false;
                            }
                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "BAN" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de bancos";
                                return false;
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.CXC:
                        using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                        {
                            var param = db.cxc_Parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de cuentas por cobrar";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de cuentas por cobrar";
                                return false;
                            }
                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CXC" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de cuentas por cobrar";
                                return false;
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.CXP:
                        using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                        {
                            var param = db.cp_parametros.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de cuentas por pagar";
                                return false;
                            }
                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de cuentas por pagar";
                                return false;
                            }
                        }
                        CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CXP" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                        if (CierreModulo != null)
                        {
                            if (Fecha.Date <= CierreModulo.FechaFin)
                            {
                                mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de cuentas por pagar";
                                return false;
                            }
                        }
                        break;
                }

                db_general.Dispose();
                db_conta.Dispose();
                return true;
            }
            catch (Exception)
            {
                db_conta.Dispose();
                db_general.Dispose();
                throw;
            }
        }
    }
}
