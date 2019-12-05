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
    [LugarNacimiento]       VARCHAR (300)  NULL,
    [IdPais]                VARCHAR (10)   NULL,
    [Cod_Region]            VARCHAR (10)   NULL,
    [IdProvincia]           VARCHAR (25)   NULL,
    [IdCiudad]              VARCHAR (25)   NULL,
    [IdParroquia]           VARCHAR (25)   NULL,
    [Sector]                VARCHAR (500)  NULL,
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
    CONSTRAINT [FK_aca_alumno_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCurso]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso]),
    CONSTRAINT [FK_aca_Alumno_tb_ciudad] FOREIGN KEY ([IdCiudad]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Alumno_tb_pais] FOREIGN KEY ([IdPais]) REFERENCES [dbo].[tb_pais] ([IdPais]),
    CONSTRAINT [FK_aca_Alumno_tb_parroquia] FOREIGN KEY ([IdParroquia]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Alumno_tb_provincia] FOREIGN KEY ([IdProvincia]) REFERENCES [dbo].[tb_provincia] ([IdProvincia]),
    CONSTRAINT [FK_aca_Alumno_tb_region] FOREIGN KEY ([Cod_Region]) REFERENCES [dbo].[tb_region] ([Cod_Region])
);





