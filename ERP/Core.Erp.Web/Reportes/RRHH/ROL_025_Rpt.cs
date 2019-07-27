using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_025_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_025_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_025_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_usuario.Text = usuario;

                int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
                int IdNomina_Tipo = p_IdNomina_Tipo.Value == null ? 0 : Convert.ToInt32(p_IdNomina_Tipo.Value);
                int IdPeriodo = p_IdPeriodo.Value == null ? 0 : Convert.ToInt32(p_IdPeriodo.Value);
                int IdDivision = p_IdDivision.Value == null ? 0 : Convert.ToInt32(p_IdDivision.Value);
                int IdArea = p_IdArea.Value == null ? 0 : Convert.ToInt32(p_IdArea.Value);

                ROL_025_Bus bus_rpt = new ROL_025_Bus();
                List<ROL_025_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdNomina_Tipo, IdPeriodo, IdDivision, IdArea);

                this.DataSource = lst_rpt;

                tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
                var emp = bus_empresa.get_info(IdEmpresa);
                lbl_empresa.Text = emp.RazonSocial;

                tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
                if (IdSucursal>0)
                {
                    var info_sucursal = bus_sucursal.get_info(IdEmpresa, IdSucursal);
                    lbl_sucursal.Text = info_sucursal.Su_Descripcion;
                }
                else
                {
                    lbl_sucursal.Text = "";
                }


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
