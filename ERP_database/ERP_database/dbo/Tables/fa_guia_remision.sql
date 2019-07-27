CREATE TABLE [dbo].[fa_guia_remision] (
    [IdEmpresa]              INT           NOT NULL,
    [IdSucursal]             INT           NOT NULL,
    [IdBodega]               INT           NOT NULL,
    [IdGuiaRemision]         NUMERIC (18)  NOT NULL,
    [IdPuntoVta]             INT           NOT NULL,
    [CodGuiaRemision]        VARCHAR (50)  NULL,
    [CodDocumentoTipo]       VARCHAR (20)  NOT NULL,
    [Serie1]                 VARCHAR (3)   NULL,
    [Serie2]                 VARCHAR (3)   NULL,
    [NumGuia_Preimpresa]     VARCHAR (20)  NULL,
    [NUAutorizacion]         VARCHAR (200) NULL,
    [Fecha_Autorizacion]     DATETIME      NULL,
    [IdCliente]              NUMERIC (18)  NOT NULL,
    [IdTransportista]        NUMERIC (18)  NOT NULL,
    [gi_fecha]               DATETIME      NOT NULL,
    [gi_plazo]               NUMERIC (10)  NULL,
    [gi_fech_venc]           DATETIME      NULL,
    [gi_Observacion]         VARCHAR (MAX) NULL,
    [gi_FechaFinTraslado]    DATE          NOT NULL,
    [gi_FechaInicioTraslado] DATE          NOT NULL,
    [placa]                  VARCHAR (50)  NOT NULL,
    [Direccion_Origen]       VARCHAR (MAX) NOT NULL,
    [Direccion_Destino]      VARCHAR (MAX) NULL,
    [Estado]                 BIT           NOT NULL,
    [aprobada_enviar_sri]    BIT           NOT NULL,
    [Generado]               BIT           NULL,
    [IdMotivoTraslado]       INT           NOT NULL,
    [IdUsuarioCreacion]      VARCHAR (50)  NULL,
    [FechaCreacion]          DATETIME      NULL,
    [IdUsuarioModificacion]  VARCHAR (50)  NULL,
    [FechaModificacion]      DATETIME      NULL,
    [IdUsuarioAnulacion]     VARCHAR (50)  NULL,
    [FechaAnulacion]         DATETIME      NULL,
    [MotivoAnulacion]        VARCHAR (MAX) NULL,
    CONSTRAINT [PK_fa_guia_remision] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdGuiaRemision] ASC),
    CONSTRAINT [FK_fa_guia_remision_fa_cliente] FOREIGN KEY ([IdEmpresa], [IdCliente]) REFERENCES [dbo].[fa_cliente] ([IdEmpresa], [IdCliente]),
    CONSTRAINT [FK_fa_guia_remision_fa_MotivoTraslado] FOREIGN KEY ([IdEmpresa], [IdMotivoTraslado]) REFERENCES [dbo].[fa_MotivoTraslado] ([IdEmpresa], [IdMotivoTraslado]),
    CONSTRAINT [FK_fa_guia_remision_fa_PuntoVta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdPuntoVta]) REFERENCES [dbo].[fa_PuntoVta] ([IdEmpresa], [IdSucursal], [IdPuntoVta]),
    CONSTRAINT [FK_fa_guia_remision_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega]),
    CONSTRAINT [FK_fa_guia_remision_tb_sis_Documento_Tipo_Talonario] FOREIGN KEY ([IdEmpresa], [CodDocumentoTipo], [Serie2], [Serie1], [NumGuia_Preimpresa]) REFERENCES [dbo].[tb_sis_Documento_Tipo_Talonario] ([IdEmpresa], [CodDocumentoTipo], [PuntoEmision], [Establecimiento], [NumDocumento]),
    CONSTRAINT [FK_fa_guia_remision_tb_transportista] FOREIGN KEY ([IdEmpresa], [IdTransportista]) REFERENCES [dbo].[tb_transportista] ([IdEmpresa], [IdTransportista])
);











