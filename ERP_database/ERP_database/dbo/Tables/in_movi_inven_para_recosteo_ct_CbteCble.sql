CREATE TABLE [dbo].[in_movi_inven_para_recosteo_ct_CbteCble] (
    [IdEmpresa]  INT          NOT NULL,
    [IdTipoCbte] INT          NOT NULL,
    [IdCbteCble] NUMERIC (18) NOT NULL,
    [Secuencia]  INT          NOT NULL,
    CONSTRAINT [PK_in_movi_inve_para_recosteo_ct_CbteCble] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC, [Secuencia] ASC)
);

