
CREATE PROCEDURE [dbo].[spaca_corregir_menu]
(
@IdEmpresa int,
@IdSede int, 
@IdUsuario varchar(20)
)
AS
insert into aca_Menu_x_aca_Sede 
(IdEmpresa,IdSede,IdMenu,Observacion)
select @IdEmpresa, @IdSede, menu.IdMenuPadre, null
from seg_Menu as menu
join aca_Menu_x_aca_Sede as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and menu.IdMenuPadre != 0
and not exists(
select * from aca_Menu_x_aca_Sede as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
)
group by menu.IdMenuPadre

insert into aca_Menu_x_aca_Sede
select @IdEmpresa,@IdSede, menu.IdMenuPadre, null
from seg_Menu as menu
join aca_Menu_x_aca_Sede as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and menu.IdMenuPadre != 0
and not exists(
select * from aca_Menu_x_aca_Sede as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
)
group by menu.IdMenuPadre

insert into aca_Menu_x_seg_usuario (IdEmpresa,IdSede,IdMenu,IdUsuario,Nuevo,Modificar,Anular)
select @IdEmpresa, @IdSede, menu.IdMenuPadre, @IdUsuario, 0,0,0
from seg_Menu as menu
join aca_Menu_x_seg_usuario as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and me.IdUsuario = @IdUsuario
and menu.IdMenuPadre != 0
and not exists(
select * from aca_Menu_x_seg_usuario as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
and f.IdUsuario = @IdUsuario
)
group by menu.IdMenuPadre

insert into aca_Menu_x_seg_usuario(IdEmpresa,IdSede,IdMenu,IdUsuario,Nuevo,Modificar,Anular)
select @IdEmpresa, @IdSede, menu.IdMenuPadre, @IdUsuario, 0,0,0
from seg_Menu as menu
join aca_Menu_x_seg_usuario as me
on me.IdMenu = menu.IdMenu
where me.IdEmpresa = @IdEmpresa 
and me.IdUsuario = @IdUsuario
and menu.IdMenuPadre != 0
and not exists(
select * from aca_Menu_x_seg_usuario as f
where f.IdEmpresa = @IdEmpresa 
and f.IdMenu = menu.IdMenuPadre
and f.IdUsuario = @IdUsuario
)
group by menu.IdMenuPadre