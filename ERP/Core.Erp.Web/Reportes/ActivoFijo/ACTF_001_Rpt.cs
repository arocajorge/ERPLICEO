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
    public partial class ACTF_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACTF_001_Rpt()
        {
            InitializeComponent();
        }

        private void ACTF_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal Id_Mejora_Baja_Activo = p_Id_Mejora_Baja_Activo.Value == null ? 0 : Convert.ToDecimal(p_Id_Mejora_Baja_Activo.Value);
            string Id_Tipo = p_Id_Tipo.Value == null ? "" : Convert.ToString(p_Id_Tipo.Value);

            ACTF_001_Bus bus_rpt = new ACTF_001_Bus();
            List<ACTF_001_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, Id_Mejora_Baja_Activo, Id_Tipo);
            this.DataSource = lst_rpt;
        }
    }
}
