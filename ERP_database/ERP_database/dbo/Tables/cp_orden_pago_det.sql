CREATE TABLE [dbo].[cp_orden_pago_det] (
    [IdEmpresa]            INT           NOT NULL,
    [IdOrdenPago]          NUMERIC (18)  NOT NULL,
    [Secuencia]            INT           NOT NULL,
    [IdEmpresa_cxp]        INT           NULL,
    [IdCbteCble_cxp]       NUMERIC (18)  NULL,
    [IdTipoCbte_cxp]       INT           NULL,
    [Valor_a_pagar]        FLOAT (53)    NOT NULL,
    [Referencia]           VARCHAR (MAX) NULL,
    [IdFormaPago]          VARCHAR (20)  NOT NULL,
    [Fecha_Pago]           DATE          NOT NULL,
    [IdEstadoAprobacion]   VARCHAR (10)  NOT NULL,
    [IdBanco]              INT           NULL,
    [IdUsuario_Aprobacion] VARCHAR (20)  NULL,
    [fecha_hora_Aproba]    DATETIME      NULL,
    [Motivo_aproba]        VARCHAR (150) NULL,
    CONSTRAINT [PK_cp_orden_pago_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdOrdenPago] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_orden_pago_det_ba_Banco_Cuenta] FOREIGN KEY ([IdEmpresa], [IdBanco]) REFERENCES [dbo].[ba_Banco_Cuenta] ([IdEmpresa], [IdBanco]),
    CONSTRAINT [FK_cp_orden_pago_det_cp_orden_pago] FOREIGN KEY ([IdEmpresa], [IdOrdenPago]) REFERENCES [dbo].[cp_orden_pago] ([IdEmpresa], [IdOrdenPago]),
    CONSTRAINT [FK_cp_orden_pago_det_cp_orden_pago_formapago] FOREIGN KEY ([IdFormaPago]) REFERENCES [dbo].[cp_orden_pago_formapago] ([IdFormaPago]),
    CONSTRAINT [FK_cp_orden_pago_det_ct_cbtecble] FOREIGN KEY ([IdEmpresa_cxp], [IdTipoCbte_cxp], [IdCbteCble_cxp]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble])
);




GO
CREATE NONCLUSTERED INDEX [IX_cp_orden_pago_det]
    ON [dbo].[cp_orden_pago_det]([IdEmpresa] ASC, [IdOrdenPago] ASC, [Secuencia] ASC, [IdEstadoAprobacion] ASC);

