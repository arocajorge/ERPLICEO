CREATE TABLE [dbo].[fa_TipoNota_x_Empresa_x_Sucursal] (
    [IdEmpresa]  INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [IdTipoNota] INT          NOT NULL,
    [IdCtaCble]  VARCHAR (20) NULL,
    CONSTRAINT [PK_fa_TipoNota_x_Empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdTipoNota] ASC),
    CONSTRAINT [FK_fa_TipoNota_x_Empresa_x_Sucursal_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_fa_TipoNota_x_Empresa_x_Sucursal_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);



