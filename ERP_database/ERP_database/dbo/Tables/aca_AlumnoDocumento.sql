CREATE TABLE [dbo].[aca_AlumnoDocumento] (
    [IdEmpresa]   INT          NOT NULL,
    [IdAlumno]    NUMERIC (18) NOT NULL,
    [IdDocumento] INT          NOT NULL,
    [EnArchivo]   BIT          NOT NULL,
    CONSTRAINT [PK_aca_AlumnoDocumento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAlumno] ASC, [IdDocumento] ASC),
    CONSTRAINT [FK_aca_AlumnoDocumento_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_AlumnoDocumento_aca_Documento] FOREIGN KEY ([IdEmpresa], [IdDocumento]) REFERENCES [dbo].[aca_Documento] ([IdEmpresa], [IdDocumento])
);

