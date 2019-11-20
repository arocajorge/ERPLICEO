CREATE TABLE [dbo].[aca_Menu_x_seg_usuario] (
    [IdEmpresa] INT          NOT NULL,
    [IdSede]    INT          NOT NULL,
    [IdMenu]    INT          NOT NULL,
    [IdUsuario] VARCHAR (50) NOT NULL,
    [Nuevo]     BIT          NOT NULL,
    [Modificar] BIT          NOT NULL,
    [Anular]    BIT          NOT NULL,
    CONSTRAINT [PK_aca_Menu_x_seg_usuario] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSede] ASC, [IdMenu] ASC, [IdUsuario] ASC),
    CONSTRAINT [FK_aca_Menu_x_seg_usuario_aca_Menu] FOREIGN KEY ([IdMenu]) REFERENCES [dbo].[aca_Menu] ([IdMenu]),
    CONSTRAINT [FK_aca_Menu_x_seg_usuario_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede]),
    CONSTRAINT [FK_aca_Menu_x_seg_usuario_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario])
);



