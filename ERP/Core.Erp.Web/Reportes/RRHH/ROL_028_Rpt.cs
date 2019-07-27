using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.General;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Bus.Reportes.RRHH;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_028_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_028_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_028_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_usuario.Text = usuario;

                int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int Id_Rdep= p_Id_Rdep.Value == null ? 0 : Convert.ToInt32(p_Id_Rdep.Value);

                ROL_028_Bus bus_rpt = new ROL_028_Bus();
                List<ROL_028_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, Id_Rdep);

                this.DataSource = lst_rpt;

                tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
                var emp = bus_empresa.get_info(IdEmpresa);
                lbl_empresa.Text = emp.RazonSocial;
                if (emp != null && emp.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
