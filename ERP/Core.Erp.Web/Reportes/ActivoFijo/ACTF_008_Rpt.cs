using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.ActivoFijo
{
    public partial class ACTF_008_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACTF_008_Rpt()
        {
            InitializeComponent();
        }

        private void ACTF_008_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdArea = string.IsNullOrEmpty(p_IdArea.Value.ToString()) ? 0 : Convert.ToInt32(p_IdArea.Value);
            decimal IdDepartamento = string.IsNullOrEmpty(p_IdDepartamento.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdDepartamento .Value);
            decimal IdEmpleadoCustodio = string.IsNullOrEmpty(p_IdEmpleadoCustodio.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdEmpleadoCustodio.Value);
            decimal IdEmpleadoEncargado = string.IsNullOrEmpty(p_IdEmpleadoEncargado.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdEmpleadoEncargado.Value);
            int IdTipo = string.IsNullOrEmpty(p_IdTipo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdTipo.Value);
            int IdCategoria = string.IsNullOrEmpty(p_IdCategoria.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCategoria.Value);
            double MontoMin = string.IsNullOrEmpty(p_MontoMin.Value.ToString()) ? 0 : Convert.ToDouble(p_MontoMin.Value);
            double MontoMax = string.IsNullOrEmpty(p_MontoMax.Value.ToString()) ? 0 : Convert.ToDouble(p_MontoMax.Value);
            string IdTipoCatalogo_Ubicacion = string.IsNullOrEmpty(p_IdTipoCatalogo_Ubicacion.Value.ToString()) ? "" : Convert.ToString(p_IdTipoCatalogo_Ubicacion.Value);
            ACTF_008_Bus bus_rpt = new ACTF_008_Bus();
            List<ACTF_008_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdArea, IdDepartamento, IdEmpleadoCustodio, IdEmpleadoEncargado,IdTipo,IdCategoria,MontoMin,MontoMax, IdTipoCatalogo_Ubicacion);
            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }

        private void GroupCustodio_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Convert.ToBoolean(p_AgruparPorCustodio.Value))
            {
                e.Cancel = true;
            }
        }
    }
}
