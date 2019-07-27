CREATE TABLE [dbo].[ct_anio_fiscal] (
    [IdanioFiscal] INT          NOT NULL,
    [af_fechaIni]  DATE         NOT NULL,
    [af_fechaFin]  DATE         NOT NULL,
    [af_estado]    NVARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ct_anio_fiscal_1] PRIMARY KEY CLUSTERED ([IdanioFiscal] ASC)
);

