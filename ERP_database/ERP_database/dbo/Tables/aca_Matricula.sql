CREATE TABLE [dbo].[aca_Matricula] (
    [IdEmpresa]             INT           NOT NULL,
    [IdMatricula]           NUMERIC (18)  NOT NULL,
    [Codigo]                VARCHAR (200) NULL,
    [IdAlumno]              NUMERIC (18)  NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [IdSede]                INT           NOT NULL,
    [IdNivel]               INT           NOT NULL,
    [IdJornada]             INT           NOT NULL,
    [IdCurso]               INT           NOT NULL,
    [IdParalelo]            INT           NOT NULL,
    [IdPersonaF]            NUMERIC (18)  NOT NULL,
    [IdPersonaR]            NUMERIC (18)  NOT NULL,
    [IdPlantilla]           INT           NOT NULL,
    [IdMecanismo]           NUMERIC (18)  NOT NULL,
    [IdCatalogoESTMAT]      INT           NULL,
    [Fecha]                 DATE          NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    [IdEmpresa_rol]         INT           NULL,
    [IdEmpleado]            NUMERIC (18)  NULL,
    CONSTRAINT [PK_aca_Matricula] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC),
    CONSTRAINT [FK_aca_Matricula_aca_alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_Matricula_aca_AnioLectivo_Curso_Paralelo] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso], [IdParalelo]) REFERENCES [dbo].[aca_AnioLectivo_Curso_Paralelo] ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso], [IdParalelo]),
    CONSTRAINT [FK_aca_Matricula_aca_Catalogo] FOREIGN KEY ([IdCatalogoESTMAT]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_Matricula_aca_MecanismoDePago] FOREIGN KEY ([IdEmpresa], [IdMecanismo]) REFERENCES [dbo].[aca_MecanismoDePago] ([IdEmpresa], [IdMecanismo]),
    CONSTRAINT [FK_aca_Matricula_aca_Plantilla] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdPlantilla]) REFERENCES [dbo].[aca_Plantilla] ([IdEmpresa], [IdAnio], [IdPlantilla]),
    CONSTRAINT [FK_aca_Matricula_ro_empleado] FOREIGN KEY ([IdEmpresa_rol], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);














GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20200507-113016]
    ON [dbo].[aca_Matricula]([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC, [IdCurso] ASC, [IdParalelo] ASC);

