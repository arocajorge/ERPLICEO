create view [dbo].[vwaf_Periodo_Sin_Depreciar] as
select *
from vwct_periodo per where NOT EXISTS (Select * from Af_Depreciacion af WHERE per.IdEmpresa = af.IdEmpresa AND per.IdPeriodo = af.IdPeriodo AND af.Estado = 'A')