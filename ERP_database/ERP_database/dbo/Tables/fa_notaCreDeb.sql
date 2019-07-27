CREATE TABLE [dbo].[fa_notaCreDeb] (
    [IdEmpresa]           INT           NOT NULL,
    [IdSucursal]          INT           NOT NULL,
    [IdBodega]            INT           NOT NULL,
    [IdNota]              NUMERIC (18)  NOT NULL,
    [dev_IdEmpresa]       INT           NULL,
    [dev_IdDev_Inven]     NUMERIC (18)  NULL,
    [CodNota]             VARCHAR (50)  NULL,
    [CreDeb]              CHAR (3)      NOT NULL,
    [CodDocumentoTipo]    VARCHAR (20)  NOT NULL,
    [Serie1]              VARCHAR (3)   NULL,
    [Serie2]              VARCHAR (3)   NULL,
    [NumNota_Impresa]     VARCHAR (20)  NULL,
    [NumAutorizacion]     VARCHAR (50)  NULL,
    [Fecha_Autorizacion]  DATETIME      NULL,
    [IdCliente]           NUMERIC (18)  NOT NULL,
    [no_fecha]            DATETIME      NOT NULL,
    [no_fecha_venc]       DATETIME      NOT NULL,
    [IdTipoNota]          INT           NOT NULL,
    [sc_observacion]      VARCHAR (MAX) NULL,
    [IdUsuario]           VARCHAR (20)  NULL,
    [IdUsuarioUltMod]     VARCHAR (20)  NULL,
    [Fecha_UltMod]        DATETIME      NULL,
    [IdUsuarioUltAnu]     VARCHAR (20)  NULL,
    [Fecha_UltAnu]        DATETIME      NULL,
    [MotiAnula]           VARCHAR (200) NULL,
    [Estado]              CHAR (1)      NOT NULL,
    [NaturalezaNota]      CHAR (3)      NULL,
    [IdCtaCble_TipoNota]  VARCHAR (20)  NULL,
    [IdPuntoVta]          INT           NOT NULL,
    [aprobada_enviar_sri] BIT           NOT NULL,
    [Generado]            BIT           NULL,
    CONSTRAINT [PK_fa_notaCreDeb] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdNota] ASC),
    CONSTRAINT [FK_fa_notaCreDeb_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble_TipoNota]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_fa_notaCreDeb_fa_cliente] FOREIGN KEY ([IdEmpresa], [IdCliente]) REFERENCES [dbo].[fa_cliente] ([IdEmpresa], [IdCliente]),
    CONSTRAINT [FK_fa_notaCreDeb_fa_PuntoVta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdPuntoVta]) REFERENCES [dbo].[fa_PuntoVta] ([IdEmpresa], [IdSucursal], [IdPuntoVta]),
    CONSTRAINT [FK_fa_notaCreDeb_fa_TipoNota1] FOREIGN KEY ([IdEmpresa], [IdTipoNota]) REFERENCES [dbo].[fa_TipoNota] ([IdEmpresa], [IdTipoNota]),
    CONSTRAINT [FK_fa_notaCreDeb_in_devolucion_inven] FOREIGN KEY ([dev_IdEmpresa], [dev_IdDev_Inven]) REFERENCES [dbo].[in_devolucion_inven] ([IdEmpresa], [IdDev_Inven]),
    CONSTRAINT [FK_fa_notaCreDeb_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega]),
    CONSTRAINT [FK_fa_notaCreDeb_tb_sis_Documento_Tipo_Talonario] FOREIGN KEY ([IdEmpresa], [CodDocumentoTipo], [Serie2], [Serie1], [NumNota_Impresa]) REFERENCES [dbo].[tb_sis_Documento_Tipo_Talonario] ([IdEmpresa], [CodDocumentoTipo], [PuntoEmision], [Establecimiento], [NumDocumento])
);










GO
CREATE NONCLUSTERED INDEX [IX_fa_notaCreDeb]
    ON [dbo].[fa_notaCreDeb]([IdEmpresa] ASC, [CodDocumentoTipo] ASC, [Serie1] ASC, [Serie2] ASC, [NumNota_Impresa] ASC);

