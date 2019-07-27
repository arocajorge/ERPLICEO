CREATE TABLE [web].[ba_SPBAN_004] (
    [IdEmpresa]      INT          NOT NULL,
    [IdConciliacion] NUMERIC (18) NOT NULL,
    [IdTipoCbte]     INT          NOT NULL,
    [IdCbteCble]     NUMERIC (18) NOT NULL,
    [secuencia]      INT          NOT NULL,
    CONSTRAINT [PK_ba_SPBAN_004] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC, [secuencia] ASC)
);

