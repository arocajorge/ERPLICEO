using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.ActivoFijo
{
    public partial class ACTF_007_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string empresa { get; set; }
        public ACTF_007_Rpt()
        {
            InitializeComponent();
        }

        private void ACTF_007_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_empresa.Text = empresa;
            lbl_fecha.Text = DateTime.Now.ToString("dd 'días de mes de' MMMM 'del' yyyy");
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdActivoFijo = p_IdActivoFijo.Value == null ? 0 : Convert.ToInt32(p_IdActivoFijo.Value);

            ACTF_007_Bus bus_rpt = new ACTF_007_Bus();
            List<ACTF_007_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdActivoFijo);
            this.DataSource = lst_rpt;


            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = emp.em_nombre;
            lbl_emp1.Text = emp.em_nombre;
            lbl_emp2.Text = emp.em_nombre;
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }
    }
}
