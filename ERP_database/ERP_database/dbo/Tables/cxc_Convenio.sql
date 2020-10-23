CREATE TABLE [dbo].[cxc_Convenio] (
    [IdEmpresa]             INT           NOT NULL,
    [IdConvenio]            INT           NOT NULL,
    [IdAlumno]              NUMERIC (18)  NOT NULL,
    [IdMatricula]           NUMERIC (18)  NULL,
    [IdPersonaConvenio]     NUMERIC (18)  NOT NULL,
    [Fecha]                 DATETIME      NOT NULL,
    [FechaPrimerPago]       DATETIME      NOT NULL,
    [Valor]                 FLOAT (53)    NOT NULL,
    [NumCuotas]             INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_cxc_Convenio] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConvenio] ASC),
    CONSTRAINT [FK_cxc_Convenio_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_cxc_Convenio_tb_persona] FOREIGN KEY ([IdPersonaConvenio]) REFERENCES [dbo].[tb_persona] ([IdPersona])
);

