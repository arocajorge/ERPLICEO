using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Bus.Reportes.RRHH;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_007_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_007_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_007_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {            
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdEmpleado = p_IdEmpleado.Value == null ? 0 : Convert.ToDecimal(p_IdEmpleado.Value);
            int IdSolicitud = p_IdSolicitud.Value == null ? 0 : Convert.ToInt32(p_IdSolicitud.Value);

            ROL_007_Bus bus_rpt = new ROL_007_Bus();
            List<ROL_007_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdEmpleado, IdSolicitud);
            if (lst_rpt.Count > 0)
            {
                ROL_007_Info info = lst_rpt[0];
                txtSolicitud.Text = "Yo, " + info.pe_apellido + " con cedula identidad # " + info.pe_cedulaRuc + " solicitud se me conceda a disfrutar o se me cancelen las vacaciones correspondiente al periodo de: " + info.Anio_Desde.ToString().Substring(0, 10) + " al " + info.Anio_Hasta.ToString().Substring(0, 10) +
                  " en las fechas de : " + info.Fecha_Desde.ToString().Substring(0, 10) + " al " + info.Fecha_Hasta.ToString().Substring(0, 10);
            }
            this.DataSource = lst_rpt;
        }
    }
}
