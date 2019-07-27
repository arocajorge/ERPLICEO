CREATE VIEW web.vwin_producto_padre_combo
AS
select pro.IdEmpresa, pro.IdProducto, pro.pr_descripcion, pre.nom_presentacion,ca.ca_Categoria
from in_Producto as pro
inner join in_categorias as ca on pro.IdEmpresa = ca.IdEmpresa and pro.IdCategoria = ca.IdCategoria
inner join in_presentacion as pre on pro.IdEmpresa = pre.IdEmpresa and pro.IdPresentacion = pre.IdPresentacion
where pro.IdProducto_padre is null and pro.Estado = 'A'