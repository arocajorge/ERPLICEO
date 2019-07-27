CREATE TABLE [dbo].[tb_persona] (
    [IdPersona]                  NUMERIC (18)  NOT NULL,
    [CodPersona]                 VARCHAR (20)  NULL,
    [pe_Naturaleza]              VARCHAR (25)  NOT NULL,
    [pe_nombreCompleto]          VARCHAR (200) NOT NULL,
    [pe_razonSocial]             VARCHAR (150) NULL,
    [pe_apellido]                VARCHAR (100) NULL,
    [pe_nombre]                  VARCHAR (100) NULL,
    [IdTipoDocumento]            VARCHAR (25)  NOT NULL,
    [pe_cedulaRuc]               VARCHAR (50)  NOT NULL,
    [pe_direccion]               VARCHAR (150) NULL,
    [pe_telfono_Contacto]        VARCHAR (50)  NULL,
    [pe_celular]                 VARCHAR (50)  NULL,
    [pe_correo]                  VARCHAR (100) NULL,
    [pe_sexo]                    VARCHAR (25)  NULL,
    [IdEstadoCivil]              VARCHAR (25)  NULL,
    [pe_fechaNacimiento]         DATETIME      NULL,
    [pe_estado]                  VARCHAR (1)   NOT NULL,
    [pe_fechaCreacion]           DATETIME      NULL,
    [pe_fechaModificacion]       DATETIME      NULL,
    [pe_UltUsuarioModi]          VARCHAR (50)  NULL,
    [IdUsuarioUltAnu]            VARCHAR (20)  NULL,
    [Fecha_UltAnu]               DATETIME      NULL,
    [MotivoAnulacion]            VARCHAR (100) NULL,
    [IdTipoCta_acreditacion_cat] VARCHAR (25)  NULL,
    [num_cta_acreditacion]       VARCHAR (50)  NULL,
    [IdBanco_acreditacion]       INT           NULL,
    CONSTRAINT [PK_tb_persona] PRIMARY KEY CLUSTERED ([IdPersona] ASC),
    CONSTRAINT [FK_tb_persona_tb_banco] FOREIGN KEY ([IdBanco_acreditacion]) REFERENCES [dbo].[tb_banco] ([IdBanco]),
    CONSTRAINT [FK_tb_persona_tb_Catalogo] FOREIGN KEY ([pe_Naturaleza]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_tb_persona_tb_Catalogo1] FOREIGN KEY ([IdTipoDocumento]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_tb_persona_tb_Catalogo2] FOREIGN KEY ([IdTipoDocumento]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_tb_persona_tb_Catalogo3] FOREIGN KEY ([IdEstadoCivil]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_tb_persona_tb_Catalogo4] FOREIGN KEY ([pe_sexo]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_tb_persona_tb_Catalogo5] FOREIGN KEY ([pe_Naturaleza]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_tb_persona_tb_Catalogo6] FOREIGN KEY ([IdTipoCta_acreditacion_cat]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo])
);


GO
CREATE NONCLUSTERED INDEX [IX_tb_persona_1]
    ON [dbo].[tb_persona]([IdPersona] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tb_persona]
    ON [dbo].[tb_persona]([pe_cedulaRuc] ASC);

