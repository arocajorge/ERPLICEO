CREATE TABLE [dbo].[aca_Parametro] (
    [IdEmpresa]               INT        NOT NULL,
    [PromedioMinimoParcial]   FLOAT (53) NULL,
    [PromedioMinimoPromocion] FLOAT (53) NULL,
    CONSTRAINT [PK_aca_Parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC)
);

