CREATE TABLE [dbo].[ro_Nomina_Tipo] (
    [IdEmpresa]        INT           NOT NULL,
    [IdNomina_Tipo]    INT           NOT NULL,
    [Descripcion]      VARCHAR (50)  NOT NULL,
    [IdUsuario]        VARCHAR (20)  NULL,
    [IdUsuarioAnu]     VARCHAR (20)  NULL,
    [MotivoAnu]        VARCHAR (100) NULL,
    [IdUsuarioUltModi] VARCHAR (20)  NULL,
    [FechaAnu]         DATETIME      NULL,
    [FechaTransac]     DATETIME      NOT NULL,
    [FechaUltModi]     DATETIME      NULL,
    [Estado]           CHAR (1)      NOT NULL,
    CONSTRAINT [PK_ro_tipoNomina] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNomina_Tipo] ASC)
);

