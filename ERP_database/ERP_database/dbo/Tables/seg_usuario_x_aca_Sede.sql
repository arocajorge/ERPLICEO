CREATE TABLE [dbo].[seg_usuario_x_aca_Sede] (
    [IdUsuario]   VARCHAR (50) NOT NULL,
    [IdEmpresa]   INT          NOT NULL,
    [IdSede]      INT          NOT NULL,
    [Observacion] VARCHAR (1)  NULL,
    CONSTRAINT [PK_seg_usuario_x_aca_Sede] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEmpresa] ASC, [IdSede] ASC),
    CONSTRAINT [FK_seg_usuario_x_aca_Sede_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede]),
    CONSTRAINT [FK_seg_usuario_x_aca_Sede_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario])
);

