CREATE TABLE [dbo].[fa_MotivoTraslado] (
    [IdEmpresa]             INT           NOT NULL,
    [IdMotivoTraslado]      INT           NOT NULL,
    [tr_Descripcion]        VARCHAR (MAX) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_fa_MotivoTraslado] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMotivoTraslado] ASC)
);

