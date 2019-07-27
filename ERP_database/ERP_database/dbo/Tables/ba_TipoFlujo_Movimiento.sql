CREATE TABLE [dbo].[ba_TipoFlujo_Movimiento] (
    [IdEmpresa]             INT           NOT NULL,
    [IdMovimiento]          NUMERIC (18)  NOT NULL,
    [IdTipoFlujo]           NUMERIC (18)  NOT NULL,
    [IdSucursal]            INT           NOT NULL,
    [IdBanco]               INT           NOT NULL,
    [Valor]                 FLOAT (53)    NOT NULL,
    [Fecha]                 DATE          NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ba_TipoFlujo_Movimiento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMovimiento] ASC),
    CONSTRAINT [FK_ba_TipoFlujo_Movimiento_ba_Banco_Cuenta] FOREIGN KEY ([IdEmpresa], [IdBanco]) REFERENCES [dbo].[ba_Banco_Cuenta] ([IdEmpresa], [IdBanco]),
    CONSTRAINT [FK_ba_TipoFlujo_Movimiento_ba_TipoFlujo] FOREIGN KEY ([IdEmpresa], [IdTipoFlujo]) REFERENCES [dbo].[ba_TipoFlujo] ([IdEmpresa], [IdTipoFlujo]),
    CONSTRAINT [FK_ba_TipoFlujo_Movimiento_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);

