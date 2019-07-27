CREATE TABLE [dbo].[in_spSys_inv_Reversar_aprobacion_ct_cbtecble] (
    [IdEmpresa]  INT          NOT NULL,
    [IdTipoCbte] INT          NOT NULL,
    [IdCbteCble] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_in_spSys_inv_Reversar_aprobacion_ct_cbtecble] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC)
);

