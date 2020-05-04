CREATE TABLE [dbo].[aca_AlumnoRetiro] (
    [IdEmpresa]          INT           NOT NULL,
    [IdRetiro]           NUMERIC (18)  NOT NULL,
    [IdMatricula]        NUMERIC (18)  NOT NULL,
    [Fecha]              DATE          NOT NULL,
    [Observacion]        VARCHAR (MAX) NOT NULL,
    [IdCatalogoESTALU]   INT           NOT NULL,
    [IdUsuarioCreacion]  VARCHAR (200) NULL,
    [FechaCreacion]      DATETIME      NULL,
    [IdUsuarioAnulacion] VARCHAR (200) NULL,
    [FechaAnulacion]     DATETIME      NULL,
    [MotivoAnulacion]    VARCHAR (MAX) NULL,
    [Estado]             BIT           NOT NULL,
    CONSTRAINT [PK_aca_AlumnoRetiro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRetiro] ASC),
    CONSTRAINT [FK_aca_AlumnoRetiro_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);









