CREATE TABLE [dbo].[Af_Area] (
    [IdEmpresa]             INT           NOT NULL,
    [IdArea]                NUMERIC (18)  NOT NULL,
    [Descripcion]           VARCHAR (MAX) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Af_Area] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdArea] ASC)
);

