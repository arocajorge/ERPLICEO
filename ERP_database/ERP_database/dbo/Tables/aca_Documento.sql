CREATE TABLE [dbo].[aca_Documento] (
    [IdEmpresa]             INT           NOT NULL,
    [IdDocumento]           INT           NOT NULL,
    [NomDocumento]          VARCHAR (500) NULL,
    [OrdenDocumento]        INT           NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Documento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDocumento] ASC)
);

