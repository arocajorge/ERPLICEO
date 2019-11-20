CREATE TABLE [dbo].[aca_Alumno] (
    [IdEmpresa]             INT            NOT NULL,
    [IdAlumno]              NUMERIC (18)   NOT NULL,
    [Codigo]                VARCHAR (200)  NULL,
    [IdPersona]             NUMERIC (18)   NOT NULL,
    [Direccion]             VARCHAR (1000) NULL,
    [Celular]               VARCHAR (500)  NULL,
    [Correo]                VARCHAR (500)  NULL,
    [Estado]                BIT            NULL,
    [IdCatalogoESTMAT]      INT            NOT NULL,
    [IdCurso]               INT            NULL,
    [IdCatalogoESTALU]      INT            NOT NULL,
    [FechaIngreso]          DATE           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200)  NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (200)  NULL,
    [FechaModificacion]     DATETIME       NULL,
    [IdUsuarioAnulacion]    VARCHAR (200)  NULL,
    [FechaAnulacion]        DATETIME       NULL,
    [MotivoAnulacion]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_aca_alumno] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAlumno] ASC),
    CONSTRAINT [FK_aca_alumno_aca_Catalogo1] FOREIGN KEY ([IdCatalogoESTMAT]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_alumno_aca_Catalogo2] FOREIGN KEY ([IdCatalogoESTALU]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_alumno_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCurso]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso])
);



