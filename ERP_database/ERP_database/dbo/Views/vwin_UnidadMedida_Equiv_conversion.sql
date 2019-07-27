CREATE VIEW vwin_UnidadMedida_Equiv_conversion as
select
B.IdUnidadMedida, A.cod_alterno, A.Descripcion, B.IdUnidadMedida_equiva, B.valor_equiv, B.interpretacion
from in_UnidadMedida A 
INNER JOIN in_UnidadMedida_Equiv_conversion B
ON A.IdUnidadMedida = B.IdUnidadMedida_equiva