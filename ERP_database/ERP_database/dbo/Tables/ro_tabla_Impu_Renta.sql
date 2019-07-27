CREATE TABLE [dbo].[ro_tabla_Impu_Renta] (
    [AnioFiscal]           INT        NOT NULL,
    [Secuencia]            INT        NOT NULL,
    [FraccionBasica]       FLOAT (53) NULL,
    [ExcesoHasta]          FLOAT (53) NULL,
    [ImpFraccionBasica]    FLOAT (53) NULL,
    [Por_ImpFraccion_Exce] FLOAT (53) NULL,
    CONSTRAINT [PK_ro_tabla_Impu_Renta] PRIMARY KEY CLUSTERED ([AnioFiscal] ASC, [Secuencia] ASC)
);

