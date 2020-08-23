CREATE TABLE [dbo].[aca_AnioLectivoConductaEquivalencia] (
    [IdEmpresa]               INT             NOT NULL,
    [IdAnio]                  INT             NOT NULL,
    [Secuencia]               INT             NOT NULL,
    [Letra]                   VARCHAR (10)    NOT NULL,
    [Calificacion]            NUMERIC (18, 2) NOT NULL,
    [Equivalencia]            VARCHAR (50)    NULL,
    [DescripcionEquivalencia] VARCHAR (MAX)   NULL,
    [IngresaMotivo]           BIT             NULL,
    [IngresaProfesor]         BIT             NULL,
    [IngresaInspector]        BIT             NULL,
    CONSTRAINT [PK_aca_AnioLectivoConductaEquivalencia] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_aca_AnioLectivoConductaEquivalencia_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio])
);







