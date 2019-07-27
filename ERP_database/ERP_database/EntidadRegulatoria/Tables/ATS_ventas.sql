CREATE TABLE [EntidadRegulatoria].[ATS_ventas] (
    [IdEmpresa]          INT             NOT NULL,
    [IdPeriodo]          INT             NOT NULL,
    [Secuencia]          INT             NOT NULL,
    [tpIdCliente]        VARCHAR (2)     NOT NULL,
    [idCliente]          VARCHAR (13)    NOT NULL,
    [parteRel]           VARCHAR (2)     NOT NULL,
    [tipoCliente]        VARCHAR (2)     NOT NULL,
    [DenoCli]            VARCHAR (500)   NOT NULL,
    [tipoComprobante]    VARCHAR (3)     NOT NULL,
    [tipoEm]             VARCHAR (1)     NOT NULL,
    [numeroComprobantes] INT             NOT NULL,
    [baseNoGraIva]       NUMERIC (20, 2) NOT NULL,
    [baseImponible]      NUMERIC (20, 2) NOT NULL,
    [baseImpGrav]        NUMERIC (20, 2) NOT NULL,
    [montoIva]           NUMERIC (20, 2) NOT NULL,
    [montoIce]           NUMERIC (20, 2) NOT NULL,
    [valorRetIva]        NUMERIC (20, 2) NOT NULL,
    [valorRetRenta]      NUMERIC (20, 2) NOT NULL,
    [formaPago]          VARCHAR (2)     NULL,
    [codEstab]           VARCHAR (3)     NOT NULL,
    [ventasEstab]        NUMERIC (20, 2) NOT NULL,
    [ivaComp]            NUMERIC (20, 2) NOT NULL,
    [IdSucursal]         INT             NULL,
    CONSTRAINT [PK_ventas] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC, [Secuencia] ASC)
);





