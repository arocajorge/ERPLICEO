CREATE VIEW vwct_grupo_x_Tipo_Gasto
AS
SELECT        ct_grupo_x_Tipo_Gasto.IdEmpresa, ct_grupo_x_Tipo_Gasto.IdTipo_Gasto, ct_grupo_x_Tipo_Gasto.nom_tipo_Gasto, ct_grupo_x_Tipo_Gasto.IdTipo_Gasto_Padre, 
                         ct_grupo_x_Tipo_Gasto_1.nom_tipo_Gasto AS nom_tipo_Gasto_Padre, ct_grupo_x_Tipo_Gasto.estado, ct_grupo_x_Tipo_Gasto.nivel, 
                         ct_grupo_x_Tipo_Gasto.orden
FROM            ct_grupo_x_Tipo_Gasto INNER JOIN
                         ct_grupo_x_Tipo_Gasto AS ct_grupo_x_Tipo_Gasto_1 ON ct_grupo_x_Tipo_Gasto.IdEmpresa = ct_grupo_x_Tipo_Gasto_1.IdEmpresa AND 
                         ct_grupo_x_Tipo_Gasto.IdTipo_Gasto_Padre = ct_grupo_x_Tipo_Gasto_1.IdTipo_Gasto