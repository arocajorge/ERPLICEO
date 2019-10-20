using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System.Collections.Generic;
using System.Linq;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_015_iglesia_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        List<BAN_015_Info> Lista = new List<BAN_015_Info>();

        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }

        public BAN_015_iglesia_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_015_iglesia_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);
            DateTime fechaIni = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fechaFin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            bool mostrarSaldo0 = string.IsNullOrEmpty(p_mostrarSaldo0.Value.ToString()) ? false : Convert.ToBoolean(p_mostrarSaldo0.Value);

            BAN_015_Bus bus_rpt = new BAN_015_Bus();
            List<BAN_015_Info> lst_rpt = bus_rpt.GetList(usuario,"IGLESIA", fechaIni, fechaFin, mostrarSaldo0);
            
            Lista = lst_rpt.Where(q => q.Tipo == "BANCOS").ToList();
            lst_rpt = lst_rpt.Where(q => q.Tipo == "FLUJOS").ToList();
                        
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(2);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }

            this.DataSource = lst_rpt;

        }

        private void subreport_cuentas_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.DataSource = Lista;

        }
    }
}
