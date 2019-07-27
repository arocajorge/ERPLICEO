using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Importacion;
using Core.Erp.Info.Reportes.Importacion;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Importacion
{
    public partial class IMP_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public IMP_003_Rpt()
        {
            InitializeComponent();
        }

        private void IMP_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            string IdPais_embarque = p_IdPais_embarque.Value == null ? "" : Convert.ToString(p_IdPais_embarque.Value);
            decimal IdProveedor = string.IsNullOrEmpty(p_IdProveedor.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdProveedor.Value);
            decimal IdProducto = string.IsNullOrEmpty(p_IdProducto.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdProducto.Value);
            int IdMarca = p_IdMarca.Value == null ? 0 : Convert.ToInt32(p_IdMarca.Value);
            DateTime fecha_ini = string.IsNullOrEmpty(p_fecha_ini.Value.ToString())? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString())? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);

            IMP_003_Bus bus_rpt = new IMP_003_Bus();
            List<IMP_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdPais_embarque, IdProveedor, IdProducto, IdMarca,  fecha_ini, fecha_fin);
            this.DataSource = lst_rpt;

        }
    }
}
