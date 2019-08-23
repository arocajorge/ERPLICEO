using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_014_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        BAN_014_Bus bus_rpt = new BAN_014_Bus();
        public string usuario { get; set; }
        public string empresa { get; set; }
        public BAN_014_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_014_Rptcs_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_empresa.Text = empresa;
                lbl_usuario.Text = usuario;
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdBanco = string.IsNullOrEmpty(p_IdBanco.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBanco.Value);
                decimal IdPersona = string.IsNullOrEmpty(p_IdPersona.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdPersona.Value);
                DateTime FechaIni = string.IsNullOrEmpty(p_FechaIni.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaIni.Value);
                DateTime FechaFin = string.IsNullOrEmpty(p_FechaFin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaFin.Value);
                List<BAN_014_Info> Lista = bus_rpt.GetList(IdEmpresa,IdBanco,IdPersona,FechaIni,FechaFin);

                tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
                var emp = bus_empresa.get_info(IdEmpresa);
                if (emp != null && emp.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
                }

                this.DataSource = Lista;
            }
            catch (Exception)
            {
                
            }
        }
    }
}
