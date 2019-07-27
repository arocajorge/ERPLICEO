using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.ActivoFijo;
using Core.Erp.Bus.Reportes.ActivoFijo;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.ActivoFijo
{
    public partial class ACTF_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACTF_003_Rpt()
        {
            InitializeComponent();
        }



        private void ACTF_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("d/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;


            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdRetiroActivo = p_IdRetiroActivo.Value == null ? 0 : Convert.ToDecimal(p_IdRetiroActivo.Value);

            ACTF_003_Bus bus_rpt = new ACTF_003_Bus();
            List<ACTF_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdRetiroActivo);
            this.DataSource = lst_rpt;
        }
    }
}
