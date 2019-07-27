using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_017_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }
        public FAC_017_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_017_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdMarca = string.IsNullOrEmpty(p_IdMarca.Value.ToString()) ? 0 : Convert.ToInt32(p_IdMarca.Value);
            DateTime fecha_ini = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);

            FAC_017_Bus bus_rpt = new FAC_017_Bus();
            List<FAC_017_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdMarca, fecha_ini, fecha_fin);
            if (IntArray != null)
            {
                foreach (var item in IntArray)
                {
                    lst_rpt.AddRange(bus_rpt.GetList(IdEmpresa, item, fecha_ini, fecha_fin));
                }
            }
            this.DataSource = lst_rpt.OrderBy(q=>q.ANIO).ThenBy(q=>q.MES).ToList();
        }

        private void xrPivotGrid1_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                if (e.IsColumn)
                    e.DisplayText = "Total";
                else
                    e.DisplayText = "Total";
            }
        }
    }
}
