CREATE TABLE [EntidadRegulatoria].[fa_elec_registros_generados] (
    [ID_REGISTRO] VARCHAR (100) NOT NULL,
    [FECHA_CARGA] DATE          NOT NULL,
    [ESTADO]      CHAR (1)      NOT NULL,
    CONSTRAINT [PK_fa_elec_registros_generados] PRIMARY KEY CLUSTERED ([ID_REGISTRO] ASC)
);



GO


