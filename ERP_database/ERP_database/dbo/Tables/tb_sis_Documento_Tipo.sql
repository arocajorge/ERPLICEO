CREATE TABLE [dbo].[tb_sis_Documento_Tipo] (
    [codDocumentoTipo] VARCHAR (20) NOT NULL,
    [descripcion]      VARCHAR (50) NULL,
    [estado]           CHAR (1)     NULL,
    [Posicion]         INT          NULL,
    CONSTRAINT [PK_fa_Documento_Tipo] PRIMARY KEY CLUSTERED ([codDocumentoTipo] ASC)
);

