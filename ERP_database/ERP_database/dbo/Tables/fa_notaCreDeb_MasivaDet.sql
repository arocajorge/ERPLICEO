CREATE TABLE [dbo].[fa_notaCreDeb_MasivaDet] (
    [IdEmpresa]          INT           NOT NULL,
    [IdNCMasivo]         NUMERIC (18)  NOT NULL,
    [Secuencia]          INT           NOT NULL,
    [IdAlumno]           NUMERIC (18)  NOT NULL,
    [Subtotal]           FLOAT (53)    NOT NULL,
    [IVA]                FLOAT (53)    NOT NULL,
    [vt_por_iva]         FLOAT (53)    NOT NULL,
    [IdCod_Impuesto_Iva] VARCHAR (25)  NOT NULL,
    [Total]              FLOAT (53)    NOT NULL,
    [ObservacionDet]     VARCHAR (MAX) NULL,
    [IdSucursal]         INT           NULL,
    [IdBodega]           INT           NULL,
    [IdNota]             NUMERIC (18)  NULL,
    CONSTRAINT [PK_fa_notaCreDeb_MasivaDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNCMasivo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_notaCreDeb_MasivaDet_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_fa_notaCreDeb_MasivaDet_fa_notaCreDeb] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota]) REFERENCES [dbo].[fa_notaCreDeb] ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota])
);

