using Core.Erp.Data.General;
using Core.Erp.Info.General;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_PrestamoMasivo_Data
    {
        ro_Parametros_Data odata = new ro_Parametros_Data();
        ro_prestamo_Data odata_prestamo = new ro_prestamo_Data();

        public List<ro_PrestamoMasivo_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, int IdSucursal, bool MostrarAnulados)
        {
            try
            {
                List<ro_PrestamoMasivo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_PrestamoMasivo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.Fecha_PriPago >= fecha_ini
                             && q.Fecha_PriPago <= fecha_fin
                             && q.Estado == (MostrarAnulados== true ? q.Estado : true)
                             select new ro_PrestamoMasivo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdCarga = q.IdCarga,
                                 Fecha_PriPago = q.Fecha_PriPago,
                                 NumCuotas = q.NumCuotas,
                                 descuento_quincena = q.descuento_quincena,
                                 descuento_mensual = q.descuento_mensual,
                                 descuento_men_quin = q.descuento_men_quin,
                                 Observacion = q.Observacion,
                                 Estado = q.Estado
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_PrestamoMasivo_Info get_info(int IdEmpresa, int IdSucursal, decimal IdCarga)
        {
            try
            {
                ro_PrestamoMasivo_Info info = new ro_PrestamoMasivo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_PrestamoMasivo Entity = Context.ro_PrestamoMasivo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdCarga == IdCarga);
                    if (Entity == null) return null;

                    info = new ro_PrestamoMasivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdCarga = Entity.IdCarga,
                        Fecha_PriPago = Entity.Fecha_PriPago,
                        NumCuotas = Entity.NumCuotas,
                        descuento_mensual = Entity.descuento_mensual,
                        descuento_men_quin = Entity.descuento_men_quin,
                        descuento_quincena = Entity.descuento_quincena,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_PrestamoMasivo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCarga) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_PrestamoMasivo_Info info)
        {
            try
            {
                var ro_parametro = odata.get_info(info.IdEmpresa);
                var Secuencia = 1;
                decimal IdPrestamo = odata_prestamo.get_id(info.IdEmpresa);
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_PrestamoMasivo Entity = new ro_PrestamoMasivo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdCarga = info.IdCarga = get_id(info.IdEmpresa),
                        Fecha_PriPago = info.Fecha_PriPago,
                        NumCuotas = info.NumCuotas,
                        descuento_mensual = info.descuento_mensual,
                        descuento_men_quin = info.descuento_men_quin,
                        descuento_quincena = info.descuento_quincena,
                        Observacion = info.Observacion,
                        Estado = info.Estado = true,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_PrestamoMasivo.Add(Entity);

                    foreach (var item in info.lst_detalle)
                    {
                        ro_PrestamoMasivo_Det Entity_Det = new ro_PrestamoMasivo_Det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdCarga = info.IdCarga,
                            Secuencia = Secuencia++,
                            IdEmpleado = item.IdEmpleado,
                            IdRubro = item.IdRubro,
                            Monto = item.Monto,
                            NumCuotas = item.NumCuotas
                        };

                        var info_prestamo = new ro_prestamo_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdPrestamo = IdPrestamo++,
                            IdEmpleado = item.IdEmpleado,
                            IdRubro = item.IdRubro,
                            descuento_mensual = info.descuento_mensual,
                            descuento_men_quin = info.descuento_men_quin,
                            descuento_quincena = info.descuento_quincena,
                            Fecha = info.Fecha_PriPago.Date,
                            MontoSol = item.Monto,
                            NumCuotas = item.NumCuotas,
                            Fecha_PriPago = info.Fecha_PriPago.Date,
                            Observacion = info.Observacion,
                            GeneraOP = true,
                            Estado = true,
                            EstadoAprob = ro_parametro.EstadoCreacionPrestamos,
                            IdUsuario = info.IdUsuario,
                            Fecha_Transac = info.Fecha_Transac = DateTime.Now
                        };

                        if (info.descuento_men_quin == true)
                        {
                            info_prestamo = get_calculoquincenal_y_men(info_prestamo);
                        }

                        if (info.descuento_quincena == true)
                        {
                            info_prestamo = get_calculoquincenal(info_prestamo);
                        }

                        if (info.descuento_mensual == true)
                        {
                            info_prestamo = get_calculomensual(info_prestamo);
                        }

                        if (info_prestamo == null)
                        {
                            return false;
                        }
                        else
                        {
                            ro_prestamo Entity_Cab_Prestamo = new ro_prestamo
                            {
                                IdEmpresa = info_prestamo.IdEmpresa,
                                IdPrestamo = info_prestamo.IdPrestamo,
                                IdEmpleado = info_prestamo.IdEmpleado,
                                IdRubro = info_prestamo.IdRubro,
                                descuento_mensual = info_prestamo.descuento_mensual,
                                descuento_men_quin = info_prestamo.descuento_men_quin,
                                descuento_quincena = info_prestamo.descuento_quincena,
                                Fecha = info_prestamo.Fecha_PriPago.Date,
                                MontoSol = info_prestamo.MontoSol,
                                NumCuotas = info_prestamo.NumCuotas,
                                Fecha_PriPago = info_prestamo.Fecha_PriPago.Date,
                                Observacion = info_prestamo.Observacion,
                                GeneraOP = info_prestamo.GeneraOP,
                                Estado = info_prestamo.Estado,
                                EstadoAprob = info_prestamo.EstadoAprob,
                                IdUsuario = info_prestamo.IdUsuario,
                                Fecha_Transac = info_prestamo.Fecha_Transac
                            };

                            Entity_Det.IdPrestamo = Entity_Cab_Prestamo.IdPrestamo;
                            Context.ro_prestamo.Add(Entity_Cab_Prestamo);
                            Context.ro_PrestamoMasivo_Det.Add(Entity_Det);

                            foreach (var item_prest in info_prestamo.lst_detalle)
                            {
                                ro_prestamo_detalle Entity_Det_Prestamo = new ro_prestamo_detalle
                                {
                                    IdEmpresa = info_prestamo.IdEmpresa,
                                    IdPrestamo = info_prestamo.IdPrestamo,
                                    NumCuota = item_prest.NumCuota,
                                    SaldoInicial = item_prest.SaldoInicial,
                                    TotalCuota = item_prest.TotalCuota,
                                    Saldo = item_prest.Saldo,
                                    FechaPago = item_prest.FechaPago,
                                    EstadoPago = item_prest.EstadoPago,
                                    Estado = true,
                                    Observacion_det = item_prest.Observacion_det,
                                    IdNominaTipoLiqui = item_prest.IdNominaTipoLiqui
                                };
                                Context.ro_prestamo_detalle.Add(Entity_Det_Prestamo);
                            }
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_prestamo_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        
        public bool anularDB(ro_PrestamoMasivo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_PrestamoMasivo Entity = Context.ro_PrestamoMasivo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCarga == info.IdCarga).FirstOrDefault();
                    var prestamo_masivo_detalle = Context.ro_PrestamoMasivo_Det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCarga == info.IdCarga).ToList();

                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    Entity.MotiAnula = info.MotiAnula;

                    if (prestamo_masivo_detalle!= null || prestamo_masivo_detalle.Count() >0)
                    {
                        foreach (var item in prestamo_masivo_detalle)
                        {
                            ro_prestamo Entity_Prestamo = Context.ro_prestamo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPrestamo == item.IdPrestamo).FirstOrDefault();
                            Entity_Prestamo.Estado = false;
                        }
                    }

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*CUOTAS DE PRESTAMOS*/
        public ro_prestamo_Info get_calculomensual(ro_prestamo_Info info)
        {
            try
            {
                info.lst_detalle = new List<ro_prestamo_detalle_Info>();
                int periodo = Convert.ToInt32(info.NumCuotas);
                double valor_cuota = info.MontoSol / info.NumCuotas;
                double saldo = info.MontoSol;
                DateTime fecha_pago = info.Fecha_PriPago;
                info.MontoSol = info.MontoSol;
                List<ro_prestamo_detalle_Info> listaDetalle = new List<ro_prestamo_detalle_Info>();
                for (int i = 1; i <= periodo; i++)
                {
                    ro_prestamo_detalle_Info item = new ro_prestamo_detalle_Info();

                    if (i == 1)
                    {
                        var fecha_pago_sgte = fecha_pago;
                        int fin_mes = DateTime.DaysInMonth(fecha_pago_sgte.Year, fecha_pago_sgte.Month);
                        fecha_pago = new DateTime(fecha_pago_sgte.Year, fecha_pago_sgte.Month, fin_mes);
                    }
                    else
                    {
                        var fecha_pago_sgte = fecha_pago.AddMonths(1);
                        int fin_mes = DateTime.DaysInMonth(fecha_pago_sgte.Year, fecha_pago_sgte.Month);

                        fecha_pago = new DateTime(fecha_pago_sgte.Year, fecha_pago_sgte.Month, fin_mes);
                    }

                    item.FechaPago = info.Fecha_PriPago;
                    item.NumCuota = i;
                    item.TotalCuota = valor_cuota;
                    item.Saldo = info.MontoSol;
                    item.Saldo = saldo - item.TotalCuota;
                    item.FechaPago = fecha_pago;
                    item.EstadoPago = "PEN";
                    item.Observacion_det = "Cuota número " + i + " fecha pago " + fecha_pago.ToString("dd/MM/yyyy");
                    item.IdNominaTipoLiqui = 2;

                    saldo = saldo - valor_cuota;
                    item.TotalCuota = Math.Round(item.TotalCuota, 2);
                    item.Saldo = Math.Round(item.Saldo, 2);

                    info.lst_detalle.Add(item);

                }

                double diferencia = info.MontoSol - info.lst_detalle.Sum(v => v.TotalCuota);
                if (diferencia != 0)
                {
                    foreach (var item in info.lst_detalle)
                    {
                        item.TotalCuota = item.TotalCuota + diferencia;
                        break;
                    }
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ro_prestamo_Info get_calculoquincenal(ro_prestamo_Info info)
        {
            try
            {
                info.lst_detalle = new List<ro_prestamo_detalle_Info>();
                int periodo = Convert.ToInt32(info.NumCuotas);
                double valor_cuota = info.MontoSol / info.NumCuotas;
                double saldo = info.MontoSol;
                DateTime fecha_pago = info.Fecha_PriPago;
                info.MontoSol = info.MontoSol;
                List<ro_prestamo_detalle_Info> listaDetalle = new List<ro_prestamo_detalle_Info>();
                for (int i = 1; i <= periodo; i++)
                {
                    ro_prestamo_detalle_Info item = new ro_prestamo_detalle_Info();

                    if (i == 1)
                    {
                        //item.FechaPago = fecha_pago;
                        fecha_pago = new DateTime(fecha_pago.Year, fecha_pago.Month, 15);
                    }
                    else
                    {
                        var fecha_pago_sgte = fecha_pago.AddMonths(1);
                        fecha_pago = new DateTime(fecha_pago_sgte.Year, fecha_pago_sgte.Month, 15);
                    }

                    item.FechaPago = info.Fecha_PriPago;
                    item.NumCuota = i;
                    item.TotalCuota = valor_cuota;
                    item.Saldo = info.MontoSol;
                    item.Saldo = saldo - item.TotalCuota;
                    item.FechaPago = fecha_pago;
                    item.EstadoPago = "PEN";
                    item.Observacion_det = "Cuota número " + i + " fecha pago " + fecha_pago.ToString("dd/MM/yyyy");
                    item.IdNominaTipoLiqui = 1;

                    saldo = saldo - valor_cuota;
                    item.TotalCuota = Math.Round(item.TotalCuota, 2);
                    item.Saldo = Math.Round(item.Saldo, 2);
                    info.lst_detalle.Add(item);

                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ro_prestamo_Info get_calculoquincenal_y_men(ro_prestamo_Info info)
        {
            try
            {
                info.lst_detalle = new List<ro_prestamo_detalle_Info>();
                int periodo = Convert.ToInt32(info.NumCuotas);
                double valor_cuota = info.MontoSol / info.NumCuotas;
                double saldo = info.MontoSol;
                DateTime fecha_pago = info.Fecha_PriPago;
                info.MontoSol = info.MontoSol;
                List<ro_prestamo_detalle_Info> listaDetalle = new List<ro_prestamo_detalle_Info>();
                for (int i = 1; i <= periodo; i++)
                {
                    ro_prestamo_detalle_Info item = new ro_prestamo_detalle_Info();

                    if (i == 1)
                    {
                        //fecha_pago = info.Fecha_PriPago; 
                        fecha_pago = new DateTime(fecha_pago.Year, fecha_pago.Month, 15);
                    }
                    else
                    {
                        fecha_pago = fecha_pago.AddDays(1);
                    }
                    if (fecha_pago.Day > 15)
                    {
                        int fin_mes = DateTime.DaysInMonth(fecha_pago.Year, fecha_pago.Month);
                        fecha_pago = new DateTime(fecha_pago.Year, fecha_pago.Month, fin_mes);
                        //fecha_pago = Convert.ToDateTime(fin_mes + fecha_pago.Month.ToString() + "/" + fecha_pago.Year.ToString());
                    }
                    else
                    {
                        fecha_pago = new DateTime(fecha_pago.Year, fecha_pago.Month, 15);
                        //fecha_pago = Convert.ToDateTime("15/" + fecha_pago.Month.ToString() + "/" + fecha_pago.Year.ToString());
                    }

                    item.FechaPago = info.Fecha_PriPago;
                    item.NumCuota = i;
                    item.TotalCuota = valor_cuota;
                    item.Saldo = info.MontoSol;
                    item.Saldo = saldo - item.TotalCuota;
                    item.FechaPago = fecha_pago;
                    item.EstadoPago = "PEN";
                    item.Observacion_det = "Cuota número " + i + " fecha pago " + fecha_pago.ToString("dd/MM/yyyy");
                    if (item.FechaPago.Day > 15)
                        item.IdNominaTipoLiqui = 2;
                    else
                        item.IdNominaTipoLiqui = 1;
                    saldo = saldo - valor_cuota;
                    item.TotalCuota = Math.Round(item.TotalCuota, 2);
                    item.Saldo = Math.Round(item.Saldo, 2);
                    info.lst_detalle.Add(item);

                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
