CREATE TABLE [dbo].[cp_retencion_det] (
    [IdEmpresa]           INT          NOT NULL,
    [IdRetencion]         NUMERIC (18) NOT NULL,
    [Idsecuencia]         INT          NOT NULL,
    [re_tipoRet]          CHAR (3)     NOT NULL,
    [re_baseRetencion]    FLOAT (53)   NOT NULL,
    [IdCodigo_SRI]        INT          NOT NULL,
    [re_Codigo_impuesto]  VARCHAR (50) NOT NULL,
    [re_Porcen_retencion] FLOAT (53)   NOT NULL,
    [re_valor_retencion]  FLOAT (53)   NOT NULL,
    [re_estado]           CHAR (1)     NOT NULL,
    [IdUsuario]           VARCHAR (20) NULL,
    [Fecha_Transac]       DATETIME     NULL,
    [IdUsuarioUltMod]     VARCHAR (20) NULL,
    [Fecha_UltMod]        DATETIME     NULL,
    [IdUsuarioUltAnu]     VARCHAR (20) NULL,
    [Fecha_UltAnu]        DATETIME     NULL,
    [nom_pc]              VARCHAR (50) NULL,
    [ip]                  VARCHAR (25) NULL,
    CONSTRAINT [PK_cp_retencion_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRetencion] ASC, [Idsecuencia] ASC),
    CONSTRAINT [FK_cp_retencion_det_cp_codigo_SRI] FOREIGN KEY ([IdCodigo_SRI]) REFERENCES [dbo].[cp_codigo_SRI] ([IdCodigo_SRI]),
    CONSTRAINT [FK_cp_retencion_det_cp_retencion] FOREIGN KEY ([IdEmpresa], [IdRetencion]) REFERENCES [dbo].[cp_retencion] ([IdEmpresa], [IdRetencion])
);

