CREATE TABLE [dbo].[fa_notaCreDeb_x_fa_factura_NotaDeb] (
    [IdEmpresa_nt]              INT           NOT NULL,
    [IdSucursal_nt]             INT           NOT NULL,
    [IdBodega_nt]               INT           NOT NULL,
    [IdNota_nt]                 NUMERIC (18)  NOT NULL,
    [secuencia]                 INT           NOT NULL,
    [IdEmpresa_fac_nd_doc_mod]  INT           NOT NULL,
    [IdSucursal_fac_nd_doc_mod] INT           NOT NULL,
    [IdBodega_fac_nd_doc_mod]   INT           NOT NULL,
    [IdCbteVta_fac_nd_doc_mod]  NUMERIC (18)  NOT NULL,
    [vt_tipoDoc]                VARCHAR (20)  NOT NULL,
    [NumDocumento]              VARCHAR (300) NULL,
    [Valor_Aplicado]            FLOAT (53)    NOT NULL,
    [fecha_cruce]               DATE          NOT NULL,
    CONSTRAINT [PK_fa_notaCreDeb_x_fa_factura_NotaDeb] PRIMARY KEY CLUSTERED ([IdEmpresa_nt] ASC, [IdSucursal_nt] ASC, [IdBodega_nt] ASC, [IdNota_nt] ASC, [secuencia] ASC)
);



