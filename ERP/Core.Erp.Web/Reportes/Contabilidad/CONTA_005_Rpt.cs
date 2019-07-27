using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Reportes.Contabilidad;
using Core.Erp.Info.Reportes.Contabilidad;
using System.Collections.Generic;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Reportes.Contabilidad
{
    public partial class CONTA_005_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }
        public CONTA_005_Rpt()
        {
            InitializeComponent();
        }

        private void CONTA_005_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdPunto_cargo_grupo = string.IsNullOrEmpty(p_IdPunto_cargo_grupo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdPunto_cargo_grupo.Value);
            DateTime fechaIni = string.IsNullOrEmpty(p_fechaIni.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fechaIni.Value);
            DateTime fechaFin = string.IsNullOrEmpty(p_fechaFin.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fechaFin.Value);
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);
            bool mostrarSaldo0 = p_mostrarSaldo0.Value == null ? false : Convert.ToBoolean(p_mostrarSaldo0.Value);

            CONTA_005_Bus bus_rpt = new CONTA_005_Bus();
            List<CONTA_005_Info> lst_rpt = bus_rpt.GetList(IdEmpresa,IdPunto_cargo_grupo, IdUsuario, fechaIni, fechaFin, mostrarSaldo0);

            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null)
            {
                if (emp.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
                }
            }
            
       }
    }
}
