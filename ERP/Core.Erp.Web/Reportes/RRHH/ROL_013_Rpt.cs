using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Bus.General;
using System.Linq;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_013_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_013_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_013_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            //lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdNomina = string.IsNullOrEmpty(p_IdNomina.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNomina.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdDivision = string.IsNullOrEmpty(p_IdDivision.Value.ToString()) ? 0 : Convert.ToInt32(p_IdDivision.Value);
            int IdArea = string.IsNullOrEmpty(p_IdArea.Value.ToString()) ? 0 : Convert.ToInt32(p_IdArea.Value);
            DateTime FechaIni = string.IsNullOrEmpty(p_FechaIni.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_FechaIni.Value);
            DateTime FechaFin = string.IsNullOrEmpty(p_FechaFin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_FechaFin.Value);
            decimal IdEmpleado = string.IsNullOrEmpty(p_IdEmpleado.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdEmpleado.Value);

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = emp.RazonSocial;
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }

            ROL_013_Bus bus_rpt = new ROL_013_Bus();
            List<ROL_013_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdNomina, IdSucursal, FechaIni, FechaFin, IdEmpleado, IdDivision, IdArea);

            List<ROL_013_Info> Lista = (from q in lst_rpt
                                        group q by new
                                        {
                                            q.IdEmpresa,
                                            q.IdEmpleado
                                        } into empleados
                                        select new ROL_013_Info
                                        {
                                            IdEmpresa = empleados.Key.IdEmpresa,
                                            IdEmpleado = empleados.Key.IdEmpleado,
                                            ValorNeto = Math.Round(empleados.Sum(q=> q.Sueldo)/12,2,MidpointRounding.AwayFromZero) - Math.Round(Convert.ToDecimal(empleados.Max(q=>q.Prestamo)),2,MidpointRounding.AwayFromZero),
                                            Prestamo = empleados.Max(q => q.Prestamo)
                                        }).ToList();
            
            

            var lst = (from a in lst_rpt
                       join b in Lista
                       on a.IdEmpleado equals b.IdEmpleado
                       select new ROL_013_Info
                       {
                           IdEmpresa = a.IdEmpresa,
                           IdRol = a.IdRol,
                           IdEmpleado = a.IdEmpleado,
                           IdArea = a.IdArea,
                           IdDivision = a.IdDivision,
                           IdRubro = a.IdRubro,
                           em_codigo = a.em_codigo,
                           Provision = a.Provision,
                           Estado = a.Estado,
                           IdSucursal = a.IdSucursal,
                           de_descripcion = a.de_descripcion,
                           Su_Descripcion = a.Su_Descripcion,
                           pe_nombreCompleto = a.pe_nombreCompleto,
                           Division = a.Division,
                           Area = a.Area,
                           Mes = a.Mes,
                           Prestamo = a.Prestamo,
                           Sueldo = a.Sueldo,
                           ValorNeto = b.ValorNeto
                       });

            num_empleados.Text = Convert.ToString(Lista.Count);
            txtTotalPrestamo.Text = Lista.Sum(q => q.Prestamo).ToString("n2");
            ValorNeto.Text = Lista.Sum(q=>q.ValorNeto).ToString("n2");
            this.DataSource = lst;
        }
    }
}
