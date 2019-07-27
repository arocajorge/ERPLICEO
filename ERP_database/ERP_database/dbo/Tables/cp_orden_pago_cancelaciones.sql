CREATE TABLE [dbo].[cp_orden_pago_cancelaciones] (
    [IdEmpresa]            INT           NOT NULL,
    [Idcancelacion]        NUMERIC (18)  NOT NULL,
    [Secuencia]            INT           NOT NULL,
    [IdEmpresa_op]         INT           NOT NULL,
    [IdOrdenPago_op]       NUMERIC (18)  NOT NULL,
    [Secuencia_op]         INT           NOT NULL,
    [IdEmpresa_op_padre]   INT           NULL,
    [IdOrdenPago_op_padre] NUMERIC (18)  NULL,
    [Secuencia_op_padre]   INT           NULL,
    [IdEmpresa_cxp]        INT           NULL,
    [IdTipoCbte_cxp]       INT           NULL,
    [IdCbteCble_cxp]       NUMERIC (18)  NULL,
    [IdEmpresa_pago]       INT           NOT NULL,
    [IdTipoCbte_pago]      INT           NOT NULL,
    [IdCbteCble_pago]      NUMERIC (18)  NOT NULL,
    [MontoAplicado]        FLOAT (53)    NOT NULL,
    [SaldoAnterior]        FLOAT (53)    NOT NULL,
    [SaldoActual]          FLOAT (53)    NOT NULL,
    [Observacion]          VARCHAR (MAX) NOT NULL,
    [fechaTransaccion]     DATETIME      NOT NULL,
    CONSTRAINT [PK_cp_cancelacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [Idcancelacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_orden_pago_cancelaciones_cp_orden_pago_det] FOREIGN KEY ([IdEmpresa_op], [IdOrdenPago_op], [Secuencia_op]) REFERENCES [dbo].[cp_orden_pago_det] ([IdEmpresa], [IdOrdenPago], [Secuencia]),
    CONSTRAINT [FK_cp_orden_pago_cancelaciones_cp_orden_pago_det1] FOREIGN KEY ([IdEmpresa_op_padre], [IdOrdenPago_op_padre], [Secuencia_op_padre]) REFERENCES [dbo].[cp_orden_pago_det] ([IdEmpresa], [IdOrdenPago], [Secuencia]),
    CONSTRAINT [FK_cp_orden_pago_cancelaciones_ct_cbtecble] FOREIGN KEY ([IdEmpresa_cxp], [IdTipoCbte_cxp], [IdCbteCble_cxp]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_cp_orden_pago_cancelaciones_ct_cbtecble1] FOREIGN KEY ([IdEmpresa_pago], [IdTipoCbte_pago], [IdCbteCble_pago]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble])
);


GO
CREATE NONCLUSTERED INDEX [IX_cp_orden_pago_cancelaciones_2]
    ON [dbo].[cp_orden_pago_cancelaciones]([IdEmpresa_cxp] ASC, [IdCbteCble_cxp] ASC, [IdTipoCbte_cxp] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_cp_orden_pago_cancelaciones_1]
    ON [dbo].[cp_orden_pago_cancelaciones]([IdEmpresa_op] ASC, [IdOrdenPago_op] ASC, [Secuencia_op] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_cp_orden_pago_cancelaciones]
    ON [dbo].[cp_orden_pago_cancelaciones]([IdEmpresa_pago] ASC, [IdCbteCble_pago] ASC, [IdTipoCbte_pago] ASC);

