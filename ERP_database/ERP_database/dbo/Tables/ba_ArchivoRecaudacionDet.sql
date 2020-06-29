CREATE TABLE [dbo].[ba_ArchivoRecaudacionDet] (
    [IdEmpresa]       INT          NOT NULL,
    [IdArchivo]       NUMERIC (18) NOT NULL,
    [Secuencia]       INT          NOT NULL,
    [IdMatricula]     NUMERIC (18) NULL,
    [IdAlumno]        NUMERIC (18) NOT NULL,
    [Valor]           FLOAT (53)   NOT NULL,
    [ValorProntoPago] FLOAT (53)   NOT NULL,
    [FechaProceso]    DATETIME     NULL,
    [FechaProntoPago] DATETIME     NULL,
    CONSTRAINT [PK_ba_ArchivoRecaudacionDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdArchivo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_ArchivoRecaudacionDet_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_ba_ArchivoRecaudacionDet_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);





