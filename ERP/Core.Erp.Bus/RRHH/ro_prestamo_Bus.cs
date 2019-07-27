using Core.Erp.Bus.General;
using Core.Erp.Data.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public class ro_prestamo_Bus
    {
        ro_prestamo_Data odata = new ro_prestamo_Data();
        ro_prestamo_detalle_Data odata_det = new ro_prestamo_detalle_Data();
        public List<ro_prestamo_Info> get_list_prestamo(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                return odata.get_list_prestamo(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_prestamo_Info> get_list(int IdEmpresa,decimal IdEmpleado)
        {
            try
            {
                return odata.get_list(IdEmpresa,IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_prestamo_Info> get_list_aprobacion(int IdEmpresa)
        {
            try
            {
                return odata.get_list_aprobacion(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool aprobar_prestamo(int IdEmpresa, string[] Lista, string IdUsuarioAprueba)
        {
            try
            {
                return odata.aprobar_prestamo(IdEmpresa, Lista, IdUsuarioAprueba);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_prestamo_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdPrestamo)
        {
            try
            {
                ro_prestamo_Info info_ = new ro_prestamo_Info();
                info_= odata.get_info(IdEmpresa, IdEmpleado, IdPrestamo);
                info_.lst_detalle = odata_det.get_list(IdEmpresa, IdPrestamo);

                return info_;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_prestamo_Info info)
        {
            try
            {
                if (info.descuento_mensual)
                    get_calculomensual(info);
                if (info.descuento_quincena)
                    get_calculoquincenal(info);
                info.NumCuotas = info.lst_detalle.Count();
                info.Fecha = info.Fecha_PriPago;
                if (odata.guardarDB(info))
                {
                    info.IdPrestamo = info.IdPrestamo;
                    odata_det = new ro_prestamo_detalle_Data();
                    info.lst_detalle.ForEach(v=> { v.IdEmpresa = info.IdEmpresa; v.IdPrestamo = info.IdPrestamo;  });
                    return odata_det.guardarDB(info.lst_detalle);
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_prestamo_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(ro_prestamo_Info info)
        {
            try
            {
                odata = new ro_prestamo_Data();
                info.NumCuotas = info.lst_detalle.Count();
                info.Fecha = info.Fecha_PriPago;
                if (odata.modificarDB(info))
                {
                    info.IdPrestamo = info.IdPrestamo;
                    odata_det = new ro_prestamo_detalle_Data();
                    info.lst_detalle.ForEach(v => { v.IdEmpresa = info.IdEmpresa; v.IdPrestamo = info.IdPrestamo; });
                    odata_det.eliminarDB(info);
                    return odata_det.guardarDB(info.lst_detalle);
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_prestamo_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(ro_prestamo_Info info)
        {
            try
            {
                if (odata.anularDB(info))
                {
                    info.IdPrestamo = info.IdPrestamo;
                    odata_det = new ro_prestamo_detalle_Data();
                    return odata_det.AnularD(info);
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Abono(ro_prestamo_Info info)
        {
            try
            {
                return odata.Abono(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

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

                double diferencia =info.MontoSol- info.lst_detalle.Sum(v => v.TotalCuota);
                if(diferencia!=0)
                {
                    foreach (var item in info.lst_detalle)
                    {
                        item.TotalCuota = item.TotalCuota + diferencia;
                        break;
                    }
                }

                return info;
            }
            catch (Exception )
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
                    else {
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

                    if(i==1)
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

        public ro_prestamo_Info get_calculo_prestamo(ro_prestamo_Info info)
        {
            try
            {
                if (info.descuento_mensual)
                    info= get_calculomensual(info);
                if (info.descuento_quincena)
                    info= get_calculoquincenal(info);
                if (info.descuento_men_quin)
                    info = get_calculoquincenal_y_men(info);
                return info;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public ro_prestamo_Info get_info(int IdEmpresa, decimal IdPrestamo)
        {
            try
            {
                ro_prestamo_Info info = new ro_prestamo_Info();
                info = odata.get_info(IdEmpresa, IdPrestamo);
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
