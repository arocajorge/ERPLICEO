CREATE TABLE [dbo].[cxc_Pagare] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPagare]              INT           NOT NULL,
    [IdAlumno]              NUMERIC (18)  NOT NULL,
    [IdMatricula]           NUMERIC (18)  NULL,
    [IdPersonaPagare]       NUMERIC (18)  NOT NULL,
    [FechaAPagar]           DATETIME      NOT NULL,
    [Valor]                 FLOAT (53)    NOT NULL,
    [Estado]                BIT           NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_cxc_Pagare] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPagare] ASC),
    CONSTRAINT [FK_cxc_Pagare_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_cxc_Pagare_tb_persona] FOREIGN KEY ([IdPersonaPagare]) REFERENCES [dbo].[tb_persona] ([IdPersona])
);

