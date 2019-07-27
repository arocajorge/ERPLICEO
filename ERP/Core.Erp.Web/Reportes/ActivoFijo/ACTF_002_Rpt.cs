using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.ActivoFijo
{
    public partial class ACTF_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {

        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACTF_002_Rpt()
        {
            InitializeComponent();
        }



        private void ACTF_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdVtaActivo = p_IdVtaActivo.Value == null ? 0 : Convert.ToDecimal(p_IdVtaActivo.Value);

            ACTF_002_Bus bus_rpt = new ACTF_002_Bus();
            List<ACTF_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdVtaActivo);
            this.DataSource = lst_rpt;
        }
    }
}
