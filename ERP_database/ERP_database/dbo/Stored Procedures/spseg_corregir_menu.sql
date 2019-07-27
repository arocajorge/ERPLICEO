CREATE PROCEDURE spseg_corregir_menu
(
@IdEmpresa int, 
@IdUsuario varchar(20)
)
AS
insert into seg_Menu_x_Empresa
select @IdEmpresa, menu.IdMenuPadre, null
from seg_Menu as menu
join seg_Menu_x_Empresa as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and menu.IdMenuPadre != 0
and not exists(
select * from seg_Menu_x_Empresa as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
)
group by menu.IdMenuPadre

insert into seg_Menu_x_Empresa
select @IdEmpresa, menu.IdMenuPadre, null
from seg_Menu as menu
join seg_Menu_x_Empresa as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and menu.IdMenuPadre != 0
and not exists(
select * from seg_Menu_x_Empresa as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
)
group by menu.IdMenuPadre

insert into seg_Menu_x_Empresa_x_Usuario
select @IdEmpresa, @IdUsuario, menu.IdMenuPadre, 0,0,0
from seg_Menu as menu
join seg_Menu_x_Empresa_x_Usuario as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and me.IdUsuario = @IdUsuario
and menu.IdMenuPadre != 0
and not exists(
select * from seg_Menu_x_Empresa_x_Usuario as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
and f.IdUsuario = @IdUsuario
)
group by menu.IdMenuPadre

insert into seg_Menu_x_Empresa_x_Usuario
select @IdEmpresa, @IdUsuario, menu.IdMenuPadre, 0,0,0
from seg_Menu as menu
join seg_Menu_x_Empresa_x_Usuario as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and me.IdUsuario = @IdUsuario
and menu.IdMenuPadre != 0
and not exists(
select * from seg_Menu_x_Empresa_x_Usuario as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
and f.IdUsuario = @IdUsuario
)
group by menu.IdMenuPadre
