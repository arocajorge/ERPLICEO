using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.Contabilidad;
using Core.Erp.Bus.Reportes.Contabilidad;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Contabilidad
{
    public partial class CONTA_006_ER_Rpt : DevExpress.XtraReports.UI.XtraReport
    {

        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }

        List<CONTA_006_Info> lst_rpt = new List<CONTA_006_Info>();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        public CONTA_006_ER_Rpt()
        {
            InitializeComponent();
        }

        private void CONTA_006_ER_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            bool mostrarSaldo0 = p_mostrarSaldo0.Value == null ? false : Convert.ToBoolean(p_mostrarSaldo0.Value);
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            bool mostrarAcumulado = string.IsNullOrEmpty(p_mostrarAcumulado.Value.ToString()) ? false : Convert.ToBoolean(p_mostrarAcumulado.Value);
            string balance = string.IsNullOrEmpty(p_balance.Value.ToString()) ? "" : Convert.ToString(p_balance.Value);
            CONTA_006_Bus bus_rpt = new CONTA_006_Bus();
            string Sucursal = "";

            tb_FiltroReportes_Bus bus_filtro = new tb_FiltroReportes_Bus();
            Sucursal = bus_filtro.GuardarDB(IdEmpresa, IntArray, IdUsuario);

            lst_rpt.AddRange(bus_rpt.GetList(IdEmpresa, IdAnio, mostrarSaldo0, IdUsuario, IdNivel, mostrarAcumulado, balance));
            lst_rpt.ForEach(q => q.Su_Descripcion = Sucursal);
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
