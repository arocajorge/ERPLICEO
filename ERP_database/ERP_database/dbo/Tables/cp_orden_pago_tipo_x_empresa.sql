CREATE TABLE [dbo].[cp_orden_pago_tipo_x_empresa] (
    [IdEmpresa]               INT          NOT NULL,
    [IdTipo_op]               VARCHAR (20) NOT NULL,
    [IdCtaCble]               VARCHAR (20) NULL,
    [IdTipoCbte_OP]           INT          NULL,
    [IdEstadoAprobacion]      VARCHAR (10) NULL,
    [Buscar_FactxPagar]       CHAR (1)     NULL,
    [IdCtaCble_Credito]       VARCHAR (20) NULL,
    [Dispara_Alerta]          BIT          NOT NULL,
    CONSTRAINT [PK_cp_orden_pago_tipo_x_empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipo_op] ASC),
    CONSTRAINT [FK_cp_orden_pago_tipo_x_empresa_cp_orden_pago_tipo] FOREIGN KEY ([IdTipo_op]) REFERENCES [dbo].[cp_orden_pago_tipo] ([IdTipo_op]),
    CONSTRAINT [FK_cp_orden_pago_tipo_x_empresa_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);

