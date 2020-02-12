CREATE TABLE [Academico].[cxc_SPCXC_004] (
    [IdEmpresa]     INT             NOT NULL,
    [IdAlumno]      NUMERIC (18)    NOT NULL,
    [IdAnio]        INT             NOT NULL,
    [IdUsuario]     VARCHAR (200)   NOT NULL,
    [NomAnio]       VARCHAR (100)   NOT NULL,
    [CodigoAlumno]  VARCHAR (50)    NOT NULL,
    [NombreAlumno]  VARCHAR (1000)  NOT NULL,
    [IdJornada]     INT             NULL,
    [NombreJornada] VARCHAR (500)   NULL,
    [SaldoDeudor]   NUMERIC (18, 2) NOT NULL,
    [SaldoAcreedor] NUMERIC (18, 2) NOT NULL,
    [SaldoFinal]    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_cxc_SPCXC_004] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAlumno] ASC, [IdAnio] ASC, [IdUsuario] ASC)
);

