create view [dbo].[vwcp_Tipo_Impresion_Cheq]
as                      
select IdCatalogo as Codigo,Nombre as Descripcion
from cp_catalogo    
where IdCatalogo_tipo='T_IMPR_RET'