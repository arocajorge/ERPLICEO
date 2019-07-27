CREATE VIEW vwin_Producto_para_composicion
AS
select p.IdEmpresa, p.IdProducto, p.pr_descripcion, p.pr_descripcion_2, p.pr_codigo, c.ca_Categoria, pre.nom_presentacion,
p.lote_num_lote, p.lote_fecha_fab, p.lote_fecha_vcto, p.Estado, t.tp_descripcion
from in_Producto as p left join in_productotipo as t
on p.IdEmpresa = t.IdEmpresa and p.IdProductoTipo = t.IdProductoTipo
left join in_presentacion as pre on p.IdEmpresa = pre.IdEmpresa
and p.IdPresentacion = pre.IdPresentacion left join in_categorias as c
on c.IdEmpresa = p.IdEmpresa and c.IdCategoria = p.IdCategoria
where not exists(
select f.IdEmpresa from in_Producto as f
where f.IdEmpresa = p.IdEmpresa
and f.IdProducto_padre = p.IdProducto
)