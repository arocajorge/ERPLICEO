CREATE TABLE [dbo].[aca_Paralelo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdParalelo]            INT           NOT NULL,
    [CodigoParalelo]        VARCHAR (10)  NOT NULL,
    [NomParalelo]           VARCHAR (500) NOT NULL,
    [OrdenParalelo]         INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Paralelo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdParalelo] ASC)
);

