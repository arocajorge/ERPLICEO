CREATE VIEW [dbo].[vwfa_notaCreDeb_ParaConciliarNC]
AS
select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, c.Total, dbo.BankersRounding(isnull(sum(b.Valor_Aplicado),0),2)Valor_Aplicado,
case when a.NaturalezaNota = 'SRI' THEN a.Serie1+'-'+a.Serie2+'-'+a.NumNota_Impresa else a.CodNota end as NumNota, a.no_fecha, a.sc_observacion, f.No_Descripcion
from fa_notaCreDeb as a left join 
fa_notaCreDeb_x_fa_factura_NotaDeb as b on a.IdEmpresa = b.IdEmpresa_nt and a.IdSucursal = b.IdSucursal_nt and a.IdBodega = b.IdBodega_nt and a.IdNota = b.IdNota_nt inner join 
fa_notaCreDeb_resumen as c on a.IdEmpresa = c.IdEmpresa and a.IdSucursal = c.IdSucursal and a.IdBodega = c.IdBodega and a.IdNota = c.IdNota inner join aca_Alumno as d
on a.IdEmpresa = d.IdEmpresa and a.IdAlumno = d.IdAlumno inner join tb_persona as e
on e.IdPersona = d.IdPersona INNER JOIn
fa_TipoNota as f on a.IdEmpresa = f.IdEmpresa and a.IdTipoNota = f.IdTipoNota
where a.CreDeb = 'C' and a.Estado = 'A'
group by a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, c.Total, a.NaturalezaNota, a.Serie1, a.Serie2, a.NumNota_Impresa, a.CodNota, a.no_fecha, a.sc_observacion, f.No_Descripcion
having c.Total - dbo.BankersRounding(isnull(sum(b.Valor_Aplicado),0),2) > 0