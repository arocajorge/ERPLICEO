CREATE TABLE [dbo].[cp_retencion_x_ct_cbtecble] (
    [rt_IdEmpresa]   INT          NOT NULL,
    [rt_IdRetencion] NUMERIC (18) NOT NULL,
    [ct_IdEmpresa]   INT          NOT NULL,
    [ct_IdTipoCbte]  INT          NOT NULL,
    [ct_IdCbteCble]  NUMERIC (18) NOT NULL,
    [Observacion]    VARCHAR (50) NULL,
    CONSTRAINT [PK_cp_retencion_x_ct_cbtecble_1] PRIMARY KEY CLUSTERED ([rt_IdEmpresa] ASC, [rt_IdRetencion] ASC, [ct_IdEmpresa] ASC, [ct_IdTipoCbte] ASC, [ct_IdCbteCble] ASC),
    CONSTRAINT [FK_cp_retencion_x_ct_cbtecble_cp_retencion] FOREIGN KEY ([rt_IdEmpresa], [rt_IdRetencion]) REFERENCES [dbo].[cp_retencion] ([IdEmpresa], [IdRetencion]),
    CONSTRAINT [FK_cp_retencion_x_ct_cbtecble_ct_cbtecble] FOREIGN KEY ([ct_IdEmpresa], [ct_IdTipoCbte], [ct_IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble])
);

