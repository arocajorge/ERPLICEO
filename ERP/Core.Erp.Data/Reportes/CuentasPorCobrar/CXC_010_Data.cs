using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
    public class CXC_010_Data
    {
        public List<CXC_010_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCliente, int Idtipo_cliente, DateTime fechaCorte, bool MostrarSoloCarteraVencida)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 999999 : IdSucursal;

                decimal IdClienteIni = IdCliente;
                decimal IdClienteFin = IdCliente == 0 ? 9999999999 : IdCliente;

                int Idtipo_clienteIni = Idtipo_cliente;
                int Idtipo_clienteFin = Idtipo_cliente == 0 ? 999999 : Idtipo_cliente;

                fechaCorte = fechaCorte.Date;

                List<CXC_010_Info> Lista = new List<CXC_010_Info>();
                using (Entities_reportes Context = new Entities_reportes())
                {
                    var lst = Context.SPCXC_010(IdEmpresa, IdSucursalIni, IdSucursalFin, IdClienteIni, IdClienteFin, Idtipo_clienteIni, Idtipo_clienteFin, fechaCorte, MostrarSoloCarteraVencida).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new CXC_010_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdBodega = q.IdBodega,
                            IdCliente = q.IdCliente,
                            Codigo = q.Codigo,
                            IdCbteVta = q.IdCbteVta,
                            CodCbteVta = q.CodCbteVta,
                            vt_fecha = q.vt_fecha,
                            vt_fech_venc = q.vt_fech_venc,
                            vt_NumFactura = q.vt_NumFactura,
                            vt_Observacion = q.vt_Observacion,
                            vt_plazo = q.vt_plazo,
                            vt_serie1 = q.vt_serie1,
                            vt_serie2 = q.vt_serie2,
                            vt_tipoDoc = q.vt_tipoDoc,
                            Su_Descripcion = q.Su_Descripcion,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_telefonoOfic = q.pe_TelefonoOfic,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Valor_Original = q.Valor_Original ?? 0,
                            Valor_vencido = q.Valor_vencido ?? 0,
                            Valor_x_Vencer = q.Valor_x_Vencer ?? 0,
                            Vencer_30_Dias = q.Vencer_30_Dias ?? 0,
                            Vencer_60_Dias = q.Vencer_60_Dias ?? 0,
                            Vencer_90_Dias = q.Vencer_90_Dias ?? 0,
                            Dias_Vencidos = q.Dias_Vencidos,
                            Idtipo_cliente = q.Idtipo_cliente,
                            Mayor_a_90Dias = q.Mayor_a_90Dias ?? 0,
                            Saldo = Convert.ToDecimal(q.Saldo),
                            Total_Pagado = q.Total_Pagado,
                            TelefonoContacto = q.TelefonoContacto,
                            NomContacto = q.NomContacto,
                            Descripcion_tip_cliente = q.Descripcion_tip_cliente
                        });
                    }
                    
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
