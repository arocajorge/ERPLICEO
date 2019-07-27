CREATE TABLE [EntidadRegulatoria].[ATS_comprobantes_anulados] (
    [IdEmpresa]        INT          NOT NULL,
    [IdPeriodo]        INT          NOT NULL,
    [Secuencia]        INT          NOT NULL,
    [tipoComprobante]  VARCHAR (2)  NOT NULL,
    [Establecimiento]  VARCHAR (3)  NOT NULL,
    [puntoEmision]     VARCHAR (3)  NOT NULL,
    [secuencialInicio] VARCHAR (9)  NOT NULL,
    [secuencialFin]    VARCHAR (9)  NOT NULL,
    [Autorización]     VARCHAR (49) NOT NULL,
    [IdSucursal]       INT          NULL,
    CONSTRAINT [PK_comprobantes_anulados] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC, [Secuencia] ASC)
);



