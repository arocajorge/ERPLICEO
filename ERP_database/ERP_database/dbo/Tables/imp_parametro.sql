CREATE TABLE [dbo].[imp_parametro] (
    [IdEmpresa]                  INT          NOT NULL,
    [IdCtaCble]                  VARCHAR (20) NOT NULL,
    [IdTipoCbte_liquidacion]     INT          NOT NULL,
    [IdTipoCbte_liquidacion_anu] INT          NOT NULL,
    [IdMotivo_Inv_ing]           INT          NOT NULL,
    [IdMovi_inven_tipo_ing]      INT          NOT NULL,
    [IdSucursal]                 INT          NOT NULL,
    [IdBodega]                   INT          NOT NULL,
    [IdCtaCble_invntario]        VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_imp_parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_imp_parametro_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [IdTipoCbte_liquidacion]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_imp_parametro_ct_cbtecble_tipo1] FOREIGN KEY ([IdEmpresa], [IdTipoCbte_liquidacion_anu]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_imp_parametro_in_Motivo_Inven] FOREIGN KEY ([IdEmpresa], [IdMotivo_Inv_ing]) REFERENCES [dbo].[in_Motivo_Inven] ([IdEmpresa], [IdMotivo_Inv]),
    CONSTRAINT [FK_imp_parametro_in_movi_inven_tipo] FOREIGN KEY ([IdEmpresa], [IdMovi_inven_tipo_ing]) REFERENCES [dbo].[in_movi_inven_tipo] ([IdEmpresa], [IdMovi_inven_tipo]),
    CONSTRAINT [FK_imp_parametro_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_imp_parametro_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);



