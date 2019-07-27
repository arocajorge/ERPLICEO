CREATE TABLE [EntidadRegulatoria].[ATS_exportaciones] (
    [IdEmpresa]           INT             NOT NULL,
    [IdPeriodo]           INT             NOT NULL,
    [Secuencia]           INT             NOT NULL,
    [tpIdClienteEx]       VARCHAR (5)     NULL,
    [idClienteEx]         VARCHAR (50)    NULL,
    [parteRel]            VARCHAR (5)     NULL,
    [tipoRegi]            VARCHAR (5)     NULL,
    [paisEfecPagoGen]     VARCHAR (5)     NULL,
    [paisEfecExp]         VARCHAR (5)     NULL,
    [exportacionDe]       VARCHAR (5)     NULL,
    [tipoComprobante]     VARCHAR (5)     NULL,
    [fechaEmbarque]       DATE            NULL,
    [valorFOB]            NUMERIC (18, 2) NULL,
    [valorFOBComprobante] NUMERIC (18, 2) NULL,
    [establecimiento]     VARCHAR (3)     NULL,
    [puntoEmision]        VARCHAR (3)     NULL,
    [secuencial]          VARCHAR (15)    NULL,
    [autorizacion]        VARCHAR (50)    NULL,
    [fechaEmision]        DATE            NULL,
    [denoExpCli]          VARCHAR (500)   NULL,
    [IdSucursal]          INT             NULL,
    CONSTRAINT [PK_ATS_exportaciones] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC, [Secuencia] ASC)
);



