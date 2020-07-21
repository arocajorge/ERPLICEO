CREATE TABLE [dbo].[cxc_SeguimientoCartera] (
    [IdEmpresa]             INT           NOT NULL,
    [IdSeguimiento]         INT           NOT NULL,
    [IdAlumno]              NUMERIC (18)  NOT NULL,
    [Fecha]                 DATETIME      NOT NULL,
    [Observacion]           VARCHAR (MAX) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_cxc_SeguimientoCartera] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSeguimiento] ASC),
    CONSTRAINT [FK_cxc_SeguimientoCartera_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno])
);

