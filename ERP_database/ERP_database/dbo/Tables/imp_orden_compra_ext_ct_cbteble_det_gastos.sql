CREATE TABLE [dbo].[imp_orden_compra_ext_ct_cbteble_det_gastos] (
    [IdEmpresa]         INT          NOT NULL,
    [IdOrdenCompra_ext] DECIMAL (18) NOT NULL,
    [IdEmpresa_ct]      INT          NOT NULL,
    [IdTipoCbte]        INT          NOT NULL,
    [IdCbteCble]        NUMERIC (18) NOT NULL,
    [secuencia_ct]      INT          NOT NULL,
    [IdGasto_tipo]      INT          NULL,
    CONSTRAINT [PK_imp_orden_compra_ext_ct_cbteble_det_gastos] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdOrdenCompra_ext] ASC, [IdEmpresa_ct] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC, [secuencia_ct] ASC),
    CONSTRAINT [FK_imp_orden_compra_ext_ct_cbteble_det_gastos_ct_cbtecble_det] FOREIGN KEY ([IdEmpresa_ct], [IdTipoCbte], [IdCbteCble], [secuencia_ct]) REFERENCES [dbo].[ct_cbtecble_det] ([IdEmpresa], [IdTipoCbte], [IdCbteCble], [secuencia]),
    CONSTRAINT [FK_imp_orden_compra_ext_ct_cbteble_det_gastos_imp_gasto] FOREIGN KEY ([IdGasto_tipo]) REFERENCES [dbo].[imp_gasto] ([IdGasto_tipo]),
    CONSTRAINT [FK_imp_orden_compra_ext_ct_cbteble_det_gastos_imp_orden_compra_ext] FOREIGN KEY ([IdEmpresa], [IdOrdenCompra_ext]) REFERENCES [dbo].[imp_orden_compra_ext] ([IdEmpresa], [IdOrdenCompra_ext])
);

