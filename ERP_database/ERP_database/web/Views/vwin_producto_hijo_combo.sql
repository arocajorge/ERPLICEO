CREATE VIEW web.vwin_producto_hijo_combo
AS
select pro.IdEmpresa, pro.IdProducto, isnull(pro.IdProducto_padre,0)IdProducto_padre, pro.pr_descripcion, pre.nom_presentacion,ca.ca_Categoria, pro.lote_num_lote, pro.lote_fecha_vcto,pro.IdUnidadMedida
from in_Producto as pro
inner join in_categorias as ca on pro.IdEmpresa = ca.IdEmpresa and pro.IdCategoria = ca.IdCategoria
inner join in_presentacion as pre on pro.IdEmpresa = pre.IdEmpresa and pro.IdPresentacion = pre.IdPresentacion
where not exists(
select p.IdEmpresa from in_Producto as p
where p.IdEmpresa = pro.IdEmpresa
and p.IdProducto_padre = pro.IdProducto
) and pro.Estado = 'A'