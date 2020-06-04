CREATE TABLE [dbo].[fa_notaCreDeb_Masiva] (
    [IdEmpresa]             INT           NOT NULL,
    [IdNCMasivo]            NUMERIC (18)  NOT NULL,
    [IdSucursal]            INT           NOT NULL,
    [IdBodega]              INT           NOT NULL,
    [IdPuntoVta]            INT           NOT NULL,
    [CreDeb]                VARCHAR (1)   NOT NULL,
    [no_fecha]              DATETIME      NOT NULL,
    [no_fecha_venc]         DATETIME      NOT NULL,
    [IdTipoNota]            INT           NOT NULL,
    [NaturalezaNota]        CHAR (3)      NULL,
    [sc_observacion]        VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_fa_notaCreDeb_Masiva] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNCMasivo] ASC),
    CONSTRAINT [FK_fa_notaCreDeb_Masiva_fa_TipoNota] FOREIGN KEY ([IdEmpresa], [IdTipoNota]) REFERENCES [dbo].[fa_TipoNota] ([IdEmpresa], [IdTipoNota])
);

