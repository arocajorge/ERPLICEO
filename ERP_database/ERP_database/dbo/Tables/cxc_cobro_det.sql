CREATE TABLE [dbo].[cxc_cobro_det] (
    [IdEmpresa]          INT          NOT NULL,
    [IdSucursal]         INT          NOT NULL,
    [IdCobro]            NUMERIC (18) NOT NULL,
    [secuencial]         INT          NOT NULL,
    [dc_TipoDocumento]   VARCHAR (20) NULL,
    [IdBodega_Cbte]      INT          NULL,
    [IdCbte_vta_nota]    NUMERIC (18) NOT NULL,
    [dc_ValorPago]       FLOAT (53)   NOT NULL,
    [dc_ValorProntoPago] FLOAT (53)   NULL,
    [IdUsuario]          VARCHAR (20) NULL,
    [Fecha_Transac]      DATETIME     NULL,
    [IdUsuarioUltMod]    VARCHAR (20) NULL,
    [Fecha_UltMod]       DATETIME     NULL,
    [IdUsuarioUltAnu]    VARCHAR (20) NULL,
    [Fecha_UltAnu]       DATETIME     NULL,
    [nom_pc]             VARCHAR (50) NULL,
    [ip]                 VARCHAR (50) NULL,
    [estado]             VARCHAR (1)  NULL,
    [IdCobro_tipo]       VARCHAR (20) NOT NULL,
    [IdNotaCredito]      NUMERIC (18) NULL,
    CONSTRAINT [PK_cxc_cobro_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdCobro] ASC, [secuencial] ASC),
    CONSTRAINT [FK_cxc_cobro_det_cxc_cobro] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdCobro]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro]),
    CONSTRAINT [FK_cxc_cobro_det_cxc_cobro_tipo] FOREIGN KEY ([IdCobro_tipo]) REFERENCES [dbo].[cxc_cobro_tipo] ([IdCobro_tipo]),
    CONSTRAINT [FK_cxc_cobro_det_fa_notaCreDeb] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega_Cbte], [IdNotaCredito]) REFERENCES [dbo].[fa_notaCreDeb] ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota])
);



