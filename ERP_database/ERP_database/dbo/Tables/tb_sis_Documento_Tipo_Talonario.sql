CREATE TABLE [dbo].[tb_sis_Documento_Tipo_Talonario] (
    [IdEmpresa]                INT           NOT NULL,
    [CodDocumentoTipo]         VARCHAR (20)  NOT NULL,
    [Establecimiento]          VARCHAR (3)   NOT NULL,
    [PuntoEmision]             VARCHAR (3)   NOT NULL,
    [NumDocumento]             VARCHAR (20)  NOT NULL,
    [FechaCaducidad]           DATE          NULL,
    [Usado]                    BIT           NULL,
    [Estado]                   CHAR (1)      NULL,
    [IdSucursal]               INT           NOT NULL,
    [NumAutorizacion]          VARCHAR (150) NULL,
    [es_Documento_Electronico] BIT           NULL,
    CONSTRAINT [PK_tb_sis_Documento_Tipo_Talonario_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [CodDocumentoTipo] ASC, [PuntoEmision] ASC, [Establecimiento] ASC, [NumDocumento] ASC),
    CONSTRAINT [FK_tb_sis_Documento_Tipo_Talonario_tb_sis_Documento_Tipo] FOREIGN KEY ([CodDocumentoTipo]) REFERENCES [dbo].[tb_sis_Documento_Tipo] ([codDocumentoTipo]),
    CONSTRAINT [FK_tb_sis_Documento_Tipo_Talonario_tb_sis_Documento_Tipo_x_Empresa] FOREIGN KEY ([IdEmpresa], [CodDocumentoTipo]) REFERENCES [dbo].[tb_sis_Documento_Tipo_x_Empresa] ([IdEmpresa], [codDocumentoTipo])
);



