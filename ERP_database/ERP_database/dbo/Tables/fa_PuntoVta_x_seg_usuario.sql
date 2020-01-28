CREATE TABLE [dbo].[fa_PuntoVta_x_seg_usuario] (
    [IdEmpresa]  INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [IdPuntoVta] INT          NOT NULL,
    [Secuencia]  INT          NOT NULL,
    [IdUsuario]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_fa_PuntoVta_x_seg_usuario] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdPuntoVta] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_PuntoVta_x_seg_usuario_fa_PuntoVta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdPuntoVta]) REFERENCES [dbo].[fa_PuntoVta] ([IdEmpresa], [IdSucursal], [IdPuntoVta]),
    CONSTRAINT [FK_fa_PuntoVta_x_seg_usuario_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario])
);

