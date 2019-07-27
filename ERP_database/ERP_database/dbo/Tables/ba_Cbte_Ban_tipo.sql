CREATE TABLE [dbo].[ba_Cbte_Ban_tipo] (
    [CodTipoCbteBan] VARCHAR (20) NOT NULL,
    [Descripcion]    VARCHAR (50) NOT NULL,
    [Signo]          CHAR (1)     NOT NULL,
    [orden]          INT          NOT NULL,
    CONSTRAINT [PK_ba_Cbte_Ban_tipo_1] PRIMARY KEY CLUSTERED ([CodTipoCbteBan] ASC)
);

