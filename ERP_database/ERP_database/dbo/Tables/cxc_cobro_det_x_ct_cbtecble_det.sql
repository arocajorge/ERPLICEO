CREATE TABLE [dbo].[cxc_cobro_det_x_ct_cbtecble_det] (
    [IdEmpresa_cb]  INT          NOT NULL,
    [IdSucursal_cb] INT          NOT NULL,
    [IdCobro_cb]    NUMERIC (18) NOT NULL,
    [secuencial_cb] INT          NOT NULL,
    [IdEmpresa_ct]  INT          NOT NULL,
    [IdTipoCbte_ct] INT          NOT NULL,
    [IdCbteCble_ct] NUMERIC (18) NOT NULL,
    [secuencia_ct]  INT          NOT NULL,
    [secuencia_reg] INT          NOT NULL,
    [observacion]   VARCHAR (50) NULL,
    CONSTRAINT [PK_cxc_cobro_det_x_ct_cbtecble_det] PRIMARY KEY CLUSTERED ([IdEmpresa_cb] ASC, [IdSucursal_cb] ASC, [IdCobro_cb] ASC, [secuencial_cb] ASC, [IdEmpresa_ct] ASC, [IdTipoCbte_ct] ASC, [IdCbteCble_ct] ASC, [secuencia_ct] ASC, [secuencia_reg] ASC)
);

