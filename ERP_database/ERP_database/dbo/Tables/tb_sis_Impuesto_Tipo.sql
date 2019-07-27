CREATE TABLE [dbo].[tb_sis_Impuesto_Tipo] (
    [IdTipoImpuesto]   VARCHAR (50) NOT NULL,
    [nom_tipoImpuesto] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tb_sis_Impuesto_Tipo] PRIMARY KEY CLUSTERED ([IdTipoImpuesto] ASC)
);

