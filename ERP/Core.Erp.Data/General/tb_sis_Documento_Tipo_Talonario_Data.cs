using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_sis_Documento_Tipo_Talonario_Data
    {
        public List<tb_sis_Documento_Tipo_Talonario_Info> get_list(int IdEmpresa, int IdSucursal,string CodDocumentoTipo,  bool mostrar_anulados)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;
                
                List<tb_sis_Documento_Tipo_Talonario_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                            Lista = Context.tb_sis_Documento_Tipo_Talonario.Where(q=> q.IdEmpresa == IdEmpresa
                                      && IdSucursal_ini <= q.IdSucursal
                                      && q.IdSucursal <= IdSucursal_fin
                                      && q.CodDocumentoTipo == (string.IsNullOrEmpty(CodDocumentoTipo) ? q.CodDocumentoTipo : CodDocumentoTipo) 
                                      && q.Estado ==(mostrar_anulados ? q.Estado : "A")).Select(q=> new tb_sis_Documento_Tipo_Talonario_Info
                                     {
                                         CodDocumentoTipo = q.CodDocumentoTipo,
                                         IdSucursal = q.IdSucursal,
                                         IdEmpresa = q.IdEmpresa,
                                         Establecimiento = q.Establecimiento,
                                         Estado = q.Estado,
                                         NumAutorizacion = q.NumAutorizacion,
                                         NumDocumento = q.NumDocumento,
                                         PuntoEmision = q.PuntoEmision,
                                         EstadoBool = q.Estado == "A" ? true : false

                                     }).ToList();                    
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_sis_Documento_Tipo_Talonario_Info get_info(int IdEmpresa, string CodDocumentoTipo, string Establecimiento, string PuntoEmision, string NumDocumento)
        {
            try
            {
                tb_sis_Documento_Tipo_Talonario_Info info = new tb_sis_Documento_Tipo_Talonario_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo_Talonario Entity = Context.tb_sis_Documento_Tipo_Talonario.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.CodDocumentoTipo == CodDocumentoTipo && q.Establecimiento == Establecimiento && q.PuntoEmision == PuntoEmision && q.NumDocumento == NumDocumento);
                    if (Entity == null) return null;
                    info = new tb_sis_Documento_Tipo_Talonario_Info
                    {
                        CodDocumentoTipo = Entity.CodDocumentoTipo,
                        IdSucursal = Entity.IdSucursal,
                        IdEmpresa = Entity.IdEmpresa,
                        Establecimiento = Entity.Establecimiento,
                        Estado = Entity.Estado,
                        es_Documento_Electronico = Entity.es_Documento_Electronico==null ? false:Convert.ToBoolean(Entity.es_Documento_Electronico),
                        FechaCaducidad = Entity.FechaCaducidad,
                        NumAutorizacion = Entity.NumAutorizacion,
                        NumDocumento = Entity.NumDocumento,
                        PuntoEmision = Entity.PuntoEmision,
                        Usado = Entity.Usado == null ? false : Convert.ToBoolean(Entity.Usado)

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_sis_Documento_Tipo_Talonario_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo_Talonario Entity = new tb_sis_Documento_Tipo_Talonario
                    {
                        CodDocumentoTipo = info.CodDocumentoTipo,
                        IdSucursal = info.IdSucursal,
                        IdEmpresa = info.IdEmpresa,
                        Establecimiento = info.Establecimiento,
                        Estado = info.Estado="A",
                        es_Documento_Electronico = info.es_Documento_Electronico,
                        FechaCaducidad = info.FechaCaducidad,
                        NumAutorizacion = info.NumAutorizacion,
                        NumDocumento = info.NumDocumento,
                        PuntoEmision = info.PuntoEmision,
                        Usado = info.Usado
                        
                    };
                    Context.tb_sis_Documento_Tipo_Talonario.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception )
            {

                throw;
            }
        }
        public bool modificarDB(tb_sis_Documento_Tipo_Talonario_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo_Talonario Entity = Context.tb_sis_Documento_Tipo_Talonario.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.CodDocumentoTipo == info.CodDocumentoTipo && q.Establecimiento == info.Establecimiento && q.PuntoEmision == info.PuntoEmision && q.NumDocumento == info.NumDocumento);
                    if (Entity == null) return false;

                    
                    Entity.es_Documento_Electronico = info.es_Documento_Electronico;
                    Entity.FechaCaducidad = info.FechaCaducidad;
                    Entity.NumAutorizacion = info.NumAutorizacion;
                    Entity.Usado = info.Usado;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(tb_sis_Documento_Tipo_Talonario_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo_Talonario Entity = Context.tb_sis_Documento_Tipo_Talonario.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.CodDocumentoTipo == info.CodDocumentoTipo && q.Establecimiento == info.Establecimiento && q.PuntoEmision == info.PuntoEmision && q.NumDocumento == info.NumDocumento);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = "I";
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// crear una funcion donde recibe parametro idEmpresa, CodDocumentotipo, establecimiento y pto emision, select a la base devuelve max.numdoc
        public string get_NumeroDocumentoInicial (int IdEmpresa, string CodDcumentoTipo, string Establecimiento, string PuntoEmision)
        {
            try
            {
                string NumeroDocumento = "000000001";
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_sis_Documento_Tipo_Talonario
                              where q.IdEmpresa == IdEmpresa
                              && q.CodDocumentoTipo == CodDcumentoTipo
                              && q.PuntoEmision == PuntoEmision
                              && q.Establecimiento == Establecimiento
                              select q;
                    if (lst.Count() > 0)
                    {
                        NumeroDocumento = lst.Max(q => q.NumDocumento) + "";
                        double NumeroDocumento_double = Convert.ToDouble(NumeroDocumento) + 1;
                        NumeroDocumento = NumeroDocumento_double.ToString("000000000");
                    }         
                }
                return NumeroDocumento;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificar_estado_usadoDB(tb_sis_Documento_Tipo_Talonario_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Documento_Tipo_Talonario Entity = Context.tb_sis_Documento_Tipo_Talonario.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.CodDocumentoTipo == info.CodDocumentoTipo && q.Establecimiento == info.Establecimiento && q.PuntoEmision == info.PuntoEmision && q.NumDocumento == info.NumDocumento);
                    if (Entity == null) return false;
                    Entity.Usado = info.Usado;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_sis_Documento_Tipo_Talonario_Info GetUltimoNoUsado(int IdEmpresa, string CodDocumentoTipo, string Establecimiento, string PuntoEmision, bool EsDocumentoElectronico, bool Actualizar)
        {
            try
            {
                DateTime Fecha = DateTime.Now.Date;
                tb_sis_Documento_Tipo_Talonario_Info info = new tb_sis_Documento_Tipo_Talonario_Info();
                if (EsDocumentoElectronico)
                    Fecha = new DateTime(2000, 1, 1);
                using (Entities_general db = new Entities_general())
                {
                    var q = (from A in db.tb_sis_Documento_Tipo_Talonario
                             where A.IdEmpresa == IdEmpresa
                             && A.CodDocumentoTipo == CodDocumentoTipo
                             && A.Establecimiento == Establecimiento
                             && A.PuntoEmision == PuntoEmision
                             && A.Usado == false
                             && A.Estado == "A"
                             && A.es_Documento_Electronico == EsDocumentoElectronico
                             && Fecha <= A.FechaCaducidad
                             select A.NumDocumento).Min();
                    if (q != null)
                    {

                        string UltRegistro = q.ToString();
                        var Entity = db.tb_sis_Documento_Tipo_Talonario.Where(v => v.IdEmpresa == IdEmpresa && v.CodDocumentoTipo == CodDocumentoTipo && v.Establecimiento == Establecimiento && v.PuntoEmision == PuntoEmision && v.Estado == "A" && v.Usado == false && v.es_Documento_Electronico == EsDocumentoElectronico).FirstOrDefault();
                        if (Entity != null)
                        {
                            if (Actualizar)
                            {
                                Entity.Usado = true;
                                db.SaveChanges();
                            }
                            info = new tb_sis_Documento_Tipo_Talonario_Info
                            {
                                IdEmpresa = Entity.IdEmpresa,
                                Establecimiento = Entity.Establecimiento,
                                PuntoEmision = Entity.PuntoEmision,
                                NumDocumento = Entity.NumDocumento,
                                CodDocumentoTipo = Entity.CodDocumentoTipo,
                                es_Documento_Electronico = Entity.es_Documento_Electronico ?? false,
                                FechaCaducidad = Entity.FechaCaducidad,
                                NumAutorizacion = Entity.NumAutorizacion                               
                            };
                        }
                    }
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
