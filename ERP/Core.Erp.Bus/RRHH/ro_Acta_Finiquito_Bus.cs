using Core.Erp.Data.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public  class ro_Acta_Finiquito_Bus
    {

        #region variables
        ro_Parametros_Bus bus_parametros = new ro_Parametros_Bus();
        ro_Parametros_Info info_parametro = new ro_Parametros_Info();
        ro_Acta_Finiquito_Data odata = new ro_Acta_Finiquito_Data();
        ro_Acta_Finiquito_Detalle_Data odata_detalle = new ro_Acta_Finiquito_Detalle_Data();
        ro_Acta_Finiquito_Info _Info = new ro_Acta_Finiquito_Info();
        List<ro_Acta_Finiquito_Detalle_Info> lst_valores_x_indegnizacion = new List<ro_Acta_Finiquito_Detalle_Info>();
        ro_contrato_Bus bus_contrato = new ro_contrato_Bus();
        ro_contrato_Info info_contrato = new ro_contrato_Info();
        ro_empleado_novedad_Bus bus_novedades = new ro_empleado_novedad_Bus();
        ro_empleado_novedad_det_Bus bus_novedad = new ro_empleado_novedad_det_Bus();
        ro_prestamo_detalle_Bus bus_prestamo = new ro_prestamo_detalle_Bus();
        ro_rol_detalle_x_rubro_acumulado_Bus bus_rubros_acumulados = new ro_rol_detalle_x_rubro_acumulado_Bus();
        ro_rubros_calculados_Bus bus_rubros_calculados = new ro_rubros_calculados_Bus();
        ro_rubros_calculados_Info info_rubros_calculados = new ro_rubros_calculados_Info();
        double sueldo = 0;
        int dias_trabajados = 0;
        double sueldo_base = 0;

        #endregion
        public List<ro_Acta_Finiquito_Info> get_list(int IdEmpresa)
        {
            try
            {
                return odata.get_list(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ro_Acta_Finiquito_Info> get_list_pre_liquidacion(int IdEmpresa)
        {
            try
            {
                return odata.get_list_pre_liquidacion(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_Acta_Finiquito_Info get_info(int IdEmpresa , decimal IdActaFiniquito)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdActaFiniquito);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_Acta_Finiquito_Info info)
        {
            try
            {
                info_contrato = bus_contrato.get_info_contato_a_liquidar(info.IdEmpresa,info.IdEmpleado);
                odata = new ro_Acta_Finiquito_Data();
                info.Ingresos = info.lst_detalle.Where(v => v.Valor > 0).Sum(v => v.Valor);
                info.Egresos = info.lst_detalle.Where(v => v.Valor<0). Sum(v => v.Valor);
                info.IdContrato = info_contrato.IdContrato;
                if ( odata.guardarDB(info))
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_Acta_Finiquito_Info info)
        {
            try
            {
                odata = new ro_Acta_Finiquito_Data();
                info.Ingresos = info.lst_detalle.Where(v => v.Valor > 0).Sum(v => v.Valor);
                info.Egresos = info.lst_detalle.Where(v => v.Valor < 0).Sum(v => v.Valor);
                if (odata.modificarDB(info))
                {

                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_Acta_Finiquito_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Liquidar(ro_Acta_Finiquito_Info info)
        {
            try
            {
                return odata.Liquidar(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_Acta_Finiquito_Info ObtenerIndemnizacion(ro_Acta_Finiquito_Info info)
        {
            try
            {
                cl_enumeradores.eTipoTerminacioncontratoRRHH TipoterminacionContrato;
                TipoterminacionContrato = (cl_enumeradores.eTipoTerminacioncontratoRRHH)Enum.Parse(typeof(cl_enumeradores.eTipoTerminacioncontratoRRHH), info.IdCausaTerminacion);

                info_parametro = bus_parametros.get_info(info.IdEmpresa);
                info_rubros_calculados = bus_rubros_calculados.get_info(info.IdEmpresa);
                _Info = info;

                switch (TipoterminacionContrato)
                {
                    
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_01://Por las causas legalmente previstas en el contrato
                        ObtenerIndemnizacionXDesahucio();
                        break;
                    
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_02://Por acuerdo de las partes (Renuncia)
                        ObtenerIndemnizacionXDesahucio();
                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_03:
                        break;
                        
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_04:// Por muerte o incapacidad del empleador o extinción de la persona jurídica contratante
                        ObtenerIndemnizacionXDespidoIntempestivo();
                        ObtenerIndemnizacionXDesahucio();
                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_05:
                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_06:
                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_07:
                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_08://Por voluntad del trabajador previo visto bueno
                        ObtenerIndemnizacionXDespidoIntempestivo();
                        ObtenerIndemnizacionXDesahucio();
                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_09://Por desahucio
                        ObtenerIndemnizacionXDesahucio();

                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_10:
                        ObtenerIndemnizacionXDespidoIntempestivo();

                        break;
                    case cl_enumeradores.eTipoTerminacioncontratoRRHH.CTL_11:
                        break;
                    default:
                        break;
                }
          //      Obtenersueldo_no_pagados();
           //     ObtenerAportePersonal();
                ObtenerProvisionDecimoIII();
                ObtenerProvisionDecimoIV();
                ObtenerProvisionFondoReserva();
                ObtenerProvisionVacaciones();
                ObtenerCuotasPrestamosPendientes();
                ObtenerNovedadesPendientes();
                if (info.EsMujerEmbarazada)
                    ObtenerIndemnizacionXDespidoMujerEmbarazada();
                if (info.EsDirigenteSindical)
                    ObtenerIndemnizacionXDespidoDirigenteSindical();
                if (info.EsPorEnfermedadNoProfesional)
                    ObtenerIndemnizacionXDespidoEnfermedadNoProfesional();
                if (info.EsPorDiscapacidad)
                    ObtenerIndemnizacionXDespidoDiscapacitado();

                
                info.lst_detalle = lst_valores_x_indegnizacion;
                int sec =1;
                info.lst_detalle.ForEach(v => v.IdSecuencia = sec++);
                return info;
            }
            catch (Exception)
            {
                throw;
            }

        }
        //ART.185 DESAHUCIO
        private Boolean ObtenerIndemnizacionXDesahucio()
        {
            try
            {
                int anioTrabajados = 0;
                double totalRubroAcumulado = 0;
                TimeSpan dias;
                dias = _Info.FechaSalida - _Info.FechaIngreso;

                if (dias.TotalDays < 360)
                    return false;

                anioTrabajados = Convert.ToInt32(Math.Floor(dias.TotalDays / 360));

                if (anioTrabajados < 1)

                    return false;


                //CORRESPONDE EL 25% X CADA AÑO/FRACCION DE AÑO DE TRABAJO
                totalRubroAcumulado = _Info.UltimaRemuneracion * 0.25 * anioTrabajados;
                if (totalRubroAcumulado > 0)
                {
                    ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                    item.IdEmpresa = _Info.IdEmpresa;
                    item.IdEmpleado = _Info.IdEmpleado;
                    item.IdActaFiniquito = _Info.IdActaFiniquito;
                    item.Observacion = "Bonificación por Desahucio según Art.185";
                    item.Valor =Math.Round( totalRubroAcumulado,2);
                    item.IdRubro = info_parametro.IdRubro_acta_finiquito;
                    lst_valores_x_indegnizacion.Add(item);
                }

                return true;
            }
            catch (Exception )
            {
                throw;

            }
        }
        //ART.188 INDEMNIZACION POR DESPIDO INTEMPESTIVO
        private bool ObtenerIndemnizacionXDespidoIntempestivo()
        {
            try
            {


                int anioTrabajados = 0;
                double totalRubroAcumulado = 0;


                anioTrabajados = Math.Abs(_Info.FechaSalida.Year - _Info.FechaIngreso.Year);


                if (anioTrabajados <= 3)
                {
                    totalRubroAcumulado = _Info.UltimaRemuneracion * 3; //HASTA 3 AÑOS DE TRABAJO RECIBE 3 MESES DE REMUNERACION

                }
                else
                {
                    if (anioTrabajados <= 25)
                    {//PUEDE ACUMULAR UNICAMENTE HASTA 25 MESES DE REMUNERACION X CADA AÑO DE TRABAJO
                        totalRubroAcumulado = _Info.UltimaRemuneracion * anioTrabajados;
                    }
                }

                if (totalRubroAcumulado > 0)
                {
                    ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();

                    item.IdEmpresa = _Info.IdEmpresa;
                    item.IdEmpleado = _Info.IdEmpleado;
                    item.IdActaFiniquito = _Info.IdActaFiniquito;
                    item.Observacion = "Indemnización por Despido Intempestivo según Art.188";
                    item.Valor =Math.Round( totalRubroAcumulado,2);
                    item.IdRubro = info_parametro.IdRubro_acta_finiquito;
                    lst_valores_x_indegnizacion.Add(item);

                }



                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        //ART.187 INDEMNIZACION POR GARANTIA PARA DIRIGENTES SINDICALES
        private Boolean ObtenerIndemnizacionXDespidoDirigenteSindical()
        {
            try
            {

                double totalRubroAcumulado = 0;

                //VERIFICA SI ES DIRIGENTE SINDICAL
                if (Convert.ToBoolean(_Info.EsDirigenteSindical))
                {
                    totalRubroAcumulado = _Info.UltimaRemuneracion * 12;//EQUIVALE A 1 AÑO DE REMUNERACION

                    if (totalRubroAcumulado > 0)
                    {
                        ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();

                        item.IdEmpresa = _Info.IdEmpresa;
                        item.IdEmpleado = _Info.IdEmpleado;
                        item.IdActaFiniquito = _Info.IdActaFiniquito;
                        item.Observacion = "Indemnización por Garantía Dirigentes Sindicales según Art.187";
                        item.Valor =Math.Round( totalRubroAcumulado,2);
                        item.IdRubro = info_parametro.IdRubro_acta_finiquito;
                        lst_valores_x_indegnizacion.Add(item);

                    }

                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        //ART.187 INDEMNIZACION POR GARANTIA PARA MUJER EMBARAZADA
        private Boolean ObtenerIndemnizacionXDespidoMujerEmbarazada()
        {
            try
            {

                double totalRubroAcumulado = 0;

                //VERIFICA SI ES MUJER EMBARAZADA
                if (Convert.ToBoolean(_Info.EsMujerEmbarazada))
                {
                    totalRubroAcumulado = _Info.UltimaRemuneracion * 12;//EQUIVALE A 1 AÑO DE REMUNERACION

                    if (totalRubroAcumulado > 0)
                    {
                        ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();

                        item.IdEmpresa = _Info.IdEmpresa;
                        item.IdEmpleado = _Info.IdEmpleado;
                        item.IdActaFiniquito = _Info.IdActaFiniquito;
                        item.Observacion = "Indemnización por despido y declaratoria de ineficaz de la trabajadora embarazada";
                        item.Valor =Math.Round( totalRubroAcumulado,2);
                        item.IdRubro = info_parametro.IdRubro_acta_finiquito;
                        lst_valores_x_indegnizacion.Add(item);
                    }

                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        //ART.51 INDEMNIZACION POR ESTABILIDAD LABORAL - LEY DE DISCAPACIDAD
        private Boolean ObtenerIndemnizacionXDespidoDiscapacitado()
        {
            try
            {

                double totalRubroAcumulado = 0;

                //VERIFICA SI ES DISCAPACITADO
                if (Convert.ToBoolean(_Info.EsPorDiscapacidad))
                {
                    totalRubroAcumulado = _Info.UltimaRemuneracion * 18;//EQUIVALE A 18 MESES DE REMUNERACION DEL MEJOR SUELDO

                    if (totalRubroAcumulado > 0)
                    {
                        ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();

                        item.IdEmpresa = _Info.IdEmpresa;
                        item.IdEmpleado = _Info.IdEmpleado;
                        item.IdActaFiniquito = _Info.IdActaFiniquito;
                        item.Observacion = "Indemnización por Estabilidad Laboral Art.51 - Ley de Discapacidad";
                        item.Valor =Math.Round( totalRubroAcumulado,2);
                        item.IdRubro = info_parametro.IdRubro_acta_finiquito;

                        lst_valores_x_indegnizacion.Add(item);
                    }

                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        //ART.179 INDEMNIZACION POR NO RECIBIR AL TRABAJADOR EN CASO DE ENFERMEDAD NO PROFESIONAL
        private Boolean ObtenerIndemnizacionXDespidoEnfermedadNoProfesional()
        {
            try
            {

                double totalRubroAcumulado = 0;

                //VERIFICA SI ES CASO DE ENFERMEDAD NO PROFESIONAL
                if (Convert.ToBoolean(_Info.EsPorEnfermedadNoProfesional))
                {
                    totalRubroAcumulado = _Info.UltimaRemuneracion * 6;//EQUIVALE A 6 MESES DE REMUNERACION

                    if (totalRubroAcumulado > 0)
                    {
                        ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();

                        item.IdEmpresa = _Info.IdEmpresa;
                        item.IdEmpleado = _Info.IdEmpleado;
                        item.IdActaFiniquito = _Info.IdActaFiniquito;
                        item.Observacion = "Indemnización por NO recibir al trabajador en caso de enfermedad no Profesional según Art.175 y 179";
                        item.Valor =Math.Round( totalRubroAcumulado,2);
                        item.IdRubro = info_parametro.IdRubro_acta_finiquito;
                        lst_valores_x_indegnizacion.Add(item);

                    }

                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        //OBTNER PRESTAMOS
        private Boolean ObtenerCuotasPrestamosPendientes()
        {
            try
            {
                double prestamo = 0;
                 prestamo = bus_prestamo.get_valor_cuotas_pendientes(_Info.IdEmpresa, _Info.IdEmpleado);
                if (prestamo > 0)
                {
                    ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                    item.IdEmpresa = _Info.IdEmpresa;
                    item.IdEmpleado = _Info.IdEmpleado;
                    item.IdActaFiniquito = _Info.IdActaFiniquito;
                    item.Observacion = "Cuotas de prestamos pendientes de cobors ";
                    item.Valor = prestamo * -1;
                    item.IdRubro = "277";
                    lst_valores_x_indegnizacion.Add(item);
                }
                
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        //OBTNER NOVEDADES PENDIENTES DE DESCUENTO O PAGO
        private Boolean ObtenerNovedadesPendientes()
        {
            try
            {
                var novedades = bus_novedad.get_list_nov_liq_empleado(_Info.IdEmpresa, _Info.IdEmpleado);
                foreach (var item_nov in novedades)
                {
                    ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                    item.IdEmpresa = _Info.IdEmpresa;
                    item.IdEmpleado = _Info.IdEmpleado;
                    item.IdActaFiniquito = _Info.IdActaFiniquito;
                    item.Observacion = item_nov.Observacion;
                    if(item_nov.rub_tipo=="E")
                        item.Valor =Math.Round( item_nov.Valor*-1,2);
                    else
                        item.Valor = Math.Round(item_nov.Valor, 2);
                    item.IdRubro = item_nov.IdRubro;
                    lst_valores_x_indegnizacion.Add(item);
                }
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        private Boolean ObtenerProvisionVacaciones()
        {
            try
            {
                var vacaciones = bus_rubros_acumulados.get_valor_x_rubro_acumulado(_Info.IdEmpresa, _Info.IdEmpleado, info_rubros_calculados.IdRubro_prov_vac);
                
                    ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                    item.IdEmpresa = _Info.IdEmpresa;
                    item.IdEmpleado = _Info.IdEmpleado;
                    item.IdActaFiniquito = _Info.IdActaFiniquito;
                    item.Observacion = "Vacaciones no gozadas";
                    item.Valor =Math.Round( vacaciones+(sueldo_base/24), 2);
                    item.IdRubro = info_rubros_calculados.IdRubro_prov_vac;
                    lst_valores_x_indegnizacion.Add(item);
                
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        private Boolean ObtenerProvisionDecimoIII()
        {
            try
            {
                var vacaciones = bus_rubros_acumulados.get_valor_x_rubro_acumulado(_Info.IdEmpresa, _Info.IdEmpleado, info_rubros_calculados.IdRubro_prov_DIII);
                ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                item.IdEmpresa = _Info.IdEmpresa;
                item.IdEmpleado = _Info.IdEmpleado;
                item.IdActaFiniquito = _Info.IdActaFiniquito;
                item.Observacion = "Decimo tercera remuneracón";
                item.Valor =Math.Round( vacaciones,2);
                item.IdRubro = info_rubros_calculados.IdRubro_DIII;
                if (item.Valor != 0)
                    lst_valores_x_indegnizacion.Add(item);

                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        private Boolean ObtenerProvisionDecimoIV()
        {
            try
            {
                var vacaciones = bus_rubros_acumulados.get_valor_x_rubro_acumulado(_Info.IdEmpresa, _Info.IdEmpleado, info_rubros_calculados.IdRubro_prov_DIV);
                ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                item.IdEmpresa = _Info.IdEmpresa;
                item.IdEmpleado = _Info.IdEmpleado;
                item.IdActaFiniquito = _Info.IdActaFiniquito;
                item.Observacion = "Decima cuarta remuneracón";
                item.Valor =Math.Round( vacaciones,2);
                item.IdRubro = info_rubros_calculados.IdRubro_prov_DIV;
                if(item.Valor!=0)
                lst_valores_x_indegnizacion.Add(item);
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }

        private Boolean ObtenerProvisionFondoReserva()
        {
            try
            {
              
                ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                item.IdEmpresa = _Info.IdEmpresa;
                item.IdEmpleado = _Info.IdEmpleado;
                item.IdActaFiniquito = _Info.IdActaFiniquito;
                item.Observacion = "Provision de fondo de reserva";
                item.Valor = Math.Round( (sueldo_base  *0.0833),2);
                item.IdRubro = info_rubros_calculados.IdRubro_fondo_reserva;
                if (item.Valor != 0)
                    lst_valores_x_indegnizacion.Add(item);

                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        private Boolean Obtenersueldo_no_pagados()
        {
            try
            {
               
                DateTime fecha_inicio;
                fecha_inicio=new DateTime(_Info.FechaSalida.Year, _Info.FechaSalida.Month, 1);
                dias_trabajados =(int) (Convert.ToDateTime(_Info.FechaSalida) - fecha_inicio).TotalDays;
                dias_trabajados = dias_trabajados + 1;
                sueldo =(double) bus_contrato.get_sueldo_actual(_Info.IdEmpresa, _Info.IdEmpleado);
                sueldo_base =Math.Round( (sueldo / 30) * dias_trabajados,2);
                ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                item.IdEmpresa = _Info.IdEmpresa;
                item.IdEmpleado = _Info.IdEmpleado;
                item.IdActaFiniquito = _Info.IdActaFiniquito;
                item.Observacion = "Sueldo no pagado";
                item.Valor = sueldo_base;
                item.IdRubro = info_rubros_calculados.IdRubro_sueldo;
                lst_valores_x_indegnizacion.Add(item);
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }
        private Boolean ObtenerAportePersonal()
        {
            try
            {
                ro_Acta_Finiquito_Detalle_Info item = new ro_Acta_Finiquito_Detalle_Info();
                item.IdEmpresa = _Info.IdEmpresa;
                item.IdEmpleado = _Info.IdEmpleado;
                item.IdActaFiniquito = _Info.IdActaFiniquito;
                item.Observacion = "Aporte personal";
                item.Valor =Math.Round( (sueldo_base*info_parametro.Porcentaje_aporte_pers)*-1,2);
                item.IdRubro = info_rubros_calculados.IdRubro_iess_perso;
                lst_valores_x_indegnizacion.Add(item);
                return true;
            }
            catch (Exception)
            {
                throw;

            }
        }




    }
}

