using Core.Erp.Data.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_anio_fiscal_x_tb_sucursal_Data
    {
        ct_cbtecble_Data data_cbtecble = new ct_cbtecble_Data();
        ct_cbtecble_det_Data data_cbtecble_det = new ct_cbtecble_det_Data();
        public List<ct_anio_fiscal_x_tb_sucursal_Info> get_list(int IdEmpresa, int IdSucursal)
        {
            try
            {
                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                List<ct_anio_fiscal_x_tb_sucursal_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    Lista = Context.vwct_anio_fiscal_x_tb_sucursal.Where(
                            q => q.IdEmpresa == IdEmpresa && q.IdSucursal >= IdSucursalInicio && q.IdSucursal <= IdSucursalFin).Select(q => new ct_anio_fiscal_x_tb_sucursal_Info
                            {
                                IdEmpresa = q.IdEmpresa,
                                IdSucursal = q.IdSucursal,
                                Su_Descripcion = q.Su_Descripcion,
                                IdanioFiscal = q.IdanioFiscal,
                                IdTipoCbte = q.IdTipoCbte,
                                IdCbteCble = q.IdCbteCble,
                                Observacion = q.cb_Observacion
                            }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ct_anio_fiscal_x_tb_sucursal_Info get_info(int IdEmpresa, int IdSucursal, int IdanioFiscal)
        {
            try
            {
                ct_anio_fiscal_x_tb_sucursal_Info info = new ct_anio_fiscal_x_tb_sucursal_Info();
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    info = (from q in Context.vwct_anio_fiscal_x_tb_sucursal
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdanioFiscal == IdanioFiscal
                             select new ct_anio_fiscal_x_tb_sucursal_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 Su_Descripcion = q.Su_Descripcion,
                                 IdanioFiscal = q.IdanioFiscal,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 Observacion = q.cb_Observacion

                             }).FirstOrDefault();

                    //ct_anio_fiscal_x_tb_sucursal Entity = Context.vwct_anio_fiscal_x_tb_sucursal.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdanioFiscal == IdanioFiscal);
                    //if (Entity == null) return null;
                    //info = new ct_anio_fiscal_x_tb_sucursal_Info
                    //{
                    //    IdanioFiscal = Entity.IdanioFiscal,
                    //    IdEmpresa = Entity.IdEmpresa,
                    //    IdSucursal = Entity.IdSucursal,
                    //    IdTipoCbte = Entity.IdTipoCbte,
                    //    IdCbteCble = Entity.IdCbteCble
                    //};
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(ct_anio_fiscal_x_tb_sucursal_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {                    
                    var Fecha = info.IdanioFiscal + "-12-" + "31";
                    ct_cbtecble_Info info_diario = armar_info(info.info_cbtecble_det, info.IdEmpresa, info.IdSucursal, info.IdTipoCbte, 0, info.Observacion, Convert.ToDateTime(Fecha));
                    if (info_diario != null)
                    {
                        info_diario.IdUsuario = info.IdUsuario;
                        if (data_cbtecble.guardarDB(info_diario))
                        {
                            info.IdTipoCbte = info_diario.IdTipoCbte;
                            info.IdCbteCble = info_diario.IdCbteCble;
                        }
                    }

                    ct_anio_fiscal_x_tb_sucursal Entity = new ct_anio_fiscal_x_tb_sucursal()
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdanioFiscal = info.IdanioFiscal,
                        IdTipoCbte = info.IdTipoCbte,
                        IdCbteCble = info.IdCbteCble
                    };

                    Context.ct_anio_fiscal_x_tb_sucursal.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                eliminar(info.IdEmpresa, Convert.ToInt32(info.IdTipoCbte), Convert.ToDecimal(info.IdCbteCble));

                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_conciliacionAnticipo_Data", Metodo = "GuardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(ct_anio_fiscal_x_tb_sucursal_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var Fecha = info.IdanioFiscal + "-12-" + "31";
                    ct_anio_fiscal_x_tb_sucursal Entity = Context.ct_anio_fiscal_x_tb_sucursal.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdanioFiscal == info.IdanioFiscal);
                    if (Entity == null)
                        return false;

                    ct_cbtecble_Info info_diario = armar_info(info.info_cbtecble_det, info.IdEmpresa, info.IdSucursal, Convert.ToInt32(info.IdTipoCbte), 0, info.Observacion, Convert.ToDateTime(Fecha));
                    //ct_cbtecble Entity_cbte = Context.ct_cbtecble.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipoCbte && q.IdCbteCble == info.IdCbteCble).FirstOrDefault();

                    if (info_diario != null)
                    {
                        info_diario.IdCbteCble = Convert.ToDecimal(info.IdCbteCble);
                        info_diario.IdUsuarioUltModi = info.IdUsuario;
                        info_diario.cb_Observacion = info.Observacion;
                        if (data_cbtecble.modificarDB(info_diario))
                        {
                            info.IdTipoCbte = info_diario.IdTipoCbte;
                            info.IdCbteCble = info_diario.IdCbteCble;
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(ct_anio_fiscal_x_tb_sucursal_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var info_anio_x_sucursal = Context.ct_anio_fiscal_x_tb_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdanioFiscal == info.IdanioFiscal).FirstOrDefault();
                    if (info_anio_x_sucursal == null)
                        return false;

                    Context.ct_anio_fiscal_x_tb_sucursal.Remove(info_anio_x_sucursal);

                    if (data_cbtecble.anularDB(new ct_cbtecble_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = Convert.ToInt32(info.IdTipoCbte),
                        IdCbteCble = Convert.ToInt32(info.IdCbteCble),
                        IdUsuarioAnu = info.IdUsuario,
                        cb_MotivoAnu = info.MotivoAnulacion
                    }))

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ct_cbtecble_Info armar_info(List<ct_cbtecble_det_Info> lista, int IdEmpresa, int IdSucursal, int IdTipoCbte, decimal IdCbteCble, string Observacion, DateTime Fecha)
        {
            try
            {
                ct_cbtecble_Info info = new ct_cbtecble_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdTipoCbte = IdTipoCbte,
                    IdCbteCble = IdCbteCble,
                    IdSucursal = IdSucursal,
                    cb_Observacion = Observacion,
                    cb_Fecha = Fecha,
                    cb_Valor = lista.Where(q => q.dc_Valor > 0).Sum(q => q.dc_Valor)
                };

                info.lst_ct_cbtecble_det = lista;
                info.lst_ct_cbtecble_det.ForEach(q => { q.IdEmpresa = IdEmpresa; q.IdTipoCbte = IdTipoCbte; q.IdCbteCble = IdCbteCble; });

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminar(int IdEmpresa, int IdTipoCbte, decimal IdCbtecble)
        {
            try
            {
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    var lst_cbte_det = db.ct_cbtecble_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte == IdTipoCbte && q.IdCbteCble == IdCbtecble).ToList();
                    db.ct_cbtecble_det.RemoveRange(lst_cbte_det);

                    var cbte = db.ct_cbtecble.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte == IdTipoCbte && q.IdCbteCble == IdCbtecble).FirstOrDefault();
                    db.ct_cbtecble.Remove(cbte);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
