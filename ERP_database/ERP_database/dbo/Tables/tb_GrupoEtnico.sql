CREATE TABLE [dbo].[tb_GrupoEtnico] (
    [IdGrupoEtnico]         INT           NOT NULL,
    [NomGrupoEtnico]        VARCHAR (500) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_tb_GrupoEtnico] PRIMARY KEY CLUSTERED ([IdGrupoEtnico] ASC)
);

