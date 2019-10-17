CREATE TABLE [dbo].[aca_NivelAcademico] (
    [IdEmpresa]             INT           NOT NULL,
    [IdNivel]               INT           NOT NULL,
    [NomNivel]              VARCHAR (500) NOT NULL,
    [Orden]                 INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Seccion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNivel] ASC)
);

