CREATE TABLE [dbo].[cxc_LiquidacionTarjeta] (
    [IdEmpresa]             INT            NOT NULL,
    [IdSucursal]            INT            NOT NULL,
    [IdLiquidacion]         NUMERIC (18)   NOT NULL,
    [Lote]                  VARCHAR (1000) NULL,
    [Fecha]                 DATE           NOT NULL,
    [IdBanco]               INT            NOT NULL,
    [Observacion]           VARCHAR (MAX)  NULL,
    [Estado]                BIT            NOT NULL,
    [Valor]                 FLOAT (53)     NOT NULL,
    [IdEmpresa_ct]          INT            NULL,
    [IdTipoCbte_ct]         INT            NULL,
    [IdCbteCble_ct]         NUMERIC (18)   NULL,
    [IdUsuarioCreacion]     VARCHAR (50)   NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (50)   NULL,
    [FechaModificacion]     DATETIME       NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)   NULL,
    [FechaAnulacion]        DATETIME       NULL,
    [MotivoAnulacion]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_cxc_LiquidacionTarjeta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdLiquidacion] ASC),
    CONSTRAINT [FK_cxc_LiquidacionTarjeta_ba_Banco_Cuenta] FOREIGN KEY ([IdEmpresa], [IdBanco]) REFERENCES [dbo].[ba_Banco_Cuenta] ([IdEmpresa], [IdBanco]),
    CONSTRAINT [FK_cxc_LiquidacionTarjeta_ct_cbtecble] FOREIGN KEY ([IdEmpresa_ct], [IdTipoCbte_ct], [IdCbteCble_ct]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_cxc_LiquidacionTarjeta_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);



