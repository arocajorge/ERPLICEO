CREATE TABLE [dbo].[tb_sis_Documento_Tipo_x_Empresa] (
    [IdEmpresa]                INT          NOT NULL,
    [codDocumentoTipo]         VARCHAR (20) NOT NULL,
    [ApareceComboFac_TipoFact] CHAR (1)     NOT NULL,
    [ApareceComboFac_Import]   CHAR (1)     NOT NULL,
    [ApareceTalonario]         CHAR (1)     NOT NULL,
    [Descripcion]              VARCHAR (50) NOT NULL,
    [Posicion]                 INT          NOT NULL,
    [ApareceCombo_FileReporte] CHAR (1)     NOT NULL,
    CONSTRAINT [PK_tb_sis_Documento_Tipo_x_Empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [codDocumentoTipo] ASC),
    CONSTRAINT [FK_tb_sis_Documento_Tipo_x_Empresa_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);

