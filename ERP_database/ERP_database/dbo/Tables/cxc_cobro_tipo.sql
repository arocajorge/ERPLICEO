CREATE TABLE [dbo].[cxc_cobro_tipo] (
    [IdCobro_tipo]         VARCHAR (20) NOT NULL,
    [tc_descripcion]       VARCHAR (50) NOT NULL,
    [Estado]               VARCHAR (1)  NOT NULL,
    [tc_abreviatura]       VARCHAR (20) NULL,
    [tc_Tomar_Cta_Cble_De] VARCHAR (20) NOT NULL,
    [ESRetenIVA]           CHAR (1)     NOT NULL,
    [ESRetenFTE]           CHAR (1)     NOT NULL,
    [PorcentajeRet]        FLOAT (53)   NOT NULL,
    [IdUsuario]            VARCHAR (20) NULL,
    [Fecha_Transac]        DATETIME     NULL,
    [IdUsuarioUltMod]      VARCHAR (20) NULL,
    [Fecha_UltMod]         DATETIME     NULL,
    [IdUsuarioUltAnu]      VARCHAR (20) NULL,
    [Fecha_UltAnu]         DATETIME     NULL,
    [IdMotivo_tipo_cobro]  VARCHAR (15) NULL,
    [EsTarjetaCredito]     BIT          NOT NULL,
    [SeDeposita]           BIT          NOT NULL,
    [PorcentajeDescuento]  FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_cxc_cobro_tipo] PRIMARY KEY CLUSTERED ([IdCobro_tipo] ASC),
    CONSTRAINT [FK_cxc_cobro_tipo_cxc_cobro_tipo_motivo] FOREIGN KEY ([IdMotivo_tipo_cobro]) REFERENCES [dbo].[cxc_cobro_tipo_motivo] ([IdMotivo_tipo_cobro])
);





