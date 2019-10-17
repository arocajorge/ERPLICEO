CREATE TABLE [dbo].[aca_Jornada] (
    [IdEmpresa]             INT           NOT NULL,
    [IdJornada]             INT           NOT NULL,
    [NomJornada]            VARCHAR (500) NOT NULL,
    [OrdenJornada]          INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Jornada_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdJornada] ASC)
);



