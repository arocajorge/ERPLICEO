using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_007_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }

        public CXP_007_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_007_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("d/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            bool mostrar_agrupado = p_mostrar_agrupado == null ? false : Convert.ToBoolean(p_mostrar_agrupado.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);

            CXP_007_Bus bus_rpt = new CXP_007_Bus();
            List<CXP_007_Info> lst_rpt = new List<CXP_007_Info>();

            if (IntArray != null)
            {
                foreach (var item in IntArray)
                {
                    lst_rpt.AddRange(bus_rpt.get_list(IdEmpresa, fecha_ini, fecha_fin, mostrar_agrupado, item));
                }
            }
            this.DataSource = lst_rpt;
        }

        private void GroupFooter_codigo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Convert.ToBoolean(p_mostrar_agrupado.Value))
            {
                e.Cancel = true;
            }
        }

        private void GroupHeader_codigo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Convert.ToBoolean(p_mostrar_agrupado.Value))
            {
                e.Cancel = true;
            }
        }

        private void GroupHeader_TipoCbte_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToBoolean(p_mostrar_agrupado.Value))
            {
                e.Cancel = true;
            }
        }

        private void GroupFooter_TipoCbte_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToBoolean(p_mostrar_agrupado.Value))
            {
                e.Cancel = true;
            }
        }
    }
}
