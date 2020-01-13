﻿CREATE TABLE [dbo].[cp_orden_giro] (
    [IdEmpresa]                     INT            NOT NULL,
    [IdCbteCble_Ogiro]              NUMERIC (18)   NOT NULL,
    [IdTipoCbte_Ogiro]              INT            NOT NULL,
    [IdOrden_giro_Tipo]             VARCHAR (5)    NOT NULL,
    [IdProveedor]                   NUMERIC (18)   NOT NULL,
    [SecuenciaProveedor]            INT            NULL,
    [co_fechaOg]                    DATETIME       NOT NULL,
    [co_serie]                      VARCHAR (10)   NOT NULL,
    [co_factura]                    VARCHAR (50)   NOT NULL,
    [co_FechaFactura]               DATETIME       NOT NULL,
    [co_FechaContabilizacion]       DATETIME       NULL,
    [co_FechaFactura_vct]           DATETIME       NOT NULL,
    [co_plazo]                      INT            NOT NULL,
    [co_observacion]                VARCHAR (1000) NULL,
    [co_subtotal_iva]               FLOAT (53)     NOT NULL,
    [co_subtotal_siniva]            FLOAT (53)     NOT NULL,
    [co_baseImponible]              FLOAT (53)     NOT NULL,
    [co_Por_iva]                    FLOAT (53)     NOT NULL,
    [co_valoriva]                   FLOAT (53)     NOT NULL,
    [IdCod_ICE]                     INT            NULL,
    [co_total]                      FLOAT (53)     NOT NULL,
    [co_valorpagar]                 FLOAT (53)     NOT NULL,
    [co_vaCoa]                      CHAR (1)       NOT NULL,
    [IdIden_credito]                INT            NULL,
    [IdCod_101]                     INT            NULL,
    [IdTipoServicio]                VARCHAR (5)    NULL,
    [IdUsuario]                     VARCHAR (20)   NULL,
    [Fecha_Transac]                 DATETIME       NULL,
    [Estado]                        VARCHAR (2)    NOT NULL,
    [IdUsuarioUltMod]               VARCHAR (20)   NULL,
    [Fecha_UltMod]                  DATETIME       NULL,
    [IdUsuarioUltAnu]               VARCHAR (20)   NULL,
    [MotivoAnu]                     VARCHAR (150)  NULL,
    [Fecha_UltAnu]                  DATETIME       NULL,
    [IdSucursal]                    INT            NOT NULL,
    [IdBodega]                      INT            NOT NULL,
    [IdSucursal_cxp]                INT            NULL,
    [PagoLocExt]                    VARCHAR (3)    NULL,
    [PaisPago]                      VARCHAR (5)    NULL,
    [ConvenioTributacion]           VARCHAR (2)    NULL,
    [PagoSujetoRetencion]           VARCHAR (2)    NULL,
    [BseImpNoObjDeIva]              FLOAT (53)     NULL,
    [fecha_autorizacion]            DATETIME       NULL,
    [Num_Autorizacion]              VARCHAR (50)   NULL,
    [Num_Autorizacion_Imprenta]     VARCHAR (50)   NULL,
    [cp_es_comprobante_electronico] BIT            NULL,
    [Tipodoc_a_Modificar]           VARCHAR (2)    NULL,
    [estable_a_Modificar]           VARCHAR (3)    NULL,
    [ptoEmi_a_Modificar]            VARCHAR (3)    NULL,
    [num_docu_Modificar]            VARCHAR (50)   NULL,
    [aut_doc_Modificar]             VARCHAR (100)  NULL,
    [IdTipoMovi]                    INT            NULL,
    [aprobada_enviar_sri]           BIT            NOT NULL,
    [IdPuntoVta]                    INT            NULL,
    [MueveInventario]               BIT            NULL,
    CONSTRAINT [PK_cp_orden_giro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCbteCble_Ogiro] ASC, [IdTipoCbte_Ogiro] ASC),
    CONSTRAINT [FK_cp_orden_giro_caj_Caja_Movimiento_Tipo] FOREIGN KEY ([IdEmpresa], [IdTipoMovi]) REFERENCES [dbo].[caj_Caja_Movimiento_Tipo] ([IdEmpresa], [IdTipoMovi]),
    CONSTRAINT [FK_cp_orden_giro_cp_codigo_SRI] FOREIGN KEY ([IdCod_ICE]) REFERENCES [dbo].[cp_codigo_SRI] ([IdCodigo_SRI]),
    CONSTRAINT [FK_cp_orden_giro_cp_codigo_SRI1] FOREIGN KEY ([IdIden_credito]) REFERENCES [dbo].[cp_codigo_SRI] ([IdCodigo_SRI]),
    CONSTRAINT [FK_cp_orden_giro_cp_codigo_SRI2] FOREIGN KEY ([IdCod_101]) REFERENCES [dbo].[cp_codigo_SRI] ([IdCodigo_SRI]),
    CONSTRAINT [FK_cp_orden_giro_cp_pais_sri] FOREIGN KEY ([PaisPago]) REFERENCES [dbo].[cp_pais_sri] ([Codigo]),
    CONSTRAINT [FK_cp_orden_giro_cp_proveedor] FOREIGN KEY ([IdEmpresa], [IdProveedor]) REFERENCES [dbo].[cp_proveedor] ([IdEmpresa], [IdProveedor]),
    CONSTRAINT [FK_cp_orden_giro_cp_proveedor_detalle] FOREIGN KEY ([IdEmpresa], [IdProveedor], [SecuenciaProveedor]) REFERENCES [dbo].[cp_proveedor_detalle] ([IdEmpresa], [IdProveedor], [Secuencia]),
    CONSTRAINT [FK_cp_orden_giro_ct_cbtecble] FOREIGN KEY ([IdEmpresa], [IdTipoCbte_Ogiro], [IdCbteCble_Ogiro]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_cp_orden_giro_fa_PuntoVta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdPuntoVta]) REFERENCES [dbo].[fa_PuntoVta] ([IdEmpresa], [IdSucursal], [IdPuntoVta]),
    CONSTRAINT [FK_cp_orden_giro_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal]),
    CONSTRAINT [FK_cp_orden_giro_tb_sucursal1] FOREIGN KEY ([IdEmpresa], [IdSucursal_cxp]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);













