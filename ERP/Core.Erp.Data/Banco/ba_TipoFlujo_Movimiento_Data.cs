using Core.Erp.Data.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_TipoFlujo_Movimiento_Data
    {
        public List<ba_TipoFlujo_Movimiento_Info> get_list(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<ba_TipoFlujo_Movimiento_Info> Lista;

                using (Entities_banco db = new Entities_banco())
                {
                    if (MostrarAnulados == false)
                    {
                        Lista = db.vwba_TipoFlujo_Movimiento.Where(q => q.Estado == true && q.IdEmpresa == IdEmpresa).Select(q => new ba_TipoFlujo_Movimiento_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMovimiento = q.IdMovimiento,
                            IdTipoFlujo = q.IdTipoFlujo,
                            IdSucursal = q.IdSucursal,
                            IdBanco = q.IdBanco,
                            Valor = q.Valor,
                            Fecha = q.Fecha,
                            Descricion = q.Descricion,
                            Su_Descripcion = q.Su_Descripcion,
                            ba_descripcion = q.ba_descripcion,
                            Estado = q.Estado
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.vwba_TipoFlujo_Movimiento.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new ba_TipoFlujo_Movimiento_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMovimiento = q.IdMovimiento,
                            IdTipoFlujo = q.IdTipoFlujo,
                            IdSucursal = q.IdSucursal,
                            IdBanco = q.IdBanco,
                            Valor = q.Valor,
                            Fecha = q.Fecha,
                            Descricion = q.Descricion,
                            Su_Descripcion = q.Su_Descripcion,
                            ba_descripcion = q.ba_descripcion,
                            Estado = q.Estado
                        }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int IdEmpresa)
        {

            try
            {
                decimal ID = 1;
                using (Entities_banco db = new Entities_banco())
                {
                    var Lista = db.ba_TipoFlujo_Movimiento.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdMovimiento);

                    if (Lista.Count() > 0)
                        ID = Lista.Max() + 1;
                }
                return Convert.ToInt32(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_TipoFlujo_Movimiento_Info get_info(int IdEmpresa, decimal IdMovimiento)
        {
            try
            {
                ba_TipoFlujo_Movimiento_Info info = new ba_TipoFlujo_Movimiento_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_TipoFlujo_Movimiento Entity = Context.ba_TipoFlujo_Movimiento.Where(q => q.IdMovimiento == IdMovimiento && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new ba_TipoFlujo_Movimiento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMovimiento = Entity.IdMovimiento,
                        IdTipoFlujo = Entity.IdTipoFlujo,
                        IdSucursal = Entity.IdSucursal,
                        IdBanco = Entity.IdBanco,
                        Valor = Entity.Valor,
                        Fecha = Entity.Fecha,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(ba_TipoFlujo_Movimiento_Info info)
        {
            try
            {
                using (Entities_banco db = new Entities_banco())
                {
                    db.ba_TipoFlujo_Movimiento.Add(new ba_TipoFlujo_Movimiento
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMovimiento = info.IdMovimiento = get_id(info.IdEmpresa),
                        IdTipoFlujo = info.IdTipoFlujo,
                        IdSucursal = info.IdSucursal,
                        IdBanco = info.IdBanco,
                        Valor = info.Valor,
                        Fecha = info.Fecha,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_TipoFlujo_Movimiento_Data", Metodo = "GuardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool ModificarBD(ba_TipoFlujo_Movimiento_Info info)
        {
            try
            {
                using (Entities_banco db = new Entities_banco())
                {
                    ba_TipoFlujo_Movimiento entity = db.ba_TipoFlujo_Movimiento.Where(q => q.IdMovimiento == info.IdMovimiento && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.IdTipoFlujo = info.IdTipoFlujo;
                    entity.IdSucursal = info.IdSucursal;
                    entity.IdBanco = info.IdBanco;
                    entity.Valor = info.Valor;
                    entity.Fecha = info.Fecha;
                    entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    entity.FechaModificacion = DateTime.Now;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_TipoFlujo_Movimiento_Data", Metodo = "ModificarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool AnularBD(ba_TipoFlujo_Movimiento_Info info)
        {
            try
            {
                using (Entities_banco db = new Entities_banco())
                {
                    ba_TipoFlujo_Movimiento entity = db.ba_TipoFlujo_Movimiento.Where(q => q.IdMovimiento == info.IdMovimiento && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Estado = false;
                    entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    entity.FechaAnulacion = DateTime.Now;
                    entity.MotivoAnulacion = info.MotivoAnulacion;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
