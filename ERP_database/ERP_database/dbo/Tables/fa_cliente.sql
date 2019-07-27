CREATE TABLE [dbo].[fa_cliente] (
    [IdEmpresa]              INT          NOT NULL,
    [IdCliente]              NUMERIC (18) NOT NULL,
    [Codigo]                 VARCHAR (50) NULL,
    [IdPersona]              NUMERIC (18) NOT NULL,
    [Idtipo_cliente]         INT          NOT NULL,
    [IdTipoCredito]          VARCHAR (20) NOT NULL,
    [cl_plazo]               INT          NOT NULL,
    [cl_Cupo]                FLOAT (53)   NOT NULL,
    [IdUsuario]              VARCHAR (20) NULL,
    [Fecha_Transac]          DATETIME     NULL,
    [IdUsuarioUltMod]        VARCHAR (20) NULL,
    [Fecha_UltMod]           DATETIME     NULL,
    [IdUsuarioUltAnu]        VARCHAR (20) NULL,
    [Fecha_UltAnu]           DATETIME     NULL,
    [Estado]                 VARCHAR (50) NOT NULL,
    [IdCtaCble_cxc_Credito]  VARCHAR (20) NULL,
    [es_empresa_relacionada] BIT          NOT NULL,
    [FormaPago]              VARCHAR (2)  NULL,
    [EsClienteExportador]    BIT          NOT NULL,
    [IdNivel]                INT          NOT NULL,
    CONSTRAINT [PK_fa_cliente] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCliente] ASC),
    CONSTRAINT [FK_fa_cliente_ct_plancta2] FOREIGN KEY ([IdEmpresa], [IdCtaCble_cxc_Credito]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_fa_cliente_fa_cliente_tipo] FOREIGN KEY ([IdEmpresa], [Idtipo_cliente]) REFERENCES [dbo].[fa_cliente_tipo] ([IdEmpresa], [Idtipo_cliente]),
    CONSTRAINT [FK_fa_cliente_fa_forma_pago] FOREIGN KEY ([FormaPago]) REFERENCES [dbo].[fa_formaPago] ([IdFormaPago]),
    CONSTRAINT [FK_fa_cliente_fa_NivelDescuento] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[fa_NivelDescuento] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_fa_cliente_fa_TerminoPago] FOREIGN KEY ([IdTipoCredito]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago]),
    CONSTRAINT [FK_fa_cliente_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_fa_cliente_tb_persona] FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[tb_persona] ([IdPersona])
);











