CREATE TABLE [dbo].[in_movi_inven_tipo_x_tb_bodega] (
    [IdEmpresa]         INT          NOT NULL,
    [IdMovi_inven_tipo] INT          NOT NULL,
    [IdSucucursal]      INT          NOT NULL,
    [IdBodega]          INT          NOT NULL,
    [IdCtaCble]         VARCHAR (20) NULL,
    CONSTRAINT [PK_in_movi_inven_tipo_x_tb_bodega] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMovi_inven_tipo] ASC, [IdBodega] ASC, [IdSucucursal] ASC),
    CONSTRAINT [FK_in_movi_inven_tipo_x_tb_bodega_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_movi_inven_tipo_x_tb_bodega_in_movi_inven_tipo] FOREIGN KEY ([IdEmpresa], [IdMovi_inven_tipo]) REFERENCES [dbo].[in_movi_inven_tipo] ([IdEmpresa], [IdMovi_inven_tipo]),
    CONSTRAINT [FK_in_movi_inven_tipo_x_tb_bodega_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega])
);

