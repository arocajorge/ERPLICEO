CREATE TABLE [dbo].[fa_notaCreDeb_resumen] (
    [IdEmpresa]              INT             NOT NULL,
    [IdSucursal]             INT             NOT NULL,
    [IdBodega]               INT             NOT NULL,
    [IdNota]                 NUMERIC (18)    NOT NULL,
    [SubtotalIVASinDscto]    NUMERIC (18, 2) NOT NULL,
    [SubtotalSinIVASinDscto] NUMERIC (18, 2) NOT NULL,
    [SubtotalSinDscto]       NUMERIC (18, 2) NOT NULL,
    [Descuento]              NUMERIC (18, 2) NOT NULL,
    [SubtotalIVAConDscto]    NUMERIC (18, 2) NOT NULL,
    [SubtotalSinIVAConDscto] NUMERIC (18, 2) NOT NULL,
    [SubtotalConDscto]       NUMERIC (18, 2) NOT NULL,
    [ValorIVA]               NUMERIC (18, 2) NOT NULL,
    [Total]                  NUMERIC (18, 2) NOT NULL,
    [PorIva]                 NUMERIC (18, 2) NULL,
    [IdCod_Impuesto_IVA]     VARCHAR (25)    NULL,
    [IdAnio]                 INT             NULL,
    [IdMatricula]            NUMERIC (18)    NULL,
    CONSTRAINT [PK_fa_notaCreDeb_resumen] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdNota] ASC),
    CONSTRAINT [FK_fa_notaCreDeb_resumen_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_fa_notaCreDeb_resumen_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula]),
    CONSTRAINT [FK_fa_notaCreDeb_resumen_fa_notaCreDeb] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota]) REFERENCES [dbo].[fa_notaCreDeb] ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota]),
    CONSTRAINT [FK_fa_notaCreDeb_resumen_tb_sis_Impuesto] FOREIGN KEY ([IdCod_Impuesto_IVA]) REFERENCES [dbo].[tb_sis_Impuesto] ([IdCod_Impuesto])
);



