using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using Core.Erp.Bus.General;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_013_Rpt : DevExpress.XtraReports.UI.XtraReport
    {

        public string usuario { get; set; }
        public string empresa { get; set; }
        public BAN_013_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_013_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdBanco = string.IsNullOrEmpty(p_IdBanco.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBanco.Value);
            decimal IdPersona = string.IsNullOrEmpty(p_IdPersona.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdPersona.Value);
            DateTime fechaIni = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fechaFin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);

            BAN_013_Bus bus_rpt = new BAN_013_Bus();
            List<BAN_013_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdBanco, IdPersona, fechaIni, fechaFin);
            
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }

            this.DataSource = lst_rpt;
        }
    }
}
